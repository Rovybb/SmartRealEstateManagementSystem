using Domain.Types.User;
using Domain.Utils;
using MediatR;

namespace Application.Commands.User
{
    public class CreateUserCommand : IRequest<Result<Guid>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }
}
