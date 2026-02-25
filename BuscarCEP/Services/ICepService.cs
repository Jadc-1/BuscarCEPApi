using BuscarCEP.Models;
using BuscarCEP.ViewModel;

namespace BuscarCEP.Services
{
    public interface ICepService
    {
        List<EnderecoViewModel> BuscarTodos();
        EnderecoViewModel BuscarEnderecoPorCEP(string cep);
        List<EnderecoViewModel> BuscarEnderecosPorUF(string uf);

        bool EnderecoExistente(string cep);

        Task<Endereco> IncluirEndereco(string cep);
    }
}
