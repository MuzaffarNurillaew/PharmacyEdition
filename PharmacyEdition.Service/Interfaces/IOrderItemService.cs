using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;

namespace PharmacyEdition.Service.Interfaces;

public interface IOrderItemService
{
    ValueTask<Response<OrderItem>> AddAsync(OrderItemCreationDto model);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<OrderItem>> UpdateAsync(long id, OrderItemCreationDto model);
    ValueTask<Response<OrderItem>> GetByIdAsync(long id);
    ValueTask<Response<List<OrderItem>>> GetAllAsync();
}
