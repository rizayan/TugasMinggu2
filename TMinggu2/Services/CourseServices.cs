using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class CourseServices : ICourse
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7172/api/Course/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        
            public async Task<IEnumerable<Course>> GetAll()
            {
                List<Course> samurais = new List<Course>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7172/api/Course"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurais = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                    }
                }
                return samurais;
            }

        public async Task<IEnumerable<CourseWithStudent>> GetCourseWithStudent()
        {
            List<CourseWithStudent> samurais = new List<CourseWithStudent>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Course/WithStudent"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<CourseWithStudent>>(apiResponse);
                }
            }
            return samurais;
        }


        public async Task<Course> GetById(int id)
        {
            
           Course samurai = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/Course/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<IEnumerable<Course>> GetByName(string name)
        {
            List<Course> samurais = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/Course/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Course> Insert(Course obj)
        {
            Course samurai = new Course();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Course", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<Course> Update(Course obj)
        {
            Course samurai = await GetById(obj.Id);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.Id} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:7172/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return samurai;
        }

        public Task<IEnumerable<StudentWithCourse>> GetStudentWithCourse()
        {
            throw new NotImplementedException();
        }
    }
}
