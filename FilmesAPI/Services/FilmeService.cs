
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadFilmeDto> AdicionaFilme(CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public async Task<List<ReadFilmeDto>> RecuperaFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;

            if (classificacaoEtaria == null)
            {
                filmes = await _context.Filmes.ToListAsync();
            }
            else
            {
                filmes = await _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToListAsync();
            }

            if (filmes != null && filmes.Count > 0)
            {
                var filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return filmesDto;
            }

            return null;
        }

        public async Task<ReadFilmeDto> RecuperaFilmePorId(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme != null)
            {
                var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return filmeDto;
            }

            return null;
        }

        public async Task AtualizaFilme(int id, UpdateFilmeDto updateDto)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            _mapper.Map(updateDto, filme);
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
        }

        public async Task DeletaFilme(ReadFilmeDto readDto)
        {
            var filme = _mapper.Map<Filme>(readDto);
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
        }
    }
}
