using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = await _filmeService.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = readDto.Id }, readDto);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            var readDto = await _filmeService.RecuperaFilmes(classificacaoEtaria);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaFilmePorId(int id)
        {
            var filmeDto = await _filmeService.RecuperaFilmePorId(id);

            if (filmeDto != null)
            {
                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var resultado = await _filmeService.AtualizaFilme(id, filmeDto);
            
            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFilme(int id)
        {
            var resultado = await _filmeService.DeletaFilme(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
