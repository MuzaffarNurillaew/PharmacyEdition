using PharmacyEdition.Domain.Entities;

namespace PharmacyEdition.Service.DTOs;

public class OrderCreationDto
{
    public ICollection<OrderItem> OrderItems { get; set; }
    public PaymentCreationDto Payment { get; set; }
}
