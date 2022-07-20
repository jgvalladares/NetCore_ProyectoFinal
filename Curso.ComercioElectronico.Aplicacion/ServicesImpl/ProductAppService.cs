using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class ProductAppService : IProductAppService
    {
        private readonly IGenericRepository<Product> genericRepo;
        private readonly IProductRepository product;
        private readonly IMapper mapper;

        public ProductAppService(IGenericRepository<Product> genericRepository, IProductRepository product,IMapper mapper)
        {
            genericRepo = genericRepository;
            this.product = product;
            this.mapper = mapper;
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var query = genericRepo.GetQueryable();
            var result = query.Select(x => new ProductDto
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                Stock=x.Stock,
                TypeProduct = x.TypeProduct.Nombre,
                Brand = x.Brand.Nombre
            });


            return await result.ToListAsync();
        }
        public async Task<List<ProductDto>> GetAsync(Guid id)
        {
            var entity = await product.GetAsync(id);
            var listProductDto = mapper.Map<List<ProductDto>>(entity);
            return listProductDto;

        }
        public async Task<List<ProductDto>> GetAsync(string code)
        {
            var entity = await product.GetAsync(code);
            var listProductDto = mapper.Map<List<ProductDto>>(entity);
            return listProductDto;

        }

        public async Task CreateAsync(CreateProductDto productDto)
        {

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Nombre = productDto.Name,
                Descripcion = productDto.Description,
                Precio = productDto.Price,
                Stock = productDto.Stock,
                BrandId = productDto.BrandId,
                TypeProductId = productDto.TypeProductId,
                CreationDate = DateTime.Now
            };

            await genericRepo.CreateAsync(product);
          
        }

        public async Task UpdateAsync(Guid id, CreateProductDto putProductDto)
        {
            var product = await genericRepo.GetAsync(id);

            product.Nombre = putProductDto.Name;
            product.Descripcion = putProductDto.Description;
            product.Precio = putProductDto.Price;
            product.BrandId = putProductDto.BrandId;
            product.TypeProductId = putProductDto.TypeProductId;

            await genericRepo.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await genericRepo.GetAsync(id);
            await genericRepo.DeleteAsync(product);
            return true;

        }

        // PAGINACION
        public async Task<ICollection<ProductDto>> GetListaAsync(int limit = 10)
        {
            //LINQ
            var consulta = await genericRepo.GetListaAsync(limit);
            var resultLINQUDto = consulta.Select(x => new ProductDto()
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
            });
            return resultLINQUDto.ToList();
        }

        public async Task<Paginacion<ProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Name", string order = "asc")
        {
            var query = genericRepo.GetQueryable();
          
            query = query.Where(x => x.IsDeleted == false);

            //0. BUsqueda
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Nombre.ToUpper().Contains(search)
               
                );
            }

            //1. Total 
            var total = await query.CountAsync(); 

            //2. Paginacion 
            query = query.Skip(offset).Take(limite);

            //3. Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToUpper())
                {
                    case "NAME":
                        query = query.OrderBy(x => x.Nombre);
                        break;
                    case "PRICE":
                        query = query.OrderBy(x => x.Precio);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new ProductDto
            {
                Code = x.Id,
                Name = x.Nombre,
                Description = x.Descripcion,
                Price = x.Precio,
                TypeProduct = x.TypeProduct.Nombre,
                Brand = x.Brand.Nombre
            });
            var items = await result.ToListAsync();
            var resultado = new Paginacion<ProductDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }

    
    }
}
