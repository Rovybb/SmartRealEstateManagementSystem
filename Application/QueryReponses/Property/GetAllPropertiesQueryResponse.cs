using Application.DTOs;
using Domain.Utils;

namespace Application.QueryReponses.Property
{
    public class GetAllPropertiesQueryResponse
    {
        PaginatedList<PropertyDto> Items { get; set; }
        int TotalCount { get; set; }
    }
}
