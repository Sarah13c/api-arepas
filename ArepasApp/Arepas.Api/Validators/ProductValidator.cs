using Arepas.Api.Dtos;
using FluentValidation;

namespace Arepas.Api.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El Campo Id es Requerido");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El Nombre del Producto es Requerido")
                .MaximumLength(50).WithMessage("La Longitud Maxima para el Campo Name es de 50 Caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("La Longitud Maxima para Description es de 250 Caracteres");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("El Precio es Requerido")
                .GreaterThan(0).WithMessage("El Precio Total debe ser Mayor a Cero");



            RuleFor(x => x.Image)
                .MaximumLength(250).WithMessage("La Longitud Maxima para el la Imagen es de 250 Caracteres");
        }
    }
}
