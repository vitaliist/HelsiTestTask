using Microsoft.AspNetCore.Mvc;
using TodoListService.Interfaces;
using TodoListService.Models;
using TodoListService.Serializable.Users;
using TodoListService.Validators;

namespace TodoListService.Controllers;

[Route("api/users/")]
public class UserController : Controller
{
    private readonly IUserManager _userManager;
    
    public UserController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult?> HandleGetUserAsync(string userId)
    {
        var user =  await _userManager.GetUserAsync(userId);

        if (user == null) NotFound();
        
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> HandleUserCreateAsync(CreateUserRequest request)
    {
        try
        {
            var validator = new RequestValidators.CreateUserRequestValidator();
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid) return ValidationProblem();

            await _userManager.CreateUserAsync(new UserModel { Name = request.Name });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> HandleUserDeleteAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            await _userManager.RemoveUserAsync(userId, cancellationToken);
            return Ok();
        }   
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}