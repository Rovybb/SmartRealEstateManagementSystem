using Application.Commands.Payment;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.Payment
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Result>
    {
        private readonly IPaymentRepository paymentRepository;

        public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var result = await paymentRepository.DeleteAsync(request.Id);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.ErrorMessage);
        }
    }
}
