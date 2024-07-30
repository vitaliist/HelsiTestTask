using FluentValidation;
using TodoListService.Serializable.TodoLists;
using TodoListService.Serializable.Users;

namespace TodoListService.Validators;

public class RequestValidators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }

    public class GetTodoListRequestValidator : AbstractValidator<GetTodoListRequest>
    {
        public GetTodoListRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
    
    public class GetTodoListsForUserRequestValidator : AbstractValidator<GetTodoListsForUserRequest>
    {
        public GetTodoListsForUserRequestValidator()
        {
            RuleFor(x => x.RequestedByUserId).NotNull().NotEmpty();
        }
    }

    public class CreateOrUpdateTodoListValidator : AbstractValidator<CreateOrUpdateTodoList>
    {
        public CreateOrUpdateTodoListValidator()
        {
            RuleFor(x => x.RequestedByUserId).NotNull().NotEmpty();
        }
    }

    public class ShareTodoListRequestValidator : AbstractValidator<ShareTodoListRequest>
    {
        public ShareTodoListRequestValidator()
        {
            RuleFor(x => x.RequestedByUserId).NotNull().NotEmpty();
            RuleFor(x => x.TodoListId).NotNull().NotEmpty();
            RuleFor(x => x.TargetUserId).NotNull().NotEmpty();
        }
    }

    public class DeleteTodoListRequestValidator : AbstractValidator<DeleteTodoListRequest>
    {
        public DeleteTodoListRequestValidator()
        {
            RuleFor(x => x.RequestedByUserId).NotNull().NotEmpty();
        }
    }
}