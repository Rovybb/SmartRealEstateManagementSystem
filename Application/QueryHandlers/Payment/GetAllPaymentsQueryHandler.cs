using Application.DTOs;
using Application.Queries.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Payment
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, Result<IEnumerable<PaymentDTO>>>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<PaymentDTO>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var result = await paymentRepository.GetAllAsync();
            if (result == null)
            {
                return Result<IEnumerable<PaymentDTO>>.Failure("No payments found");
            }

            var payments = result.Select(payment => mapper.Map<PaymentDTO>(payment));
            return Result<IEnumerable<PaymentDTO>>.Success(payments);
        }
    }
}
