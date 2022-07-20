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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    { 

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.Nombre).HasMaxLength(256).IsRequired();

            builder.Property(b => b.Descripcion).HasMaxLength(256);

            builder.Property(b => b.Precio).IsRequired().HasColumnType("decimal(15,2)");

            //Mediante este codigo realizo las relaciones de tablas
            builder.HasOne(b => b.TypeProduct) 
                .WithMany()
                .HasForeignKey(b => b.TypeProductId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Brand) 
                .WithMany()
                .HasForeignKey(b => b.BrandId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.IsDeleted).IsRequired();

          
        }
    }
}
