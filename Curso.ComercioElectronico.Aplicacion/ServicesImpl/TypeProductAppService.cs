using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
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
    public class TypeProductAppService : ITypeProductAppService
    {
        private readonly IGenericRepository<TypeProduct> genericRepo;
        private readonly ITypeProductRepository typeProduct;
        private readonly IMapper mapper;

        public TypeProductAppService(IGenericRepository<TypeProduct> genericRepository, ITypeProductRepository typeProduct, IMapper mappre)
        {
            genericRepo = genericRepository;
            this.typeProduct = typeProduct;
            this.mapper = mapper;
        }

       
        public async Task<ICollection<TypeProductDto>> GetAllAsync()
        {
            var query = await genericRepo.GetAsync();
            var result = query.Select(x => new TypeProductDto
            {
                Code = x.Codigo,
                Description = x.Nombre,
                CreateDate = x.CreationDate
            });

            return result.ToList();
        }

        public async Task<List<TypeProductDto>> GetAsync(string id)
        {
            var entity = await typeProduct.GetAsync(id);
            var listTypeDto = mapper.Map<List<TypeProductDto>>(entity);
            return listTypeDto;

        }

        public async Task CreateAsync(CreateTypeProductDto typeProductDto)
        {
            var typeProduct = new TypeProduct {
                Codigo = typeProductDto.Code,
                Nombre = typeProductDto.Description,
                CreationDate = DateTime.Now
        };

            await genericRepo.CreateAsync(typeProduct);
        }
        public async Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto)
        {
            var type = await genericRepo.GetAsync(id);

            type.Codigo = putTypeProductDto.Code;
            type.Nombre = putTypeProductDto.Description;
            type.CreationDate = DateTime.Now;

            await genericRepo.UpdateAsync(type);
            
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var type = await genericRepo.GetAsync(id);
            await genericRepo.DeleteAsync(type);

            return true;
        }
    
        public async Task<PaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            var query = genericRepo.GetQueryable();
       
            query = query.Where(x => x.IsDeleted == false);

            //0. BUsqueda
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Codigo.ToUpper().Contains(search)
        
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
                    case "CODE":
                        query = query.OrderBy(x => x.Codigo);
                        break;
                    
                    default:
                        throw new ArgumentException($"el parametro sort {sort} n es soportado!");
                }
            }

            var result = query.Select(x => new TypeProductDto
            {
                Code = x.Codigo,
                Description = x.Nombre,
                CreateDate = x.CreationDate
            });
            var items = await result.ToListAsync();
            var resultado = new PaginacionTypeProduct<TypeProductDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }

        
    }
}
