using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities;

public class Order : Auditable
{
    public StatusType Status { get; set; } = StatusType.Pending;
    public long UserId { get; set; }
    public User User { get; set; }
    public long PatmentId { get; set; }
    public Payment Payment { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}