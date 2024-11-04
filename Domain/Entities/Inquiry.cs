using Domain.Types.Inquiry;

namespace Domain.Entities
{
    public class Inquiry
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; } 
        public Guid ClientId { get; set; } 
        public Guid AgentId { get; set; } 
        public string Message { get; set; }
        public InquiryStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
