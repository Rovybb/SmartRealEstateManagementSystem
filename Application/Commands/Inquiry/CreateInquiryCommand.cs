using Domain.Types.Inquiry;
using Domain.Utils;
using MediatR;

namespace Application.Commands.Inquiry
{
    public class CreateInquiryCommand : IRequest<Result<Guid>>
    {
        public Guid PropertyId { get; set; }
        public Guid ClientId { get; set; }
        public Guid AgentId { get; set; }
        public string Message { get; set; }
        public InquiryStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
