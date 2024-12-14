using Domain.Utils;
using MediatR;

public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
