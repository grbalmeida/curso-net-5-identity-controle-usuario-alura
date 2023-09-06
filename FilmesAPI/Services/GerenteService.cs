using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadGerenteDto> AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            var gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public async Task<ReadGerenteDto> RecuperaGerentePorId(int id)
        {
            var gerente = await _context.Gerentes.FirstOrDefaultAsync(gerente => gerente.Id == id);

            if (gerente != null)
            {
                var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return gerenteDto;
            }

            return null;
        }

        public async Task DeletaGerente(ReadGerenteDto readDto)
        {
            var gerente = _mapper.Map<Gerente>(readDto);
            _context.Gerentes.Remove(gerente);
            await _context.SaveChangesAsync();
        }
    }
}
