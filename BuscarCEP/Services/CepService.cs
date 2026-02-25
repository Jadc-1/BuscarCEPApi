using BuscarCEP.Models;
using BuscarCEP.Repositories;
using BuscarCEP.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BuscarCEP.Services
{
    public class CepService : ICepService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IViaCepService _viaCepService;
        
        public CepService(IEnderecoRepository enderecoRepository, IViaCepService viaCepService)
        {
            _enderecoRepository = enderecoRepository;
            _viaCepService = viaCepService;
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

        public List<EnderecoViewModel> BuscarEnderecosPorUF(string uf)
        {
            if (uf == null)
            {
                throw new ArgumentNullException("Por favor preencha o campo");
            }
            else if (uf.Length != 2)
            {
                throw new ArgumentException("UF inválida");
            }

            var enderecos = _enderecoRepository.BuscarPorUF(uf).ToList();
            var listaEnderecosViewModel = new List<EnderecoViewModel>();   
            foreach (var endereco in enderecos)
            {
                var enderecoViewModel = new EnderecoViewModel(endereco.cep,
                    endereco.logradouro,
                    endereco.bairro,
                    endereco.uf,
                    endereco.unidade.ToString(),
                    endereco.ibge,
                    endereco.gia);

                listaEnderecosViewModel.Add(enderecoViewModel);
            } 
            return listaEnderecosViewModel;
        }

        public async Task<Endereco> IncluirEndereco(string cep)
        {
            cep = cep.Replace("-", "").Trim();
  
            if (EnderecoExistente(cep))
            {
                throw new ArgumentException("Endereço já cadastrado no banco");
            }

            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException("O campo Cep é obrigatório");
            }

            if (cep.Length != 8 || !cep.All(char.IsDigit))
            {
                throw new ArgumentException("O Cep deve conter 8 dígitos numéricos");
            }

            var endereco = await _viaCepService.BuscarEnderecoPorCEP(cep);
            long unidade = 0;

            if (!string.IsNullOrEmpty(endereco.unidade))
            {
                long.TryParse(endereco.unidade, out unidade); // Converte a string para long, para salvar no banco
            }

            if (endereco == null)
            {
                throw new ArgumentException("Cep não encontrado");
            }
            else
            {
                var novoEndereco = new Endereco(endereco.cep,
                    endereco.logradouro,
                    endereco.bairro,
                    endereco.uf.ToLower(),
                    unidade,
                    endereco.ibge,
                    endereco.gia);

                _enderecoRepository.Adicionar(novoEndereco);
                return novoEndereco;
            }

        }
        
        public bool EnderecoExistente(string cep)
        {
            var endereco = _enderecoRepository.BuscarPorCEP(cep).FirstOrDefault();
            return (endereco != null);
        }

        public List<EnderecoViewModel> BuscarTodos()
        {
            if (_enderecoRepository.BuscarTodos().Count() == 0)
            {
                throw new KeyNotFoundException("Nenhum endereço encontrado");
            } else
            {
                var enderecos = _enderecoRepository.BuscarTodos().ToList();
                var listaEnderecosViewModel = new List<EnderecoViewModel>();
                foreach (var endereco in enderecos)
                {
                    var enderecoViewModel = new EnderecoViewModel(endereco.cep,
                        endereco.logradouro,
                        endereco.bairro,
                        endereco.uf,
                        endereco.unidade.ToString(),
                        endereco.ibge,
                        endereco.gia);
                    listaEnderecosViewModel.Add(enderecoViewModel);
                }
                return listaEnderecosViewModel;
            }

        }
    }
}
