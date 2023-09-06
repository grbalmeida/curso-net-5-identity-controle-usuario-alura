using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            var readDto = await _sessaoService.AdicionaSessao(sessaoDto);
            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaSessaoPorId(int id)
        {
            var readDto = await _sessaoService.RecuperaSessaoPorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }
    }
}
