using Curso.CursoElectronico.Dominio;
using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura
{
    public class ComercioElectronicoDbContext : DbContext
    {
        public ComercioElectronicoDbContext(DbContextOptions options) : base(options) 
        {
        } 
       
        public DbSet<Product> Products { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //1. Primero configurar proveedor 

            //2. Configurar conexion
            //var conexion = @"Server=(localdb)\mssqllocaldb;Database=CursoNet.ComercioElectronico;Trusted_Connection=True";
            //optionsBuilder.UseSqlServer(conexion);
        }

        //Mediante este metodo le coloco cuantos caracteres tendra mi clave primaria asi como mi descripcion 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Medinte este metodo llamamos a todas las Configuraciones 
            
        }
    }
}
