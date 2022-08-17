using TugasMinggu2.DTO;

namespace TugasMinggu2.DAL
{
    public interface IUser
    {
        Task Registration(CreateUserDTO user);
        Task<UserDTO> Authenticate(string username, string password);
        Task<IEnumerable<UserDTO>> GetAll();
    }
}
