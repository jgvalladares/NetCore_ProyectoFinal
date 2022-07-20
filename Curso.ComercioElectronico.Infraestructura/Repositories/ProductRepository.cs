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
    public class ProductRepository : IProductRepository
    {
        private readonly ComercioElectronicoDbContext context;

        public ProductRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }


        public async Task<ICollection<Product>> GetAsync()
        {

            return await context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAsync(Guid Id)
        {
            var query = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(Id.ToString()))
            {
                query = from product in query
                        where product.Id.ToString().Contains(Id.ToString()) || product.Nombre.Contains(Id.ToString())
                        select product;
            };
            return await query.ToListAsync();
        }
        public async Task<List<Product>> GetAsync(string code)
        {
            var query = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(code))
            {
                query = from product in query
                        where product.Id.ToString().Contains(code) || product.Nombre.Contains(code)
                        select product;
            };
            return await query.ToListAsync();
        }
        public async Task<IQueryable<Product>> GetByIdAsync(Guid id)
        {
            var query = context.Products.Where(x => x.Id == id);
            Product productExist = await query.SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"El producto {id} no existe");
            }
            return query;
        }

        public async Task<IQueryable<Product>> PostAsync(Product product)
        {
            var query = context.Products.Where(b => b.Nombre == product.Nombre);
            if (product.Stock < 0)
                throw new ArgumentException("El stock no puede ser menor a 0");

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return query;

        }

        public async Task<IQueryable<Product>> PutAsync(Product product)
        {
            var query = context.Products.Where(b => b.Id == product.Id);
            bool productExist = context.Products.Any(b => b.Id == product.Id);
            if (!productExist)
                throw new ArgumentException($"El producto  {product.Id} no existe");

            if (product.Stock < 0)
                throw new ArgumentException("El stock no debe ser menor a 0");

            product.ModifiedDate = DateTime.Now;
            await context.SaveChangesAsync();
            return query;

        }
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            Product productExist = await context.Products.Where(b => b.Id == id).SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"El producto {id} no existe");
            }
            productExist.ModifiedDate = DateTime.Now;

            context.Products.Update(productExist);
            await context.SaveChangesAsync();
            return true;

        }
    }




}
