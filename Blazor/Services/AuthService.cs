using Blazor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Services
{
    public class AuthService : IAuthService
    {
       
        public HttpClient _httpClient { get; }
        public AuthService(HttpClient httpClient)
        {
            
            _httpClient = httpClient;

        }

        public async Task<LoginResponseModel> LoginAsync(LoginModel model)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:54596/api/users/login"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")

            };

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonConvert.DeserializeObject<LoginResponseModel>(result));
        }


        public async Task<bool> RegisterModel(RegisterModel model)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("/register"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(model))

            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await Task.FromResult(true);
            else
                return await Task.FromResult(false);
        }
    }
}
