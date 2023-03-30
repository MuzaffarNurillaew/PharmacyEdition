using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface ICreditCardService
{
    ValueTask<Response<CreditCard>> AddAsync(CreditCardCreationDto model);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<CreditCard>> UpdateAsync(long id, CreditCardCreationDto model);
    ValueTask<Response<CreditCard>> GetByIdAsync(long id);
    ValueTask<Response<List<CreditCard>>> GetAllAsync();
}
