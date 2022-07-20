using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductAppService
    {
       
        //paginacion, ordenamiento y busquedas 
        Task<Paginacion<ProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 0, string sort = "", string order = "asc");
        Task<ICollection<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetAsync(Guid Id);
        Task<List<ProductDto>> GetAsync(string code);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(Guid id, CreateProductDto putproductDto);
        Task<bool> DeleteAsync(Guid id);

    }

    public class Paginacion<T>
    {
        public int Total { get; set; }
        public ICollection<T> Item { get; set; } = new List<T>();
    }

}
