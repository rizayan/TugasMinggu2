using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class UserServices : IUser
    {
        public Task<User> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        /* public async Task<User> Authenticate(string username, string password)
{
    User samurai = new User();
    samurai.Username = username;
    samurai.Password = password;
    using (var httpClient = new HttpClient())
    {
        StringContent content =
            new StringContent(JsonConvert.SerializeObject(samurai), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PostAsync("https://localhost:7172/api/User/Login", content))
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                samurai = JsonConvert.DeserializeObject<User>(apiResponse);
            }
        }
    }
    return samurai;
}*/

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Auth> Login(CreateUser user)
        {
            Auth authentication = new Auth();
            CreateUser newUser = new CreateUser();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/User/Login", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        authentication = JsonConvert.DeserializeObject<Auth>(apiResponse);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error : {apiResponse}");
                    }
                }
            }
            return authentication;
        }

        public async Task Registration(CreateUser user)
        {
            CreateUser samurai = new CreateUser();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/User", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<CreateUser>(apiResponse);
                    }
                }
            }
            
        }

        public async Task<CreateUser> Registration1(CreateUser user)
        {
            CreateUser samurai = new CreateUser();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/User/RegisTest", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<CreateUser>(apiResponse);
                    }
                }
            }
            return samurai;
        }
    }
}
