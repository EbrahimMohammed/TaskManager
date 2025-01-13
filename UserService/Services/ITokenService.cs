using UsersService.Models;

namespace UsersService.Services
{
    public interface ITokenService
    {
        public string CreateToken(User user);

    }
}
