using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TodoListService.Interfaces;
using TodoListService.Models;
using TodoListService.Serializable.TodoLists;
using TodoListService.Serializable.TodoLists.Responses;
using TodoListService.Validators;

namespace TodoListService.Controllers;

[Route("api/todolists/")]
public class TodoListController : Controller
{
    private readonly ITodoListManager _todoListManager;
    private readonly IUserManager _userManager;

    public TodoListController(ITodoListManager todoListManager, IUserManager manager)
    {
        _todoListManager = todoListManager;
        _userManager = manager;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> HandleGetTodoListRequestAsync(GetTodoListRequest request, CancellationToken cToken)
    {
        try
        {
            var validator = new RequestValidators.GetTodoListRequestValidator();
            var validation = await validator.ValidateAsync(request, cToken);
            if (!validation.IsValid) return ValidationProblem();
            
            var todolist = await _todoListManager.GetTodoListAsync(request.Id, request.UserId, cToken);
            return Ok(new GetTodoListResponse { TodoList = todolist });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> HandleGetTodoListsRequestAsync(GetTodoListsForUserRequest request,  CancellationToken cToken)
    {
        try
        {
            var validator = new RequestValidators.GetTodoListsForUserRequestValidator();
            var validation = await validator.ValidateAsync(request, cToken);
            if (!validation.IsValid) return ValidationProblem();
        
            var (lists, pageNumber) = await _todoListManager.GetTodoListsAsync(request.RequestedByUserId, request.Page, request.ItemsPerPage, cToken);
            return Ok(new GetTodoListsResponse { TodoLists = lists, Page = pageNumber });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> HandleCreateOrUpdateTodoListRequestAsync(CreateOrUpdateTodoList request, CancellationToken cToken)
    {
        try
        {
            var validator = new RequestValidators.CreateOrUpdateTodoListValidator();
            var validation = await validator.ValidateAsync(request, cToken);
            if (!validation.IsValid) return ValidationProblem();
            
            var user = await GetUserAsync(request.RequestedByUserId);

            if (string.IsNullOrEmpty(request.Id))
                await _todoListManager.CreateAsync(new TodoListModel { Name = request.Name, CreatedBy = user, Tasks = request.Tasks }, cToken);
            else
            {
                await _todoListManager.UpdateTodoListAsync(
                    new TodoListModel { Id = request.Id, Name = request.Name, Tasks = request.Tasks }, request.RequestedByUserId, cToken);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("share")]
    public async Task<IActionResult> HandleShareTodoListRequestAsync(ShareTodoListRequest request, CancellationToken cToken)
    {
        try
        {
            var validator = new RequestValidators.ShareTodoListRequestValidator();
            var validation = await validator.ValidateAsync(request, cToken);
            if (!validation.IsValid) return ValidationProblem();

            if (request.IsRemove)
                await _todoListManager.RemoveUserFromShareListAsync(request.TodoListId, request.RequestedByUserId, request.TargetUserId, cToken);
            else
            {
                var shareWith = await GetUserAsync(request.TargetUserId);
                await _todoListManager.AddUserToShareListAsync(request.TodoListId, request.RequestedByUserId, shareWith, cToken);
            }
           
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> HandleDeleteTodoListRequestAsync(DeleteTodoListRequest request, CancellationToken cToken)
    {
        try
        {
            var validator = new RequestValidators.DeleteTodoListRequestValidator();
            var validation = await validator.ValidateAsync(request, cToken);
            if (!validation.IsValid) return ValidationProblem();
            
            await _todoListManager.RemoveAsync(request.Id, request.RequestedByUserId, cToken);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<UserModel> GetUserAsync(string userId, bool includeDeleted = false)
    {
            var user = await _userManager.GetUserAsync(userId, includeDeleted);
            if (user == null)
                throw new ValidationException($"UserId {userId} not found");
            
            return user;
    }
}