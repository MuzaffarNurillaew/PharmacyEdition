using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;

namespace PharmacyEdition.Service.Services;

public class MedicineService : IMedicineService
{
    private IMedicineRepository medicineRepository = new MedicineRepository();

    public async ValueTask<Response<Medicine>> AddAsync(MedicineCreationDto model)
    {
        var existingEntity = medicineRepository.SelectAllAsync()
            .Where(u => u.Name == model.Name).FirstOrDefault();

        if (existingEntity is not null)
            return new Response<Medicine>
            {
                StatusCode = 400,
                Message = "The medicine with this name exists"
            };

        var mappedEntity = new Medicine
        {
            CreatedAt = DateTime.UtcNow,
            Count = model.Count,
            Description = model.Description,
            Name = model.Name,
            Price = model.Price
        };

        var insertedEntity = await medicineRepository.InsertAsync(mappedEntity);

        return new Response<Medicine>
        {
            StatusCode = 200,
            Message = "Success",
            Value = insertedEntity
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = medicineRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await medicineRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<Medicine>>> GetAllAsync()
    {
        var entities = medicineRepository.SelectAllAsync().ToList();

        return new Response<List<Medicine>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entities
        };
    }

    public async ValueTask<Response<Medicine>> GetByIdAsync(long id)
    {
        var entity = medicineRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<Medicine>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<Medicine>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entity
        };
    }

    public async ValueTask<Response<Medicine>> UpdateAsync(long id, MedicineCreationDto model)
    {
        var existedEntity = medicineRepository.SelectAsync(u => u.Id == id);

        if (existedEntity is null)
            return new Response<Medicine>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        existedEntity.UpdatedAt = DateTime.UtcNow;
        existedEntity.Price = model.Price;
        existedEntity.Count = model.Count;
        existedEntity.Description = model.Description;
        existedEntity.Name = model.Name;

        var updatedEntity = await medicineRepository.UpdateAsync(existedEntity.Id, existedEntity);

        return new Response<Medicine>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedEntity
        };
    }
}