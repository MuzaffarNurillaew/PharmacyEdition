using Microsoft.AspNetCore.Mvc;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Interfaces;
using PharmacyEdition.Service.Services;

namespace PharmacyEdition.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService userService = new UserService();


    public async Task<IActionResult> Index()
    {
        var users = await this.userService.GetAllAsync();
        return View(users.Value);
    }

    public async Task<IActionResult> Create(UserCreationDto model)
    {
        var createdUser = await this.userService.AddAsync(model);
        return View(createdUser.Value);
    }
}
