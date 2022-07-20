using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //indica que el usuario debe estar autentificado para acceder
    public class TypeProductController : ControllerBase, ITypeProductAppService
    {
        private readonly ITypeProductAppService service;
        public TypeProductController(ITypeProductAppService tpeProductAppService)
        {
            service = tpeProductAppService;
        }
        [HttpGet]
        public Task<ICollection<TypeProductDto>> GetAllAsync()
        {
            return service.GetAllAsync();
        }

        [HttpGet("Codigo")]
        public async Task<List<TypeProductDto>> GetAsync(string Codigo)
        {
            return await service.GetAsync(Codigo);
        }

        [HttpPost]
        public Task CreateAsync(CreateTypeProductDto typeProductDto)
        {
            return service.CreateAsync(typeProductDto);
        }
        [HttpPut("codigo")]
        public Task UpdateAsync(string id, CreateTypeProductDto putTypeProductDto)
        {
            return service.UpdateAsync(id, putTypeProductDto);
        }
        [HttpDelete("codigo")]
        public Task<bool> DeleteAsync(string id)
        {
            return service.DeleteAsync(id);
        }

        [HttpGet("Paginacion")]
        public Task<PaginacionTypeProduct<TypeProductDto>> GetListaAsync(string? search = "", int offset = 0, int limite = 10, string sort = "Code", string order = "asc")
        {
            return service.GetListaAsync(search,offset,limite,sort,order);
        }

    }
}
