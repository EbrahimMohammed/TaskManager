using UsersService.Models;

namespace UsersService.Data
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<bool> EmailExists(string email);

        Task<User> GetUserByEmail(string email);

        Task Save();
    }
}
