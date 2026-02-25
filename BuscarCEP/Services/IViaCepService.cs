using BuscarCEP.ViewModel;

namespace BuscarCEP.Services
{
    public interface IViaCepService
    {
        Task<EnderecoViewModel> BuscarEnderecoPorCEP(string cep);

    }
}
