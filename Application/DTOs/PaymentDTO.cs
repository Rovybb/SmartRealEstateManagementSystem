using Domain.Types.Payment;

namespace Application.DTOs
{
    public class PaymentDTO
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
