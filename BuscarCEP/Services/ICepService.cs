using BuscarCEP.ViewModel;

namespace BuscarCEP.Services
{
    public interface ICepService
    {
        EnderecoViewModel BuscarTodos();
        EnderecoViewModel BuscarEnderecoPorCEP(string cep);
        EnderecoViewModel BuscarEnderecoPorUF(string uf);
        EnderecoViewModel IncluirEndereco(EnderecoViewModel enderecoViewModel);
    }
}
