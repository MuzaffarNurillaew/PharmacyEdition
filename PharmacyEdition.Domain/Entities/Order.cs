using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities;

public class Order : Auditable
{
    public StatusType Status { get; set; } = StatusType.Pending;

    public Payment Payment { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}