using BuscarCEP.Models;
using BuscarCEP.Repositories;
using BuscarCEP.Services;
using BuscarCEP.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BuscarCEP.Controllers
{
    [ApiController]
    [Route("/endereco")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IViaCepService _viaCepService;

        public EnderecoController(IEnderecoRepository enderecoRepository, IViaCepService viaCepService)
        {
            _enderecoRepository = enderecoRepository;
            _viaCepService = viaCepService;
        }

        [HttpPost]
        public IActionResult Adicionar(EnderecoViewModel enderecoViewModel)
        {
            var endereco = new Endereco(
                enderecoViewModel.cep,
                enderecoViewModel.logradouro,
                enderecoViewModel.bairro,
                enderecoViewModel.uf,
                enderecoViewModel.unidade,
                enderecoViewModel.ibge,
                enderecoViewModel.gia
                );

            _enderecoRepository.Adicionar(endereco);
            return Ok();
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var enderecos = _enderecoRepository.BuscarTodos();
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("/cep/{cep}")]
        public IActionResult BuscarPorCEP(string cep)
        {
            var enderecos = _enderecoRepository.BuscarPorCEP(cep);
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("/uf/{uf}")]
        public IActionResult BuscarPorUF(string uf)
        {
            var enderecos = _enderecoRepository.BuscarPorUF(uf);
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("/via-cep/{cep}")]
        public IActionResult BuscarViaCep(string cep)
        {
            var endereco = _viaCepService.BuscarEnderecoPorCEP(cep);
            return Ok(endereco);
        }
    }
}
