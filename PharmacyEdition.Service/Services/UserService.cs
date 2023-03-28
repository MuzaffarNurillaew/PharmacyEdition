//////using PharmacyEdition.Domain.Entities;
//////using PharmacyEdition.Service.DTOs;
//////using PharmacyEdition.Service.Helpers;
//////using PharmacyEdition.Service.Interfaces;
//////using PharmacyEditon.Data.IRepositories;
//////using PharmacyEditon.Data.Repositories;

//////namespace PharmacyEdition.Service.Services;

//////public class UserService : IUserService
//////{
//////    private IUserRepository userRepository = new UserRepository();

//////    public async ValueTask<Response<UserDto>> AddAsync(UserCreationDto model)
//////    {
//////        var existingEntity = userRepository.SelectAllAsync()
//////            .Where(u => u.Phone == model.Phone).FirstOrDefault();

//////        if (existingEntity is not null)
//////            return new Response<UserDto>
//////            {
//////                StatusCode = 400,
//////                Message = "The account has opened with this phone number"
//////            };

//////        var mappedEntity = new User
//////        {
//////            CreatedAt = DateTime.UtcNow,
//////            Phone = model.Phone,
//////            Age = model.Age,
//////            LastName = model.LastName,
//////            Password = model.Password,
//////            FirstName = model.FirstName
//////        };

//////        var insertedEntity = await userRepository.InsertAsync(mappedEntity);

//////        var mappedDto = new UserDto
//////        {
//////            Age = insertedEntity.Age,
//////            LastName = insertedEntity.LastName,
//////            FirstName = insertedEntity.FirstName,
//////            Id = insertedEntity.Id,
//////            CreatedAt = insertedEntity.CreatedAt,
//////            UpdatedAt = insertedEntity.UpdatedAt,
//////            Phone = insertedEntity.Phone
//////        };

//////        return new Response<UserDto>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = mappedDto
//////        };
//////    }

//////    public async ValueTask<Response<bool>> DeleteAsync(long id)
//////    {
//////        var existingEntity = await userRepository.SelectAsync(u => u.Id == id);

//////        if (existingEntity is null)
//////            return new Response<bool>
//////            {
//////                StatusCode = 404,
//////                Message = "Not found",
//////                Value = false
//////            };

//////        await userRepository.DeleteAsync(u => u.Id == id);

//////        return new Response<bool>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = true
//////        };
//////    }

//////    public async ValueTask<Response<List<UserDto>>> GetAllAsync()
//////    {
//////        var entities = userRepository.SelectAllAsync().ToList();
//////        var modelDtos = new List<UserDto>();

//////        foreach (var item in entities)
//////        {
//////            modelDtos.Add(new UserDto
//////            {
//////                Age = item.Age,
//////                LastName = item.LastName,
//////                FirstName = item.FirstName,
//////                Id = item.Id,
//////                CreatedAt = item.CreatedAt,
//////                UpdatedAt = item.UpdatedAt,
//////                Phone = item.Phone
//////            });
//////        }

//////        return new Response<List<UserDto>>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = modelDtos
//////        };
//////    }

//////    public async ValueTask<Response<UserDto>> GetByIdAsync(long id)
//////    {
//////        var entity = await userRepository.SelectAsync(u=> u.Id == id);

//////        if (entity is null)
//////            return new Response<UserDto>
//////            {
//////                StatusCode = 404,
//////                Message = "Not found"
//////            };

//////        var mappedDto = new UserDto
//////        {

//////        };

//////        return new Response<UserDto>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = mappedDto
//////        };
//////    }

//////    public async ValueTask<Response<UserDto>> LoginAsync(string username, string password)
//////    {
//////        var entity = userRepository.SelectAllAsync()
//////            .Where(u => u.Username == username && u.Password == password).ToList().FirstOrDefault();

//////        if (entity is null)
//////            return new Response<UserDto>
//////            {
//////                StatusCode = 400,
//////                Message = "Username or password was incorrect"
//////            };

//////        var mappedDto = new UserDto
//////        {
//////            Username = username,
//////            Bio = entity.Bio,
//////            CreatedAt = entity.CreatedAt,
//////            UpdatedAt = entity.UpdatedAt,
//////            Fullname = entity.Fullname,
//////            Role = entity.Role,
//////            Id = entity.Id
//////        };

//////        return new Response<UserDto>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = mappedDto
//////        };
//////    }

//////    public async ValueTask<Response<UserDto>> UpdateAsync(long id, UserCreationDto model)
//////    {
//////        var existedEntity = await userRepository.SelectAsync(id);

//////        if (existedEntity is null)
//////            return new Response<UserDto>
//////            {
//////                StatusCode = 404,
//////                Message = "Not found"
//////            };

//////        existedEntity.Username = model.Username;
//////        existedEntity.Bio = model.Bio;
//////        existedEntity.Password = model.Password;
//////        existedEntity.Fullname = model.Fullname;
//////        existedEntity.UpdatedAt = DateTime.Now;

//////        var updatedEntity = await userRepository.UpdateAsync(existedEntity);

//////        var mappedDto = new UserDto
//////        {
//////            Username = updatedEntity.Username,
//////            Bio = updatedEntity.Bio,
//////            CreatedAt = updatedEntity.CreatedAt,
//////            UpdatedAt = updatedEntity.UpdatedAt,
//////            Role = updatedEntity.Role,
//////            Fullname = updatedEntity.Fullname,
//////            Id = updatedEntity.Id
//////        };

//////        return new Response<UserDto>
//////        {
//////            StatusCode = 200,
//////            Message = "Success",
//////            Value = mappedDto
//////        };
//////    }
//////}
