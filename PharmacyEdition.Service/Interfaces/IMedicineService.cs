using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IMedicineService
{
    ValueTask<Response<Medicine>> AddAsync(MedicineCreationDto model);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<Medicine>> UpdateAsync(long id, MedicineCreationDto model);
    ValueTask<Response<Medicine>> GetByIdAsync(long id);
    ValueTask<Response<List<Medicine>>> GetAllAsync();
}
