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
    public class TypeProductConfiguration : IEntityTypeConfiguration<TypeProduct>
    {
        public void Configure(EntityTypeBuilder<TypeProduct> builder)
        {
            builder.ToTable("TypeProducts");
            builder.HasKey(b => b.Codigo);

            builder.Property(b => b.Codigo).HasMaxLength(4).IsRequired();

            builder.Property(b => b.Nombre).HasMaxLength(256).IsRequired();
        }


    }
}
