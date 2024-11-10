using Application.Commands.Property;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.Property
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Result<Guid>>
    {
        private readonly IPropertyRepository propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<Result<Guid>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyResult = await propertyRepository.GetByIdAsync(request.Id);
            if (!propertyResult.IsSuccess)
            {
                return Result<Guid>.Failure(propertyResult.ErrorMessage);
            }

            await propertyRepository.DeleteAsync(request.Id);
            return Result<Guid>.Success(request.Id);
        }
    }
}