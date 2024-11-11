using Application.DTOs;
using Application.Queries.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Property
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Result<IEnumerable<PropertyDto>>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<PropertyDto>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await propertyRepository.GetAllAsync();
            if (properties == null)
            {
                return Result<IEnumerable<PropertyDto>>.Failure("No properties found.");
            }

            var propertyDTOs = mapper.Map<IEnumerable<PropertyDto>>(properties);
            return Result<IEnumerable<PropertyDto>>.Success(propertyDTOs);
        }
    }
}
