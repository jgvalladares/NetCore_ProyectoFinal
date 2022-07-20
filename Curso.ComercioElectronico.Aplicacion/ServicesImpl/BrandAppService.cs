using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IGenericRepository<Brand> genericRepo;
        private readonly IValidator<CreateBrandDto> validator;
        private readonly IMapper mapper;

        public BrandAppService(IGenericRepository<Brand> genericRepository, IValidator<CreateBrandDto> validator, IMapper mapper)
        {
            genericRepo = genericRepository;
            this.validator = validator;
            this.mapper = mapper;
        }
   
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            var query = await genericRepo.GetAsync();

            var result = query.Select(x => new BrandDto
            {
                Code = x.Codigo,
                Description = x.Nombre,
                CreateDate = x.CreationDate
            });
            return result.ToList();
        }

        public async Task<BrandDto> GetAsync(string codigo)
        {
            var entity = await genericRepo.GetAsync(codigo);
            return new BrandDto
            {
                Code = entity.Codigo,
                Description = entity.Nombre,
                CreateDate = entity.CreationDate
            };
        }


        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            await validator.ValidateAndThrowAsync(brandDto);
            
            var brand = new Brand {
                Codigo = brandDto.Code,
                Nombre = brandDto.Description,
                CreationDate = DateTime.Now 
            };
            
        
            await genericRepo.CreateAsync(brand);
            
      
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var bran = await genericRepo.GetAsync(id);

            await genericRepo.DeleteAsync(bran);
            return true;
        }

        public async Task UpdateAsync(string id, CreatePutBrandDto putBrandDto)
        {
            var bran = await genericRepo.GetAsync(id);

            bran.Codigo = putBrandDto.Code;
            bran.Nombre = putBrandDto.Description;
            bran.CreationDate = DateTime.Now;

            await genericRepo.UpdateAsync(bran);
        }

        public async Task<PaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            var query = genericRepo.GetQueryable();
            //Filtra no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //0. Busqueda
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Codigo.ToUpper().Contains(search)
                //|| x.codigo.Tuopper().startsWith(search))
                );
            }

            //1. Total 
            var total = await query.CountAsync(); //

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

            var result = query.Select(x => new BrandDto
            {
                Code = x.Codigo,
                Description = x.Nombre,
                CreateDate = x.CreationDate
            });

            var items = await result.ToListAsync();

            var resultado = new PaginacionBrand<BrandDto>();
            resultado.Total = total;
            resultado.Item = items;
            return resultado;
        }
    }
}

