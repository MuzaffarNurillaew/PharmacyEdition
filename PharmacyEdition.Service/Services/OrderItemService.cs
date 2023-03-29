using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;
using System.Runtime.InteropServices;

namespace PharmacyEdition.Service.Services;

public class OrderItemService : IOrderItemService
{
    private IOrderItemRepository orderItemRepository = new OrderItemRepository();
    private IMedicineService medicineService = new MedicineService();


    public async ValueTask<Response<OrderItem>> AddAsync(OrderItemCreationDto model)
    {
        var medicine = (await medicineService.GetByIdAsync(model.MedicineId)).Value;

        if (medicine is null)
            return new Response<OrderItem>
            {
                StatusCode = 404,
                Message = "Medicine is not found"
            };

        if (medicine.Count < model.Count)
            return new Response<OrderItem>
            {
                StatusCode = 407,
                Message = "Medicine is not enough"
            };

        var totalPrice = model.Count * medicine.Price;

        medicine.Count -= model.Count;

        var mappedMedicine = new MedicineCreationDto
        {
            Count = medicine.Count,
            Description = medicine.Description,
            Name = medicine.Name,
            Price = medicine.Price
        };

        await medicineService.UpdateAsync(medicine.Id, mappedMedicine);

        var mappedEntity = new OrderItem
        {
            CreatedAt = DateTime.UtcNow,
            Count = model.Count,
            MedicineId = model.MedicineId,
            OrderId = model.OrderId,
            TotalPrice = totalPrice
        };

        var insertedEntity = await orderItemRepository.InsertAsync(mappedEntity);

        return new Response<OrderItem>
        {
            StatusCode = 200,
            Message = "Success",
            Value = insertedEntity
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = orderItemRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await orderItemRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<OrderItem>>> GetAllAsync()
    {
        var entities = orderItemRepository.SelectAllAsync().ToList();

        return new Response<List<OrderItem>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entities
        };
    }

    public async ValueTask<Response<OrderItem>> GetByIdAsync(long id)
    {
        var entity = orderItemRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<OrderItem>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<OrderItem>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entity
        };
    }

    public async ValueTask<Response<OrderItem>> UpdateAsync(long id, OrderItemCreationDto model)
    {
        var existedEntity = orderItemRepository.SelectAsync(u => u.Id == id);

        if (existedEntity is null)
            return new Response<OrderItem>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        var medicine = (await medicineService.GetByIdAsync(model.MedicineId)).Value;

        if (medicine is null)
            return new Response<OrderItem>
            {
                StatusCode = 404,
                Message = "Medicine is not found"
            };

        if (medicine.Count < model.Count)
            return new Response<OrderItem>
            {
                StatusCode = 407,
                Message = "Medicine is not enough"
            };

        var totalPrice = model.Count * medicine.Price;

        medicine.Count -= model.Count;
        var mappedMedicine = new MedicineCreationDto
        {
            Count = medicine.Count,
            Description = medicine.Description,
            Name = medicine.Name,
            Price = medicine.Price
        };
        await medicineService.UpdateAsync(medicine.Id, mappedMedicine);

        existedEntity.UpdatedAt = DateTime.UtcNow;
        existedEntity.Count = model.Count;
        existedEntity.MedicineId = model.MedicineId;
        existedEntity.OrderId = model.OrderId;
        existedEntity.TotalPrice = totalPrice;

        var updatedEntity = await orderItemRepository.UpdateAsync(existedEntity.Id, existedEntity);

        return new Response<OrderItem>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedEntity
        };
    }
}
