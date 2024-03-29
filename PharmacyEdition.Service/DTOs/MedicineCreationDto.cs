﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyEdition.Service.DTOs;

public class MedicineCreationDto
{
    public string Name { get; set; }
    public long Count { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}