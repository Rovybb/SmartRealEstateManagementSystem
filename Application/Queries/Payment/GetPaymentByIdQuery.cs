using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.Payment
{
    public class GetPaymentByIdQuery : IRequest<Result<PaymentDTO>>
    {
        public Guid Id { get; set; }
    }
}
