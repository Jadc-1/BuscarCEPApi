using BuscarCEP.Repositories;
using BuscarCEP.ViewModel;

namespace BuscarCEP.Services
{
    public class CepService : ICepService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        
        public CepService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public EnderecoViewModel BuscarEnderecoPorCEP(string cep)
        {
            cep = cep.Replace("-", "").Trim();

            if (cep.Length != 8 || !cep.All(char.IsDigit))
            {
                throw new ArgumentException("O Cep deve conter 8 dígitos numéricos");
            }

            var endereco = _enderecoRepository.BuscarPorCEP(cep).FirstOrDefault();
            if (endereco == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado para o CEP informado");
            }
            return new EnderecoViewModel(endereco.cep, 
                endereco.logradouro, 
                endereco.bairro, 
                endereco.uf, 
                endereco.unidade.ToString(), 
                endereco.ibge, 
                endereco.gia);
        }

        public EnderecoViewModel BuscarEnderecoPorUF(string uf)
        {
            // Todo - Validações para a busca por UF
            throw new NotImplementedException();
        }

        public EnderecoViewModel BuscarTodos()
        {
            //Todo - Validações para a busca de todos os endereços
            throw new NotImplementedException();
        }

        public EnderecoViewModel IncluirEndereco(EnderecoViewModel enderecoViewModel)
        {
            //Todo - Validações para a inclusão de um novo endereço
            throw new NotImplementedException();
        }
    }
}
