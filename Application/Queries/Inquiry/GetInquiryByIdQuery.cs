using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.Inquiry
{
    public class GetInquiryByIdQuery : IRequest<Result<InquiryDTO>>
    {
        public Guid Id { get; set; }
    }
}
