using BuscarCEPInterface.Models;
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
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult BuscarCEP(string cep)
        {
            var endereco = _apiEnderecoService.BuscarPorCep(cep);
            return View(endereco);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
