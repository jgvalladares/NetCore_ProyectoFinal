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
    public class OrderController: ControllerBase,IOrderItemAppServices
    {
        private readonly OrderItemService service;
        public OrderController(OrderItemService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderItem)
        {
            return await service.AddProductAsync(createOrderItem);
        }
        [HttpDelete]
        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            return await service.CancelAsync(orderId);
        }
        [HttpGet("{orderId}")]
        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            return await service.GetByIdAsync(orderId);
        }
        [HttpPut("{orderId}")]
        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            return await service.PayAsync(orderId);
        }
        [HttpDelete("/order/{orderId}/productId")]
        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await service.RemoveProductAsync(orderId,productId); 
        }
        [HttpPut("/order/{orderId}/item/")]
        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderIem)
        {
            return await service.UpdateProductAsync(orderId, orderIem); 
        }
    }
}
