using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderDto
    {
        public Guid Code { get; set; }
        public virtual List<OrderItemResultDto> orderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string DeliveryMethodId { get; set; }
        public decimal Subtotal { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;

    }
}
