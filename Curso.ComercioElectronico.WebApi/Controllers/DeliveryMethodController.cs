using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //indica que el usuario debe estar autentificado para acceder
    public class DeliveryMethodController : ControllerBase, IDeliveryMethodAppService
    {
        private readonly IDeliveryMethodAppService service;
        public DeliveryMethodController(IDeliveryMethodAppService deliveryMethodAppService)
        {
            this.service = deliveryMethodAppService;
        }
        [HttpGet]
        public async Task<ICollection<DeliveryMethodDto>> GetAllAsync()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("Code")]
        public async Task<DeliveryMethodDto> GetAsync(string codigo)
        {
            return await service.GetAsync(codigo);
        }
        [HttpPost]
        public  Task CreateAsync(CreateDeliveryMethodDto deliveryMethodDto)
        {
            return service.CreateAsync(deliveryMethodDto);
        }
        [HttpDelete("Code")]
        public  Task<bool> DeleteAsync(string id)
        {
            return service.DeleteAsync(id);
        }
        [HttpPut("Code")]
        public  Task UpdateAsync(string id, CreateDeliveryMethodDto putDeliveryMethodDto)
        {
            return service.UpdateAsync(id, putDeliveryMethodDto);
        }
    }
}
