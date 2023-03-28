using PharmacyEdition.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyEdition.Models;

public class Medicine : Auditable
{
    public string Name { get; set; }
    public int Count { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public DateTime Expiration { get; set; } = DateTime.Now.AddYears(1);
    public string Description { get; set; }
}
