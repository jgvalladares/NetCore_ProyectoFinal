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
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
      

        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
           
                builder.ToTable("DeliveryMethods");

                builder.HasKey(b => b.Codigo);

                builder.Property(b => b.Codigo).HasMaxLength(5).IsRequired();                

                builder.Property(b => b.Description).HasMaxLength(256).IsRequired();
            
        }
    }
}
