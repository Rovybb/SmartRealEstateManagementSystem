using Domain.Types.Inquiry;
using Domain.Utils;
using MediatR;

namespace Application.Commands.Inquiry
{
    public class UpdateInquiryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required InquiryStatus Status { get; set; }
        public required Guid PropertyId { get; set; }
        public required Guid ClientId { get; set; }
        public required Guid AgentId { get; set; }
    }
}
