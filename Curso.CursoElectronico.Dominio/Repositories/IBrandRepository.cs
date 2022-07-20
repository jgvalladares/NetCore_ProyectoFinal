using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Repositories
{
    public interface IBrandRepository
    {
        Task<ICollection<Brand>> GetAsync();
        Task<Brand> GetAsync(string codigo);
    }
}
