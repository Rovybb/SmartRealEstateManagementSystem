using Domain.Types.User;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; } // Hashed
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; } // Nullable for users who haven't logged in yet
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; } // Type of user (Client, Professional)

        // Fields specific to Professional accounts
        public string Company { get; set; } // Null for clients, filled for professionals

        public string Type { get; set; } // Specifies the type of user (e.g., Landlord, Real Estate Agent, etc.)
    }
}
