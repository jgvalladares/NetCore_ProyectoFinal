using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.CursoElectronico.Dominio.Repositories;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dependency_Injection
{
    public static class AplicacionServiceCollectionExtensions
    {
        
        public static IServiceCollection AddAplicacion(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddTransient<IBrandAppService, BrandAppService>();
            services.AddTransient<ITypeProductAppService, TypeProductAppService>();
            services.AddTransient<IProductAppService, ProductAppService>();
            services.AddTransient<IDeliveryMethodAppService, DeliveryMethodAppServices>();
            services.AddTransient<IOrderItemAppServices, OrderItemService>();
            //Validaciones
            services.AddValidatorsFromAssemblyContaining<BrandValidator>();


            //AUTOMAPPER
            //agrega todos los profiles que existen en este proyecto
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
        
    }
}
