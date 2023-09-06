using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            var readDto = await _gerenteService.AdicionaGerente(gerenteDto);
            return CreatedAtAction(nameof(RecuperaGerentePorId), new { readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaGerentePorId(int id)
        {
            var readDto = await _gerenteService.RecuperaGerentePorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaGerente(int id)
        {
            var resultado = await _gerenteService.DeletaGerente(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
