using TugasMinggu2.DTO;

namespace TugasMinggu2.DAL
{
    public interface IUser
    {
        Task Registration(CreateUserDTO user);
        Task <CreateUserDTO> Registration1(CreateUserDTO user);
        Task<UserDTO> Authenticate(string username, string password);
        Task<IEnumerable<UserDTO>> GetAll();
    }
}
