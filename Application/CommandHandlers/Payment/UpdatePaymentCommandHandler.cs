using Application.Commands.Payment;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.Payment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Result<Guid>>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var existingPayment = await paymentRepository.GetByIdAsync(request.Id);
            if (!existingPayment.IsSuccess)
            {
                return Result<Guid>.Failure("Payment not found.");
            }

            mapper.Map(request, existingPayment.Data);

            var updateResult = await paymentRepository.UpdateAsync(existingPayment.Data);
            if (updateResult.IsSuccess)
            {
                return Result<Guid>.Success(existingPayment.Data.Id);
            }
            return Result<Guid>.Failure(updateResult.ErrorMessage);


        }
    }
}
