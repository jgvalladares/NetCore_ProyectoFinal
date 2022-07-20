using Curso.CursoElectronico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    { 

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.Subtotal).IsRequired().HasColumnType("decimal(15,2)");

            builder.Property(b => b.TotalPrice).IsRequired().HasColumnType("decimal(15,2)");

            

            //Mediante este codigo realizo las relaciones de tablas
            builder.HasOne(b => b.DeliveryMethod) //Un tipo de producto puede tener muchos productos 
                .WithMany()
                .HasForeignKey(b => b.DeliveryMethodId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsDeleted).IsRequired();

        }
    }
}
