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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    { 

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.QuantityProduct).IsRequired();

            //Mediante este codigo realizo las relaciones de tablas
            builder.HasOne(b => b.Order) 
                .WithMany()
                .HasForeignKey(b => b.OrderId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Product) 
                .WithMany()
                .HasForeignKey(b => b.ProductId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsDeleted).IsRequired();
        }
    }
}
