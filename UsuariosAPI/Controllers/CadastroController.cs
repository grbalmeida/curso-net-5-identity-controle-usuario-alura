using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroService.CadastraUsuario(createDto);

            if (resultado.IsFailed) return StatusCode(500);

            return Ok(resultado.Successes);
        }

        [HttpGet("/ativa")]
        public async Task<IActionResult> AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            var resultado = await _cadastroService.AtivaContaUsuario(request);

            if (resultado.IsFailed) return StatusCode(500);

            return Ok(resultado.Successes);
        }
    }
}
