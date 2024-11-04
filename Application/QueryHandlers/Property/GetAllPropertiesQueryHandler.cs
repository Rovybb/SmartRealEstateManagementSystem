using Application.DTOs;
using Application.Queries.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Property
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Result<IEnumerable<PropertyDTO>>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<PropertyDTO>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await propertyRepository.GetAllAsync();
            if (properties == null)
            {
                return Result<IEnumerable<PropertyDTO>>.Failure("No properties found.");
            }

            var propertyDTOs = mapper.Map<IEnumerable<PropertyDTO>>(properties);
            return Result<IEnumerable<PropertyDTO>>.Success(propertyDTOs);
        }
    }
}
