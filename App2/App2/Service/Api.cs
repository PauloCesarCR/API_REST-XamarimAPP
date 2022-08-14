using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;

namespace App2.Service
{
    public class Api
    {

        const string URL = "http://192.168.0.108:8081/api/Users/";


        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");
            return client;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            HttpClient client = GetClient();

            var response = await client.GetAsync(URL);

            if (response.IsSuccessStatusCode)
            {

                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserModel>>(content);
            }
            return new List<UserModel>();
        }

        public async Task<UserModel> GetById(string id)
        {
            string url = URL + "?id=" + id;

            HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserModel>>(content);
                return users[0];
            }
            return new UserModel();
        }
        public async Task CreateUser(PostUsersRequest user)
        {
            string json = JsonConvert.SerializeObject(user);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(URL, content);
        }

        public async Task UpdateUser(PostUsersRequest user, string id)
        {
            string url = URL + "/" + id;
            string json = JsonConvert.SerializeObject(user);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(url, content);
        }

        public async Task DeleteUser(string id)
        {
            string url = URL + "/" + id;
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.DeleteAsync(url);

        }
    }
}
