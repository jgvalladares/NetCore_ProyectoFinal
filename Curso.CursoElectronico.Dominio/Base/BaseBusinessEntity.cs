using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Base
{
    public class BaseBusinessEntity : BaseEntity //Mediante baseentty tenemos el CRUD para las tablas 
    {
        public Guid Id { get; set; }
    }
}
