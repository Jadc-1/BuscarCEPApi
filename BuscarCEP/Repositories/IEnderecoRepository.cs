using BuscarCEP.Models;

namespace BuscarCEP.Repositories
{
    public interface IEnderecoRepository
    {
        void Adicionar(Endereco endereco);

        List<Endereco> BuscarTodos();

        List<Endereco> BuscarPorCEP(string cep);

        List<Endereco> BuscarPorUF(string UF);

        List<Endereco> BuscarPorViaCep(string cep);
    }
}
