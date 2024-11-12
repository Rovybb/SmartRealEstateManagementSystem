using FluentValidation;

namespace Application.Commands.Inquiry
{
    public class UpdateInquiryCommandValidator : AbstractValidator<UpdateInquiryCommand>
    {
        public UpdateInquiryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status must be a valid enum value.");
            RuleFor(x => x.PropertyId).NotEmpty().WithMessage("PropertyId is required.");
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.");
            RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.AgentId).NotEmpty().WithMessage("AgentId is required.");
        }
    }
}
