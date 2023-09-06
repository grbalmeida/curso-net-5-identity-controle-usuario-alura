using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadEnderecoDto> AdicionaEndereco(CreateEnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public async Task<IEnumerable<Endereco>> RecuperaEnderecos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<ReadEnderecoDto> RecuperaEnderecoPorId(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);

            if (endereco != null)
            {
                var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return enderecoDto;
            }

            return null;
        }

        public async Task<Result> AtualizaEndereco(int id, UpdateEnderecoDto updateDto)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(updateDto, endereco);
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> DeletaEndereco(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
