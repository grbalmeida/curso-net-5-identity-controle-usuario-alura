﻿using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Requests;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Result> LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (resultadoIdentity.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());

                var token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Login falhou");
        }
    }
}
