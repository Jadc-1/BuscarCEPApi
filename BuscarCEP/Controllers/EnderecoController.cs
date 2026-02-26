using BuscarCEP.Models;
using BuscarCEP.Repositories;
using BuscarCEP.Services;
using BuscarCEP.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BuscarCEP.Controllers
{
    [ApiController]
    [Route("endereco")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IViaCepService _viaCepService;
        private readonly ICepService _cepService;

        public EnderecoController(IEnderecoRepository enderecoRepository, IViaCepService viaCepService, ICepService cepService)
        {
            _enderecoRepository = enderecoRepository;
            _viaCepService = viaCepService;
            _cepService = cepService;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(string cep)
        {
            try
            {
                var endereco = await _cepService.IncluirEndereco(cep);

                return Created("endereco", endereco); // 201 (Criado)

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult BuscarEnderecos()
        {
            // Todo - TryCatch, trocar repository para service, com validações
            var enderecos = _enderecoRepository.BuscarTodos();
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("cep/{cep}")]
        public IActionResult BuscarPorCEP(string cep)
        {
            try
            {
                var endereco = _cepService.BuscarEnderecoPorCEP(cep);
                return Ok(endereco);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);  // 400 (erro de solicitação)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);  // 404 (não encontrado)
            }
        }

        [HttpGet]
        [Route("uf/{uf}")]
        public IActionResult BuscarPorUF(string uf)
        {
            // Todo - TryCatch, trocar repository para service, com validações
            var enderecos = _cepService.BuscarEnderecosPorUF(uf);
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("via-cep/{cep}")]
        public IActionResult BuscarViaCep(string cep)
        {
            try
            {
                // Todo - TryCatch, trocar repository para service, com validações
                var endereco = _viaCepService.BuscarEnderecoPorCEP(cep);
                return Ok(endereco);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);  // 404 (não encontrado)
            }
        }
    }
}
