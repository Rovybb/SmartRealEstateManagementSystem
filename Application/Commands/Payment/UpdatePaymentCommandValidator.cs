using FluentValidation;

namespace Application.Commands.Payment
{
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentCommandValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PropertyId).NotEmpty();
            RuleFor(x => x.BuyerId).NotEmpty();
            RuleFor(x => x.SellerId).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.PaymentMethod).IsInEnum();
        }
    }
}
