using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAsync();
        Task<List<Product>> GetAsync(Guid id);
        Task<List<Product>> GetAsync(string code);
        Task<IQueryable<Product>> GetByIdAsync(Guid id);
        Task<IQueryable<Product>> PostAsync(Product product);
        Task<IQueryable<Product>> PutAsync(Product product);
        Task<bool> DeleteByIdAsync(Guid id);


    }
}
