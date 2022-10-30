using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class EnrollmentServices : IEnrollment
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7172/api/Enrollment"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

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



        public async Task<Enrollment> GetById(int id)
        {
            Enrollment samurai = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/Enrollment/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return samurai;
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

        public async Task<Enrollment> Update(Enrollment obj)
        {
            Enrollment samurai = await GetById(obj.EnrollmentID);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.EnrollmentID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:7172/api/Enrollment", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                }
            }
            return samurai;
        }
    }
}
