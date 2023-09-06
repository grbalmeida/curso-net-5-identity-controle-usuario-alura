using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var readDto = await _enderecoService.AdicionaEndereco(enderecoDto);

            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { readDto.Id }, readDto);
        }

        [HttpGet]
        public async Task<IEnumerable<Endereco>> RecuperaEnderecos()
        {
            return await _enderecoService.RecuperaEnderecos();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaEnderecoPorId(int id)
        {
            var readDto = await _enderecoService.RecuperaEnderecoPorId(id);
        
            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var resultado = await _enderecoService.AtualizaEndereco(id, enderecoDto);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaEndereco(int id)
        {
            var resultado = await _enderecoService.DeletaEndereco(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
