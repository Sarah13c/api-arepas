using Arepas.Api.Dtos;
using FluentValidation;

namespace Arepas.Api.Validators
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El customerId es Requerido");


            RuleFor(x => x.DeliveryFullName)
                .NotEmpty().WithMessage("El Nombre Completo es Requerido")
                .MaximumLength(100).WithMessage("La Longitud Maxima para Nombre Completo de la orden es de 100 Caracteres");

            RuleFor(x => x.DeliveryAddress)
                .NotEmpty().WithMessage("La Dirección de Entrega es Requerida")
                .MaximumLength(250).WithMessage("La Longitud Maxima para la Dirección de Entrega es de 250 Caracteres");

            RuleFor(x => x.DeliveryPhoneNumber)
                .NotEmpty().WithMessage("El Teléfono del Cliente es Requerido")
                .MaximumLength(50).WithMessage("La Longitud Maxima para el Teléfono de la orde es de 50 Caracteres");

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("El Precio Total es Requerido");

            RuleFor(x => x.Notes)
                .MaximumLength(250).WithMessage("La Longitud Maxima para las Notas es de 250 Caracteres");
        }
    }
}