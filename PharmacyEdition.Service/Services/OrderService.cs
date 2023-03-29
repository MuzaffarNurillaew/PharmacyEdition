using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;
using System.Collections.Generic;

namespace PharmacyEdition.Service.Services;

public class OrderService : IOrderService
{
    private IOrderRepository orderRepository = new OrderRepository();
    private IOrderItemService orderItemService = new OrderItemService();
    private IMedicineService medicineService = new MedicineService();
    private IPaymentService paymentService = new PaymentService();

    public async ValueTask<Response<Order>> AddAsync(OrderCreationDto model)
    {
        var payment = (await paymentService.AddAsync(model.Payment)).Value;

        foreach (var orderItem in model.OrderItems)
        {
            var medicine = (await medicineService.GetByIdAsync(orderItem.MedicineId)).Value;

            if (medicine is null)
                return new Response<Order>
                {
                    StatusCode = 404,
                    Message = "Medicine is not found"
                };

            if (medicine.Count < orderItem.Count)
                return new Response<Order>
                {
                    StatusCode = 407,
                    Message = "Medicine is not enough"
                };
        }

        var orderItems = new List<OrderItem>();

        foreach (var orderItem in model.OrderItems)
        {
            var createdItemResponse = await orderItemService.AddAsync(orderItem);

            orderItems.Add(createdItemResponse.Value);
        }

        var mappedEntity = new Order
        {
            CreatedAt = DateTime.UtcNow,
            PatmentId = payment.Id,
            Payment = payment,
            Status = StatusType.Pending,
            UserId = model.UserId,
            OrderItems = orderItems,
        };

        var insertedEntity = await orderRepository.InsertAsync(mappedEntity);

        return new Response<Order>
        {
            StatusCode = 200,
            Message = "Success",
            Value = insertedEntity
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = orderRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await orderRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<Order>>> GetAllAsync()
    {
        var entities = orderRepository.SelectAllAsync().ToList();

        return new Response<List<Order>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entities
        };
    }

    public async ValueTask<Response<Order>> GetByIdAsync(long id)
    {
        var entity = orderRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<Order>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<Order>
        {
            StatusCode = 200,
            Message = "Success",
            Value = entity
        };
    }

    public async ValueTask<Response<Order>> UpdateAsync(long id, OrderCreationDto model)
    {
        var existedEntity = orderRepository.SelectAsync(u => u.Id == id);

        if (existedEntity is null)
            return new Response<Order>
            {
                StatusCode = 404,
                Message = "Not found"
            };
        if (model.OrderItems is not null)
        {
            foreach (var orderItem in model.OrderItems)
            {
                var medicine = (await medicineService.GetByIdAsync(orderItem.MedicineId)).Value;

                if (medicine is null)
                    return new Response<Order>
                    {
                        StatusCode = 404,
                        Message = "Medicine is not found"
                    };

                if (medicine.Count < orderItem.Count)
                    return new Response<Order>
                    {
                        StatusCode = 407,
                        Message = "Medicine is not enough"
                    };
            }
        }

        if (model.Payment is not null)
        {
            var payment = (await paymentService.AddAsync(model.Payment)).Value;
            existedEntity.Payment = payment;
            existedEntity.PatmentId = payment.Id;
        }

        if (model.OrderItems is not null)
        {
            var orderItems = new List<OrderItem>();

            foreach (var orderItem in model.OrderItems)
            {
                var createdItemResponse = await orderItemService.AddAsync(orderItem);

                orderItems.Add(createdItemResponse.Value);
            }
            existedEntity.OrderItems = orderItems;
        }

        existedEntity.UpdatedAt = DateTime.UtcNow;
        existedEntity.UserId = model.UserId;

        var updatedEntity = await orderRepository.UpdateAsync(existedEntity.Id, existedEntity);

        return new Response<Order>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedEntity
        };
    }
}
