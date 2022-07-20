using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.CursoElectronico.Dominio.Dependency_Injection
{
    public static class DominioServiceCollectionExtensions
    {
        public static IServiceCollection AddDominio(this IServiceCollection services, IConfiguration config)
        {
          
            return services;
        }
    }
}
