using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Service.DTOs;

public class OrderCreationDto
{
    public long UserId { get; set; }
    public PaymentCreationDto Payment { get; set; }
    public ICollection<OrderItemCreationDto> OrderItems { get; set; }
}
