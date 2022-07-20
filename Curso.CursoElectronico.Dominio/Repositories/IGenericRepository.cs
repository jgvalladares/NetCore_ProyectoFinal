using Curso.CursoElectronico.Dominio.Base;
using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        
        Task<ICollection<T>> GetAsync();

        //el limit indica que se ueden presentar 10 item por pagina
        Task<ICollection<T>> GetListaAsync(int limit = 10);

    
        Task<T> GetAsync(string code);

        Task<T> AddProductOrder(T orderItem);

        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
