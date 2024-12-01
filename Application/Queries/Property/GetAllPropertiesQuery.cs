using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.Property
{
    public class GetAllPropertiesQuery : IRequest<Result<PaginatedList<PropertyDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Dictionary<string, string>? Filters { get; set; } // Key: Field, Value: Value
    }

}
