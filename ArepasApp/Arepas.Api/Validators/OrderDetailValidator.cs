using Arepas.Api.Dtos;
using FluentValidation;

namespace Arepas.Api.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailDto>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El Campo Id es Requerido");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("El OrderId es Requerido");



            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El ProductId es Requerido");


            RuleFor(x => x.Quantity)
           .NotNull().WithMessage("El Campo Quantity es Requerido")
           .GreaterThan(0).WithMessage("La Cantidad debe ser Mayor a Cero");


            RuleFor(x => x.PriceOrd)
                .NotNull().WithMessage("El Campo PriceOrd es Requerido")
                .GreaterThan(0).WithMessage("El Precio debe ser Mayor a Cero"); ;
        }
    }
}
