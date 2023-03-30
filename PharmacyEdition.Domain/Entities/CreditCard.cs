using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities
{
    public class CreditCard : Auditable
    {
        public string CardNumber { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime Expiration { get; set; } = DateTime.Now.AddMonths(6);
        public IEnumerable<Payment> Payments { get; set; }
    }
}