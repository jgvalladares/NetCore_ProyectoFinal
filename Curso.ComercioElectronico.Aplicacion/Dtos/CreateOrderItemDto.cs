using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public Guid? OrderId { get; set; }
        public int QuantityProduct { get; set; }
    }
}
