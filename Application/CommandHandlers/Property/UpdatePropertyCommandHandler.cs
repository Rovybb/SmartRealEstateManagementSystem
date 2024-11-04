using Application.Commands.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.Property
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Result<Guid>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var existingProperty = await propertyRepository.GetByIdAsync(request.Id);
            if (existingProperty == null)
            {
                return null;
            }

            mapper.Map(request, existingProperty);
            existingProperty.UpdatedAt = DateTime.UtcNow;

            var updateResult = await propertyRepository.UpdateAsync(existingProperty);
            if (updateResult.IsSuccess)
            {
                return Result<Guid>.Success(existingProperty.Id);
            }

            return Result<Guid>.Failure(updateResult.ErrorMessage);
        }
    }
}
