using Curso.CursoElectronico.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Entities
{
    public class Order : BaseBusinessEntity
    {
        public decimal Subtotal { get; set; }
        public decimal TotalPrice { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string DeliveryMethodId { get; set; } 
        public virtual List<OrderItem> orderItems { get; set; }
    }
}
