using FluentValidation;

namespace Application.Commands.Property
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .When(x => x.Title != null) 
                .WithMessage("Title cannot be empty!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .When(x => x.Description != null) 
                .WithMessage("Description cannot be empty!");

            RuleFor(x => x.Type)
                .IsInEnum()
                .When(x => x.Type.HasValue) 
                .WithMessage("Invalid property type.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .When(x => x.Status.HasValue)
                .WithMessage("Invalid property status.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .When(x => x.Price.HasValue)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .When(x => x.Address != null)
                .WithMessage("Address cannot be empty!");

            RuleFor(x => x.Area)
                .GreaterThan(0)
                .When(x => x.Area.HasValue) 
                .WithMessage("Area must be greater than zero.");

            RuleFor(x => x.Rooms)
                .GreaterThan(0)
                .When(x => x.Rooms.HasValue)
                .WithMessage("Rooms must be greater than zero.");

            RuleFor(x => x.Bathrooms)
                .GreaterThan(0)
                .When(x => x.Bathrooms.HasValue)
                .WithMessage("Bathrooms must be greater than zero.");

            RuleFor(x => x.ConstructionYear)
                .GreaterThan(0)
                .When(x => x.ConstructionYear.HasValue) 
                .WithMessage("Construction Year must be valid.");
        }
    }
}
