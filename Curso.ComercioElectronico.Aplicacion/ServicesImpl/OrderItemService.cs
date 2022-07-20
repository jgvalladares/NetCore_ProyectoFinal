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
    public class OrderItemService: IOrderItemAppServices
    {
        private readonly IOrderRepository orderrepo;

        private readonly IMapper mapper;
        public OrderItemService(IOrderRepository orderrepo, IMapper mapper)
        {
            this.orderrepo = orderrepo;
            this.mapper = mapper;
        }

        public async Task<OrderDto> GetResult(IQueryable<Order> query)
        {
            decimal total = 0;
            OrderDto orderDto = await query.Select(x => new OrderDto()
            {
                Code = x.Id,
                DeliveryMethodId = x.DeliveryMethodId,
                DeliveryMethod = x.DeliveryMethod,
                orderItems= x.orderItems.Select(op => new OrderItemResultDto()
                {
                    Product = op.Product.Nombre,
                    Price = op.Product.Precio,
                    QuantityProduct = op.QuantityProduct,
                    Total = op.Total
                }).ToList(),

                Subtotal = x.Subtotal,
                TotalPrice = x.TotalPrice
            }).SingleOrDefaultAsync();
            foreach (var product in orderDto.orderItems)
            {
                total += product.Total;
            }
            orderDto.Subtotal = total - (total * (decimal)0.12);
            //orderDto.Iva = (total * (decimal)0.12);
            orderDto.TotalPrice = total;
            return orderDto;
        }
    

        public Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderItem)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(createOrderItem);
            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(await orderrepo.AddProductAsync(orderItem));
            return orderItemDto;


        }

        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            IQueryable<Order> query = await orderrepo.CancelAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            return orderDto;
            throw new NotImplementedException();
        }

        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = await orderrepo.PayAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            await orderrepo.UpdateOrderAsync(orderId, orderDto.Subtotal, orderDto.TotalPrice);
            return orderDto;

        }

        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await orderrepo.RemoveProductAsync(orderId, productId);

        }

        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderItemDto)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(orderItemDto);
            OrderItemDto orderItemResultDto = mapper.Map<OrderItemDto>(await orderrepo.UpdateProductAsync(orderId, orderItem));
            return orderItemResultDto;
        }

    }
}
