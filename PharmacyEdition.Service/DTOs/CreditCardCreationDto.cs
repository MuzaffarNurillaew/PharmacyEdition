using PharmacyEdition.Domain.Entities;

namespace PharmacyEdition.Service.DTOs;

public class CreditCardCreationDto
{
    public string CardNumber { get; set; }
    public long UserId { get; set; }
}
