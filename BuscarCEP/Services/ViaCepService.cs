using BuscarCEP.ViewModel;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuscarCEP.Services
{
    public class ViaCepService : IViaCepService
    {
        public Task<EnderecoViewModel> BuscarEnderecoPorCEP(string cep)
        {
            cep = cep.Replace("-", "").Trim();

            if (cep.Length != 8 || !cep.All(char.IsDigit))
            {
                throw new ArgumentException("O Cep deve conter 8 dígitos numéricos");
            }

            string viaCEPUrl = "https://viacep.com.br/ws/" + cep + "/json/";
            using var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var result = client.DownloadString(viaCEPUrl);
            
            if (result.Contains("\"erro\": true"))
            {
                throw new Exception("CEP não encontrado");
            }

            EnderecoViewModel? jsonRetorno = JsonConvert.DeserializeObject<EnderecoViewModel>(result);
            return Task.FromResult(jsonRetorno);
        }
    }
}
