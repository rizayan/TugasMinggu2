using System.Text;
using Newtonsoft.Json;
using TMinggu2.Models;

namespace TMinggu2.Services
{
    public class StudentServices : IStudent
    {
        public Task AddStudentCourse(string name, string course)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7172/api/Student/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            List<Student> samurais = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Student> GetById(int id)
        {
            Student samurai = new Student();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/Student/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<IEnumerable<Student>> GetByName(string name)
        {
            List<Student> samurais = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7172/api/Student/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return samurais;
        }

        public Task<IEnumerable<CourseWithStudent>> GetCourseWithStudent()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentWithCourse>> GetStudentWithCourse()
        {
            List<StudentWithCourse> samurais = new List<StudentWithCourse>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Student/WithCourseWP"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<StudentWithCourse>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Student> Insert(Student obj)
        {
            Student samurai = new Student();
            using (var httpClient = new HttpClient())
            {
                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Student", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<Student> Update(Student obj)
        {
            Student samurai = await GetById(obj.ID);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.ID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:7172/api/Student", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return samurai;
        }
    }
}
