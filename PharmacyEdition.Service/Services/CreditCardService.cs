using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;

namespace PharmacyEdition.Service.Services;

public class CreditCardService : ICreditCardService
{
    private ICreditCardRepository creditCardRepository = new CreditCardRepository();

    public async ValueTask<Response<CreditCard>> AddAsync(CreditCardCreationDto model)
    {
        var existingEntity = creditCardRepository.SelectAllAsync()
            .Where(u => u.CardNumber == model.CardNumber).FirstOrDefault();

        if (existingEntity is not null)
            return new Response<CreditCard>
            {
                StatusCode = 400,
                Message = "This credit card exists"
            };

        var mappedEntity = new CreditCard
        {
            CreatedAt = DateTime.UtcNow,
            CardNumber = model.CardNumber,
            UserId = model.UserId
        };

        var insertedEntity = await creditCardRepository.InsertAsync(mappedEntity);

        return new Response<CreditCard>
        {
            StatusCode = 200,
            Message = "Success",
            Value = insertedEntity
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = creditCardRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await creditCardRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<CreditCard>>> GetAllAsync()
    {
        var entities = creditCardRepository.SelectAllAsync().ToList();

        return new Response<List<CreditCard>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entities
        };
    }

    public async ValueTask<Response<CreditCard>> GetByIdAsync(long id)
    {
        var entity = creditCardRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<CreditCard>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<CreditCard>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entity
        };
    }

    public async ValueTask<Response<CreditCard>> UpdateAsync(long id, CreditCardCreationDto model)
    {
        var existedEntity = creditCardRepository.SelectAsync(u => u.Id == id);

        if (existedEntity is null)
            return new Response<CreditCard>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        existedEntity.UpdatedAt = DateTime.UtcNow;
        existedEntity.CardNumber = model.CardNumber;
        existedEntity.UserId = model.UserId;

        var updatedEntity = await creditCardRepository.UpdateAsync(existedEntity.Id, existedEntity);

        return new Response<CreditCard>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedEntity
        };
    }
}
