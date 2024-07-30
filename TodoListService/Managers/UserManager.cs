using TodoListService.Adapters;
using TodoListService.Interfaces;
using TodoListService.Models;

namespace TodoListService.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel?> GetUserAsync(string userId, bool includeDeleted = false, CancellationToken cToken = default)
    {
        var user = await _userRepository.GetUserAsync(userId, includeDeleted, cToken);
        return user?.ToModel();
    }

    public async Task CreateUserAsync(UserModel model, CancellationToken cancellationToken = default)
    {
        var entity = model.ToEntity();
        await _userRepository.CreateAsync(entity, cancellationToken);
    }

    public async Task RemoveUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        await _userRepository.RemoveAsync(userId, cancellationToken);
    }
}