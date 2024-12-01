using Application.DTOs;
using Application.Queries.Property;
using Application.QueryReponses.Property;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Property
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Result<PaginatedList<GetAllPropertiesQueryResponse>>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PaginatedList<GetAllPropertiesQueryResponse>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var result = await propertyRepository.GetPropertiesAsync(request.PageNumber, request.PageSize, request.Filters);

            if (result.Items == null)
            {
                return Result<PaginatedList<PropertyDto>>.Failure("No properties found.");
            }

            var paginatedProperties = mapper.Map<PaginatedList<PropertyDto>>(result);
            return Result<PaginatedList<PropertyDto>>.Success(paginatedProperties);
        }
    }

}
