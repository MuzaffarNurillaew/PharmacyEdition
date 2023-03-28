using PharmacyEdition.Domain.Commons;
using PharmacyEdition.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyEdition.Domain.Entities
{
    public class OrderItem : Auditable
    {
        public long MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long Count { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

    }
}
