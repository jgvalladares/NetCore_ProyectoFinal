using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly ComercioElectronicoDbContext context;
        public DeliveryMethodRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<DeliveryMethod>> GetAsync()
        {
            return await context.DeliveryMethods.ToListAsync();
        }

        public async Task<DeliveryMethod> GetAsync(string codigo)
        {
            return await context.DeliveryMethods.FindAsync(codigo);
        }
    }
}
