using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class BrandValidator : AbstractValidator<CreateBrandDto>
    {
        public BrandValidator()
        {
            ///se utiliza expresiones regulares para determinar la condición de lo que se escribe
            /////esta expresion significa que acepta minusculas y mayuscula y que debe contener un -
            /// el asterisco permite que se repitan los caracteres
            RuleFor(x => x.Code).Matches("^[a-zA-Z0-9-]*$").WithMessage("El codigo no cumple con las condiciones");
               
            RuleFor(x => x.Description).NotNull().MaximumLength(256).WithMessage("Escriba una descripcion");

            RuleFor(x => x.Description).Must(a => ValidateDescription(a))
                .WithMessage("La descripcion debe contener al menos 10 caracteres."); //EXCEPCIONES PERSONALIZADAS 
        }

        /// <summary>
        /// este es un validador personalizado, aunq se puede realizar con los validares propios.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        private bool ValidateDescription(string desc) 
        {
            if (desc.Length<10 && desc.Length > 30)
            {
                return false;
            }
            return true;
        }
    }
}
