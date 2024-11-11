using FluentValidation;

namespace Application.Commands.Inquiry
{
    public class CreateInquiryCommandValidator : AbstractValidator<CreateInquiryCommand>
    {
        public CreateInquiryCommandValidator()
        {
            RuleFor(x => x.PropertyId).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.AgentId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Status).NotEmpty().IsInEnum();
        }
    }
}
