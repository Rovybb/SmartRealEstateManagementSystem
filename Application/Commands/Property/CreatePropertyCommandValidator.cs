using FluentValidation;

namespace Application.Commands.Property
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Rooms).GreaterThan(0);
            RuleFor(x => x.Bathrooms).NotEmpty();
            RuleFor(x => x.ConstructionYear).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty();
        }
    }
}
