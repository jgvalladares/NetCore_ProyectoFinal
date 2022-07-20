using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Dependency_Injection
{
    public static class InfraestructuraServiceCollectionExtensions 
    {
        
        public static IServiceCollection AddInfraestructura  (this IServiceCollection services, IConfiguration config) 
        {
            //Agregar conexion a BASE DE DATOS 
            services.AddDbContext<ComercioElectronicoDbContext>(options => {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            }); //Estoy inicializando el contexto 

            
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITypeProductRepository, TypeProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IDeliveryMethodRepository, DeliveryMethodRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
            
        }
    
    
    }
}
