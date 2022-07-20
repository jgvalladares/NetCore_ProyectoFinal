using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class UpdateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }

    }
}
