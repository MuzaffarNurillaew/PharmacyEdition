using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Domain.Enums;

namespace PharmacyEdition.Domain.Entities
{
    public class CreditCard : Auditable
    {
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public DateTime Expiration { get; set; } = DateTime.Now.AddMonths(6);

        public Payment Payment { get; set; }
    }
}
