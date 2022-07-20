using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Base
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false; //si este campo esta en TRUE se elimina de la TABLA (ELIMINADO LOGICO)

        public DateTime CreationDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool Status { get; set; } = true;
    }
}
