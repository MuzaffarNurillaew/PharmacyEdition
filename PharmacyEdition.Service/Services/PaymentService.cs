using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;

namespace PharmacyEdition.Service.Services;

public class PaymentService : IPaymentService
{
    private IPaymentRepository paymentRepository = new PaymentRepository();

    public async ValueTask<Response<Payment>> AddAsync(PaymentCreationDto model)
    {
        var mappedEntity = new Payment
        {
            CreatedAt = DateTime.UtcNow,
            CreditCardId = model.CreditCardId,
            OrderId = model.OrderId,
            IsPaid = model.IsPaid,
            Type = model.Type
        };

        var insertedEntity = await paymentRepository.InsertAsync(mappedEntity);

        return new Response<Payment>
        {
            StatusCode = 200,
            Message = "Success",
            Value = insertedEntity
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = paymentRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await paymentRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<Payment>>> GetAllAsync()
    {
        var entities = paymentRepository.SelectAllAsync().ToList();

        return new Response<List<Payment>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entities
        };
    }

    public async ValueTask<Response<Payment>> GetByIdAsync(long id)
    {
        var entity = paymentRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<Payment>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<Payment>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entity
        };
    }

    public async ValueTask<Response<Payment>> UpdateAsync(long id, PaymentCreationDto model)
    {
        var existedEntity = paymentRepository.SelectAsync(u => u.Id == id);

        if (existedEntity is null)
            return new Response<Payment>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        existedEntity.UpdatedAt = DateTime.UtcNow;
        existedEntity.IsPaid = model.IsPaid;
        existedEntity.CreditCardId = model.CreditCardId;
        existedEntity.Type = model.Type;
        existedEntity.OrderId = model.OrderId;

        var updatedEntity = await paymentRepository.UpdateAsync(existedEntity.Id, existedEntity);

        return new Response<Payment>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedEntity
        };
    }
}