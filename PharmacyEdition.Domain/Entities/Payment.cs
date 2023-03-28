using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities;

public class Payment : Auditable
{
    public PaymentType Type { get; set; }
    public bool IsPaid { get; set; }
    public long? CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; }
}