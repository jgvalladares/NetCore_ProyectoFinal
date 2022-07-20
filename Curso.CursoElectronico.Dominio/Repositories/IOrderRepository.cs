using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderItem> AddProductAsync(OrderItem orderItem);
        Task<OrderItem> UpdateProductAsync(Guid orderId, OrderItem orderItem);
        Task<bool> RemoveProductAsync(Guid orderId, Guid productId);
        public IQueryable<Order> GetByIdAsync(Guid orderId);
        Task<IQueryable<Order>> PayAsync(Guid orderId);
        public Task<IQueryable<Order>> CancelAsync(Guid orderId);
        public Task<Order> UpdateOrderAsync(Guid orderId, decimal subtotal, decimal totalPrice);
        Task<ICollection<Order>> GetAsync();
        
    }
}
