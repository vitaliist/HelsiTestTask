using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TodoListService.Validators;

namespace TodoListService.Serializable.Users;

public class CreateUserRequest : RequestValidators.CreateUserRequestValidator
{
    [Required]
    public string Name { get; set; }
}
