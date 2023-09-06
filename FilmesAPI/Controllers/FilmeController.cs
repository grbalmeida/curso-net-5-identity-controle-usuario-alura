using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var readDto = await _filmeService.RecuperaFilmePorId(id);

            if (readDto == null)
            {
                return NotFound();
            }

            await _filmeService.AtualizaFilme(id, filmeDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFilme(int id)
        {
            var readDto = await _filmeService.RecuperaFilmePorId(id);

            if (readDto == null)
            {
                return NotFound();
            }

            await _filmeService.DeletaFilme(readDto);

            return NoContent();
        }
    }
}
