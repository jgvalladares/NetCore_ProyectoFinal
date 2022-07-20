using Curso.CursoElectronico.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Entities
{
    public class Product : BaseBusinessEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; } 
        public TypeProduct TypeProduct { get; set; }

        public string TypeProductId { get; set; } //Esto nos ayuda a mapear las relaciones 
        public Brand Brand { get; set; }

        public string BrandId { get; set; } //ESTO NOS AYUDA A MAPEAR LAS RELACIONES
    }
}
