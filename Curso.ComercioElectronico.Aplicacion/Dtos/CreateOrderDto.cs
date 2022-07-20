using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class CreateOrderDto
    {
        public decimal Subtotal { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
    }
}
