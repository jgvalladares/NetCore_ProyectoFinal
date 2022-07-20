using Curso.ComercioElectronico.Aplicacion;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //3. Se establece los apis que requiero usuario autentificado
    //[Authorize(Roles ="Admin")] //mediante este atributo se necesita que el usuario este AUTENFIFICADO para poder acceder 
    public class BrandController : ControllerBase, IBrandAppService
    {
        private readonly IBrandAppService service;
        private readonly IValidator<CreateProductDto> validator;
        public BrandController(IBrandAppService bandAppService, IValidator<CreateProductDto> validator)
        {
            this.service = bandAppService;
            this.validator = validator;
        }
     
        [HttpGet]
        //[Authorize(Roles = "Admin,Support")]
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("Code")]
        public async Task<BrandDto> GetAsync(string Codigo)
        {
            return await service.GetAsync(Codigo);
        }
        /// <summary>
        /// para hacer la paginación
        /// </summary>
        /// <param name="search"></param>
        /// <param name="offset"></param>
        /// <param name="limite"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpGet("Pagination")]
        public Task<PaginacionBrand<BrandDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            return service.GetListaAsync(search, offset, limite, sort, order);
        }

        [HttpPost]
        public Task CreateAsync(CreateBrandDto brandDto)
        {   
                return service.CreateAsync(brandDto);
            
        }

        [HttpDelete("Code")]
        public Task<bool> DeleteAsync(string Codigo)
        {
            return service.DeleteAsync(Codigo);
        }

        [HttpPut("Code")]
        public Task UpdateAsync(string Codigo, CreatePutBrandDto brandDto)
        {
            return service.UpdateAsync(Codigo, brandDto);
        }
        
    }
}
