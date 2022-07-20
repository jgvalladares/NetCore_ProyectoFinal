using Curso.CursoElectronico.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Entities
{
    public class Brand : BaseCatalogEntity
    {
        public string Nombre { get; set; }
    }
}
