using Domain.Utils;
using MediatR;

namespace Application.Commands.User
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
