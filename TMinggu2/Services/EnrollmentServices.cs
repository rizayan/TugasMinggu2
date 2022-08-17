using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class EnrollmentServices : IEnrollment
    {
        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            List<Enrollment> samurais = new List<Enrollment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Enrollment"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Enrollment>>(apiResponse);
                }
            }
            return samurais;
        }
        public async Task<Enrollment> Insert(Enrollment obj)
        {
            Enrollment samurai = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Enrollment", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return samurai;
        }
    }
}
