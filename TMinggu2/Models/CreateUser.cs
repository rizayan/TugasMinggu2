using System.ComponentModel.DataAnnotations;

namespace TMinggu2.Models
{
    public class CreateUser
    {
       
        public string Username { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
    }
}
