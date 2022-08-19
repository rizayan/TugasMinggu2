using TMinggu2.Models;

namespace TMinggu2.Services
{
    public interface IUser
    {
        Task Registration(CreateUser user);
        Task<CreateUser> Registration1(CreateUser user);
        Task<Auth> Login (CreateUser user);
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
