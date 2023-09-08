using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result> CadastraUsuario(CreateUsuarioDto createDto)
        {
            var usuario = _mapper.Map<Usuario>(createDto);
            var usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            if (resultadoIdentity.Succeeded) return Result.Ok();

            return Result.Fail("Falha ao cadastrar usuário");
        }
    }
}
