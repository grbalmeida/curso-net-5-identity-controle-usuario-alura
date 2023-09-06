using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class SessaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadSessaoDto> AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            var sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public async Task<ReadSessaoDto> RecuperaSessaoPorId(int id)
        {
            var sessao = await _context.Sessoes.FirstOrDefaultAsync(sessao => sessao.Id == id);

            if (sessao != null)
            {
                var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
                return sessaoDto;
            }

            return null;
        }
    }
}
