using Application.Repositories;
using Core.Security.Entities;

namespace Application.Services.UserService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByEmail(string email)
    {
        User? user = await _userRepository.GetAsync(i=>i.Email == email);
        return user;
    }
}
