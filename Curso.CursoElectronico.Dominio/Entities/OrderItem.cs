using Curso.CursoElectronico.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Entities
{
    public class OrderItem : BaseBusinessEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int QuantityProduct { get; set; } = 1;
        public decimal Total { get; set; }
    }
}
