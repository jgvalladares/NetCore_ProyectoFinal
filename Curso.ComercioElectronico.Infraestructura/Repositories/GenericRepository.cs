using Curso.CursoElectronico.Dominio.Base;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ComercioElectronicoDbContext context;

        public GenericRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<T>> GetAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
     
        public async Task<ICollection<T>> GetListaAsync(int limit = 10)
        {
            var consulta = context.Set<T>()
                            .Take(limit);

            return await consulta.ToListAsync();
        }
        public async Task<T> GetAsync(string code)
        {
            return await context.Set<T>().FindAsync(code);
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();   //confirmamos el cambio que se realiza
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }
        public async Task<IQueryable<Order>> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = context.Orders.Where(o => o.Id == orderId).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
                throw new ArgumentException($"La orden {orderId} no existe");

            //order.Status = Status.Pagado.ToString();
            order.CreationDate = DateTime.Now;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return query;
        }

        public Task<T> AddProductOrder(T orderItem)
        {
            throw new NotImplementedException();
        }

        Task<T> IGenericRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

    }
}
