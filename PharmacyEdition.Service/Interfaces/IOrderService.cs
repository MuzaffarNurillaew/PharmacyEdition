using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IOrderService
{
    ValueTask<Response<Order>> AddAsync(OrderCreationDto model);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<Order>> UpdateAsync(long id, OrderCreationDto model);
    ValueTask<Response<Order>> GetByIdAsync(long id);
    ValueTask<Response<List<Order>>> GetAllAsync();
}
