using Domain.Types.User;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; } // Hashed
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Nationality { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; } // Nullable for users who haven't logged in yet
        public required UserStatus Status { get; set; }
        public required UserRole Role { get; set; } // Type of user (Client, Professional)

        // Fields specific to Professional accounts
        public string? Company { get; set; } // Null for clients, filled for professionals

        public string? Type { get; set; } // Specifies the type of user (e.g., Landlord, Real Estate Agent, etc.)
        public IEnumerable<Property>? Properties { get; set; } // Properties owned by the user
        public IEnumerable<Inquiry>? Inquiries { get; set; } // Inquiries made by the user
        public IEnumerable<Payment>? Payments { get; set; } // Payments made by the user
    }
}
