using Domain.Entities;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserRepository repository;

    public RegisterUserCommandHandler(IUserRepository repository) => this.repository = repository;

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        return await repository.Register(user, cancellationToken);
    }
}
