using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ComercioElectronicoDbContext context;

        public OrderRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Order>> GetAsync()
        {
            return await context.Orders.ToListAsync();
        }

       
        public async Task<OrderItem> AddProductAsync(OrderItem orderItem)
        {

            Product product = await ValidateStock(orderItem);
            //si orderProduct.OrderId es vacio crear orden, si no es vacio y la orden existe agrega el producto a la orden
            Order order = new();
            OrderItem orderProductExist = new OrderItem();
            if (orderItem.OrderId == Guid.Empty)
            {
                order = await CreateOrderAsync();
                orderItem.OrderId = order.Id;
                orderItem.Total = product.Precio * orderItem.QuantityProduct;
                //Agregamos orderProduct y guardar cambios
                await context.OrderItems.AddAsync(orderItem);
                await context.SaveChangesAsync();
            }
            else
            {
                Order orderFind = await FindOrderAsync(orderItem);
                orderProductExist = await context.OrderItems.Where(o => o.OrderId == orderFind.Id && o.ProductId == product.Id).SingleOrDefaultAsync();
                //Modificamos la cantidad si existe la orden con producto
                if (orderProductExist != null)
                {
                    orderProductExist.QuantityProduct += orderItem.QuantityProduct;
                    orderProductExist.Total += product.Precio * orderItem.QuantityProduct;
                    // context.Entry(orderProductExist).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                else
                {
                    orderItem.Total = product.Precio * orderItem.QuantityProduct;
                    await context.OrderItems.AddAsync(orderItem);
                    await context.SaveChangesAsync();
                }
            }
            //Quitar cantidad seleccionada de productos al stock del producto y actualizar producto
            product.Stock -= orderItem.QuantityProduct;
            //context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();
            //Si no existe orden retorna la orden creada, sino retorna orden existente
            if (order.Id != Guid.Empty || orderProductExist == null)
            {
                return orderItem;
            }
            return orderProductExist;
        }
        //VALIDACION DEL STOCK DE PRODUCTOS 
        public async Task<Product> ValidateStock(OrderItem orderItem, char operation = 'C')
        {
            //Validar que la cantidad sea mayor a 0
            if (orderItem.QuantityProduct <= 0)
                throw new ArgumentException("La cantidad seleccionada debe ser mayor a 0");

            //Consultar y validar si existe el producto seleccionado
            Product product = await context.Products.FindAsync(orderItem.ProductId);
            if (product == null)
                throw new ArgumentException($"No existe el producto con Id: {orderItem.ProductId}");
            if (operation != 'U')
            {
                //Validar que la cantidad seleccionada sea menor al stock del producto, y que el stock es 0
                if (product.Stock < orderItem.QuantityProduct || product.Stock == 0)
                    throw new ArgumentException($"No suficiente stock del producto, cantidad disponible: {product.Stock}, cantidad seleccionada: {orderItem.QuantityProduct}");
            }
            return product;
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<Order> FindOrderAsync(OrderItem orderItem)
        {
            Order orderFind = await context.Orders.FindAsync(orderItem.OrderId);
            if (orderFind == null)
                throw new ArgumentException($"No existe la orden con id: {orderItem.OrderId}");
            return orderFind;
        }
        //METODO PARA PAGAR
        public async Task<IQueryable<Order>> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = context.Orders.Where(x => x.Id == orderId).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            order.CreationDate = DateTime.Now;
            //context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return query;
        }
        public async Task<Order> CreateOrderAsync()
        {
            //Creando la nueva orden
            Order order = new Order()
            {
                //Status = Status.Pendiente.ToString(),
                Subtotal = 0,
                TotalPrice = 0
            };
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        //public Task<OrderItem> AddProductAsync(OrderItem orderItem)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<OrderItem> UpdateProductAsync(Guid orderId, OrderItem orderItem)
        {
            OrderItem orderProductFind = await context.OrderItems.Where(o => o.ProductId == orderItem.ProductId && o.OrderId == orderId).SingleOrDefaultAsync();
            Product product = await ValidateQuantityStock(orderItem, 'U');
            //Si voy a aumentar cantidad de producto, restar stock del producto,
            //Sino si voy a reducir la cantidad del producto, aumentar stock del producto
            var stockRestado = (orderItem.QuantityProduct - orderProductFind.QuantityProduct);
            var stockAumentado = (orderProductFind.QuantityProduct - orderItem.QuantityProduct);
            if (orderProductFind.QuantityProduct < orderItem.QuantityProduct)
            {
                if (product.Stock - stockRestado < 0)
                    throw new ArgumentException($"No hay suficiente stock del producto, cantidad disponible: {product.Stock}, cantidad seleccionada: {orderItem.QuantityProduct}");

                product.Stock -= stockRestado;
            }
            else
            {
                if (orderProductFind.QuantityProduct < orderItem.QuantityProduct)
                    throw new ArgumentException($"No se permite devolver mas productos de los adquiridos, Productos adquiridos {orderProductFind.QuantityProduct}, productos devueltos {orderItem.QuantityProduct}");

                product.Stock += stockAumentado;
            }
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            orderProductFind.QuantityProduct = orderItem.QuantityProduct;
            orderProductFind.Total = product.Precio * orderProductFind.QuantityProduct;

            context.Entry(orderProductFind).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return orderProductFind;

        }

        public async Task<Product> ValidateQuantityStock(OrderItem orderItem, char operation = 'C')
        {
            //Validar que la cantidad sea mayor a 0
            if (orderItem.QuantityProduct <= 0)
                throw new ArgumentException("La cantidad seleccionada debe ser mayor a 0");

            //Consultar y validar si existe el producto seleccionado
            Product product = await context.Products.FindAsync(orderItem.ProductId);
            if (product == null)
                throw new ArgumentException($"No existe el producto con Id: {orderItem.ProductId}");
            if (operation != 'U')
            {
                //Validar que la cantidad seleccionada sea menor al stock del producto, y que el stock es 0
                if (product.Stock < orderItem.QuantityProduct || product.Stock == 0)
                    throw new ArgumentException($"No suficiente stock del producto, cantidad disponible: {product.Stock}, cantidad seleccionada: {orderItem.QuantityProduct}");
            }

            return product;
        }


        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            //devolver stock del producto 
            OrderItem orderProduct = await context.OrderItems.Where(o => o.OrderId == orderId && o.ProductId == productId).SingleOrDefaultAsync();
            if (orderProduct == null)
                throw new ArgumentException($"No existe la orden {orderId} con el producto {productId}");
            context.OrderItems.Remove(orderProduct);
            await context.SaveChangesAsync();

            var quantity = orderProduct.QuantityProduct;
            Product product = await context.Products.FindAsync(productId);
            if (product == null)
                throw new ArgumentException($"No existe el producto {productId}");
            product.Stock += quantity;
            //context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return true;


        }

        public IQueryable<Order> GetByIdAsync(Guid orderId)
        {
            return context.Orders.Where(o => o.Id == orderId).AsQueryable();

        }

        public async Task<IQueryable<Order>> CancelAsync(Guid orderId)
        {
            var query = context.Orders.Where(x => x.Id == orderId).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
            {
                throw new ArgumentException($"No existe la orden con id: {orderId}");
            }
            else
            {
                List<OrderItem> orderItem = await context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
                Product productSelected = new Product();
                foreach (var i in orderItem)
                {
                    productSelected = await context.Products.Where(o => o.Id == i.ProductId).SingleAsync();
                    productSelected.Stock += i.QuantityProduct;
                    await context.SaveChangesAsync();
                }
            }
            await context.SaveChangesAsync();

            return query;
        }



        public async  Task<Order> UpdateOrderAsync(Guid orderId, decimal subtotal, decimal totalPrice)
        {
            Order order = await context.Orders.FindAsync(orderId);
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            order.Subtotal = subtotal;
            order.TotalPrice = totalPrice;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return order;


        }
    }
}

