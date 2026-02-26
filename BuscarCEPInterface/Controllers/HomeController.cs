using BuscarCEPInterface.Models;
using BuscarCEPInterface.Models.ViewModels;
using BuscarCEPInterface.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuscarCEPInterface.Controllers
{
    public class HomeController : Controller
    {
        private BuscarCEPViewModel _viewModel = new BuscarCEPViewModel();
        private ApiEnderecoService _apiEnderecoService = new ApiEnderecoService(new HttpClient());

        public HomeController()
        {
            _viewModel.Enderecos = new List<EnderecoViewModel>();
        }
        public IActionResult Index()
        {
            return View(_viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> BuscarCEPAsync(string cep, bool buscarNaBase)
        {
            EnderecoViewModel? endereco = null;

            if (buscarNaBase)
            {
                endereco = await _apiEnderecoService.BuscarPorCepAsync(cep);
            }
            else
            {
                endereco = await _apiEnderecoService.IncluirEnderecoAsync(cep);
            }

            if (endereco != null)
            {
                _viewModel.Enderecos = [endereco];
            }
            else
            {
                _viewModel.Enderecos = null;
            }

            return View("Index", _viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarPorUfAsync(string uf)
        {
            var enderecos = await _apiEnderecoService.BuscarPorUfAsync(uf);

            if (enderecos != null)
            {
                _viewModel.Enderecos = [.. enderecos];
            }
            else
            {
                _viewModel.Enderecos = null;
            }
            return View("Index", _viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
