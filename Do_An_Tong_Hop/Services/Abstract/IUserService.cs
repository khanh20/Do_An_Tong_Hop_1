using API.Dtos.Users;

namespace API.Services.Abstract
{
    public interface IUserService
    {
        void Create(CreateUserDto input);
        string Login(LoginDto input);

    }
}
