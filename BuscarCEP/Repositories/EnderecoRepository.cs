using BuscarCEP.Infraestructures;
using BuscarCEP.Models;

namespace BuscarCEP.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext(); 
        public void Adicionar(Endereco endereco)
        {
            _connectionContext.Enderecos.Add(endereco);
            _connectionContext.SaveChanges();
        }

        public List<Endereco> BuscarTodos()
        {
            return _connectionContext.Enderecos.ToList();
        }

        public List<Endereco> BuscarPorCEP(string cep)
        {
            return _connectionContext.Enderecos.Where(e => e.cep == cep).ToList();
        }

        public List<Endereco> BuscarPorUF(string UF)
        {
            return _connectionContext.Enderecos.Where(e => e.uf == UF).ToList();
        }

    }
}
