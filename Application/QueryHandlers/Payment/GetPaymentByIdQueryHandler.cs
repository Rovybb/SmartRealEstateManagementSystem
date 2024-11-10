using Application.DTOs;
using Application.Queries.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Payment
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDTO>>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PaymentDTO>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await paymentRepository.GetByIdAsync(request.Id);
            if (result.IsSuccess)
            {
                return Result<PaymentDTO>.Success(mapper.Map<PaymentDTO>(result.Data));
            }
            return Result<PaymentDTO>.Failure(result.ErrorMessage);
        }
    }
}
