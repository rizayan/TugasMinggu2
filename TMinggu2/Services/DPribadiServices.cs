using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class DPribadiServices : IDPribadi
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7172/api/DPribadi/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<DPribadi>> GetAll()
        {
            List<DPribadi> samurais = new List<DPribadi>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/DPribadi"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<DPribadi>>(apiResponse);
                }
            }
            return samurais;
        }
        

        public async Task<DPribadi> GetById(int id)
        {
            DPribadi samurai = new DPribadi();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/DPribadi/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<DPribadi>(apiResponse);
                    }
                }
            }
            return samurai;
        }


        public async Task<IEnumerable<DPribadi>> GetByName(string name)
        {
            List<DPribadi> samurais = new List<DPribadi>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/DPribadi/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<DPribadi>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<DPribadi> GetByNIK(int nik)
        {
            DPribadi samurai = new DPribadi();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/DPribadi/n/{nik}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<DPribadi>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<DPribadi> Insert(DPribadi obj)
        {
            DPribadi samurai = new DPribadi();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/DPribadi", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<DPribadi>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<DPribadi> Update(DPribadi obj)
        {
            DPribadi samurai = await GetById(obj.Id);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.Id} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:7172/api/DPribadi", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<DPribadi>(apiResponse);
                }
            }
            return samurai;
        }

       
    }
}
