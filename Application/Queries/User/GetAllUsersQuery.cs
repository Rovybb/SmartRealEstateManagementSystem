using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.Queries.User
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<UserDTO>>>
    {
    }
}
