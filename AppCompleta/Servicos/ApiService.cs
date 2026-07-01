using System.Net.Http.Json;
using AppCompleta.Models;

namespace AppCompleta.Servicos
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress =
                new Uri("http://192.168.1.173:5257/");
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/users/register",
                    user);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRO API: {ex}");
            }
        }

        public async Task<LoginResponse?> LoginAsync(
            string login,
            string senha)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/users/login",
                    new
                    {
                        Login = login,
                        Senha = senha
                    });

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<LoginResponse>();
        }
    }
}