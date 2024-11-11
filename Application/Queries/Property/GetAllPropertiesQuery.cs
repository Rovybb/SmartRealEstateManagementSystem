using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.Property
{
    public class GetAllPropertiesQuery : IRequest<Result<IEnumerable<PropertyDto>>>
    {
    }
}
