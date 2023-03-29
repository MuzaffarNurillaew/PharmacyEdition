using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IPaymentService
{
    ValueTask<Response<Payment>> AddAsync(PaymentCreationDto model);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<Payment>> UpdateAsync(long id, PaymentCreationDto model);
    ValueTask<Response<Payment>> GetByIdAsync(long id);
    ValueTask<Response<List<Payment>>> GetAllAsync();
}
