using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var readDto = await _cinemaService.AdicionaCinema(cinemaDto);
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { readDto.Id }, readDto);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            var readDto = await _cinemaService.RecuperaCinemas(nomeDoFilme);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaCinemaPorId(int id)
        {
            var readDto = await _cinemaService.RecuperaCinemaPorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var resultado = await _cinemaService.AtualizaCinema(id, cinemaDto);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaCinema(int id)
        {
            var resultado = await _cinemaService.DeletaCinema(id);

            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
