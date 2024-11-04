using Domain.Types.Payment;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public PaymentType Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
