using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Helpers;
using PharmacyEdition.Service.Interfaces;
using PharmacyEditon.Data.IRepositories;
using PharmacyEditon.Data.Repositories;

namespace PharmacyEdition.Service.Services;

public class UserService : IUserService
{
    private IUserRepository userRepository = new UserRepository();

    public async ValueTask<Response<UserDto>> AddAsync(UserCreationDto model)
    {
        var existingEntity = userRepository.SelectAllAsync()
            .Where(u => u.Phone == model.Phone).FirstOrDefault();

        if (existingEntity is not null)
            return new Response<UserDto>
            {
                StatusCode = 400,
                Message = "The account has opened with this phone number"
            };

        var mappedEntity = new User
        {
            CreatedAt = DateTime.UtcNow,
            Phone = model.Phone,
            Age = model.Age,
            LastName = model.LastName,
            Password = model.Password,
            FirstName = model.FirstName
        };

        var insertedEntity = await userRepository.InsertAsync(mappedEntity);

        var mappedDto = new UserDto
        {
            Age = insertedEntity.Age,
            LastName = insertedEntity.LastName,
            FirstName = insertedEntity.FirstName,
            Id = insertedEntity.Id,
            CreatedAt = insertedEntity.CreatedAt,
            UpdatedAt = insertedEntity.UpdatedAt,
            Phone = insertedEntity.Phone
        };

        return new Response<UserDto>
        {
            StatusCode = 200,
            Message = "Success",
            Value = mappedDto
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var existingEntity = userRepository.SelectAsync(u => u.Id == id);

        if (existingEntity is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found",
                Value = false
            };

        await userRepository.DeleteAsync(u => u.Id == id);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async ValueTask<Response<List<UserDto>>> GetAllAsync()
    {
        var entities = userRepository.SelectAllAsync().ToList();
        var modelDtos = new List<UserDto>();

        foreach (var item in entities)
        {
            modelDtos.Add(new UserDto
            {
                Age = item.Age,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Id = item.Id,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                Phone = item.Phone
            });
        }

        return new Response<List<UserDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = modelDtos
        };
    }

    public async ValueTask<Response<UserDto>> GetByIdAsync(long id)
    {
        var entity = userRepository.SelectAsync(u => u.Id == id);

        if (entity is null)
            return new Response<UserDto>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        var mappedDto = new UserDto
        {
            Age = entity.Age,
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Phone = entity.Phone
        };

        return new Response<UserDto>
        {
            StatusCode = 200,
            Message = "Success",
            Value = mappedDto
        };
    }

    public async ValueTask<Response<UserDto>> LoginAsync(string phone, string password)
    {
        var entity = userRepository.SelectAllAsync()
            .Where(u => u.Phone == phone && u.Password == password).FirstOrDefault();

        if (entity is null)
            return new Response<UserDto>
            {
                StatusCode = 400,
                Message = "Phone number or password was incorrect"
            };

        var mappedDto = new UserDto
        {
            Age = entity.Age,
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Phone = entity.Phone
        };

        return new Response<UserDto>
        {
            StatusCode = 200,
            Message = "Success",
            Value = mappedDto
        };
    }

    public async ValueTask<Response<UserDto>> UpdateAsync(long id, UserCreationDto model)
    {
        var existedEntity = userRepository.SelectAsync(u=> u.Id == id);

        if (existedEntity is null)
            return new Response<UserDto>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        existedEntity.Phone = model.Phone;
        existedEntity.Password = model.Password;
        existedEntity.Age = model.Age;
        existedEntity.LastName = model.LastName;
        existedEntity.FirstName = model.FirstName;
        existedEntity.UpdatedAt = DateTime.UtcNow;


        var updatedEntity = await userRepository.UpdateAsync(existedEntity.Id, existedEntity);

        var mappedDto = new UserDto
        {
            Age = updatedEntity.Age,
            LastName = updatedEntity.LastName,
            FirstName = updatedEntity.FirstName,
            Id = updatedEntity.Id,
            CreatedAt = updatedEntity.CreatedAt,
            UpdatedAt = updatedEntity.UpdatedAt,
            Phone = updatedEntity.Phone
        };

        return new Response<UserDto>
        {
            StatusCode = 200,
            Message = "Success",
            Value = mappedDto
        };
    }
}
