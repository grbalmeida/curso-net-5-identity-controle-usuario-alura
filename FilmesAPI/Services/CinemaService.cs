using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadCinemaDto> AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public async Task<List<ReadCinemaDto>> RecuperaCinemas(string nomeDoFilme)
        {
            var cinemasQuery = _context.Cinemas.AsQueryable();

            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                cinemasQuery = cinemasQuery.Where(cinema => cinema.Sessoes.Any(sessao =>
                    sessao.Filme.Titulo.Contains(nomeDoFilme)));
            }

            var cinemas = await cinemasQuery.ToListAsync();

            var cinemasDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);

            return cinemasDto;
        }

        public async Task<ReadCinemaDto> RecuperaCinemaPorId(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);

            if (cinema != null)
            {
                var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }

            return null;
        }

        public async Task AtualizaCinema(int id, UpdateCinemaDto updateDto)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);
            _mapper.Map(updateDto, cinema);
            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task DeletaCinema(ReadCinemaDto readDto)
        {
            var cinema = _mapper.Map<Cinema>(readDto);
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
        }
    }
}
