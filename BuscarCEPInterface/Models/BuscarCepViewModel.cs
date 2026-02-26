using BuscarCEPInterface.Models.ViewModels;

namespace BuscarCEPInterface.Models
{
    public class BuscarCEPViewModel
    {
        public string? Cep { get; set; }
        public EnderecoViewModel? EnderecoCep { get; set; }

        public List<EnderecoViewModel>? Enderecos { get; set; }

        public bool PossuiCadastro { get; set; }

        public bool BuscarNaBase { get; set; }

    }
}
