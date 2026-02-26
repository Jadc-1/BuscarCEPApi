using BuscarCEPInterface.Models.ViewModels;

namespace BuscarCEPInterface.Services
{
    public class ApiEnderecoService
    {
        private readonly HttpClient _httpClient;

        public ApiEnderecoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https//localhost:7219/");
        }

        public EnderecoViewModel BuscarPorCep(string cep)
        {
            var endereco = _httpClient.GetFromJsonAsync<EnderecoViewModel>($"endereco/cep/{cep}").Result;
            return endereco;
        }

        public EnderecoViewModel BuscarPorUf(string uf)
        {
            var endereco = _httpClient.GetFromJsonAsync<EnderecoViewModel>($"endereco/uf/{uf}").Result;
            return endereco;
        }

        public void IncluirEndereco(string cep)
        {
            var response = _httpClient.PostAsJsonAsync("endereco", cep).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao incluir endereço: {response.ReasonPhrase}");
            }
        }
    }
}
