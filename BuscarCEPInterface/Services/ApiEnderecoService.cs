using BuscarCEPInterface.Models.ViewModels;
using System.Runtime.ConstrainedExecution;

namespace BuscarCEPInterface.Services
{
    public class ApiEnderecoService
    {
        private readonly HttpClient _httpClient;

        public ApiEnderecoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7219/");
        }

        public async Task<EnderecoViewModel?> BuscarPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"endereco/cep/{cep}/");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EnderecoViewModel>();
        }

        public async Task<EnderecoViewModel?> BuscarViaCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"endereco/via-cep/{cep}/");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return null;

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var envelope = System.Text.Json.JsonSerializer.Deserialize<ApiEnvelope<EnderecoViewModel>>(json, options);
            return envelope?.result;
        }

        public async Task<List<EnderecoViewModel>?> BuscarPorUfAsync(string uf)
        {
            var response = await _httpClient.GetAsync($"endereco/uf/{uf}/");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return null;

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return System.Text.Json.JsonSerializer.Deserialize<List<EnderecoViewModel>>(json, options);
        }

        public async Task<EnderecoViewModel?> IncluirEnderecoAsync(string cep)
        {
            var response = await _httpClient.PostAsJsonAsync($"endereco?cep={cep}", cep);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json)) return null;

                var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return System.Text.Json.JsonSerializer.Deserialize<EnderecoViewModel>(json, options);
            }
        }
    }

    public class ApiEnvelope<T>
    {
        public T? result { get; set; }
    }
}
