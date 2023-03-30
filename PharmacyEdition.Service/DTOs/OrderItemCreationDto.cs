using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyEdition.Service.DTOs;

public class OrderItemCreationDto
{
    public long MedicineId { get; set; }
    public long OrderId { get; set; }
    public long Count { get; set; }
}
