using Curso.CursoElectronico.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Entities
{
    public class DeliveryMethod : BaseCatalogEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
