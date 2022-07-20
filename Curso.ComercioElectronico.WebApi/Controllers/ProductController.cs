using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    //SEGMENTOS: 
    //api/
    //[controller] => 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //indica que el usuario debe estar autentificado para acceder
    public class ProductController : ControllerBase, IProductAppService
    {
        private readonly IProductAppService service;
        public ProductController(IProductAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        //[Authorize(Policy = "TieneUnaLicencia")]
        public Task<ICollection<ProductDto>> GetAllAsync()
        {
            return service.GetAllAsync();
        }
        // id = GUID roducto...
        [HttpGet("Id")]
        public Task<List<ProductDto>> GetAsync(Guid Id)
        {
            return service.GetAsync(Id);        
        }
        [HttpGet("code")]
        public Task<List<ProductDto>> GetAsync(string code)
        {
            return service.GetAsync(code);
        }


        [HttpPost]
        public Task CreateAsync(CreateProductDto productDto)
        {
            return service.CreateAsync(productDto);
        }

        [HttpPut("Id")]
        public Task UpdateAsync(Guid id, CreateProductDto putproductDto)
        {
            return service.UpdateAsync(id,putproductDto);
        }

        [HttpDelete("Id")]
        public Task<bool> DeleteAsync(Guid id)
        {
            return service.DeleteAsync(id);
        }

     

        [HttpGet("/search")]
        //[Route("search")]
        public async Task<Paginacion<ProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Name", string order = "asc")
        {
            return await service.GetListaAsync(search, offset, limite, sort, order);
        }
    }  
}

