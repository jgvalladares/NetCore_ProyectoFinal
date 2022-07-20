using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator()
        {
            ///// not null me indica que el valor no puede ser vacio
            RuleFor(x => x.Name).NotNull().Length(4,50);
            RuleFor(x => x.Description).Length(5, 50);
            RuleFor(x => x.Price).NotNull();
        }
    }
}
