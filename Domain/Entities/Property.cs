using Domain.Types.Property;

namespace Domain.Entities
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = "";
        public decimal Area { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int ConstructionYear { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
