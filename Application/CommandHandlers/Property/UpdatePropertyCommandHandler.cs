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

        public async Task<Result<Guid>?> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CreatedBy: {request.CreatedBy}, CreatedAt: {request.CreatedAt}, UpdatedAt: {request.UpdatedAt}");

            var existingProperty = await propertyRepository.GetByIdAsync(request.Id);
            if (existingProperty == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(request.Title))
                existingProperty.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description))
                existingProperty.Description = request.Description;

            if (request.Type.HasValue)
                existingProperty.Type = request.Type.Value;

            if (request.Status.HasValue)
                existingProperty.Status = request.Status.Value;

            if (request.Price.HasValue)
                existingProperty.Price = request.Price.Value;

            if (!string.IsNullOrEmpty(request.Address))
                existingProperty.Address = request.Address;

            if (request.Area.HasValue)
                existingProperty.Area = request.Area.Value;

            if (request.Rooms.HasValue)
                existingProperty.Rooms = request.Rooms.Value;

            if (request.Bathrooms.HasValue)
                existingProperty.Bathrooms = request.Bathrooms.Value;

            if (request.ConstructionYear.HasValue)
                existingProperty.ConstructionYear = request.ConstructionYear.Value;

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
