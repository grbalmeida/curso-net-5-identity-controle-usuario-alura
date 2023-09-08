using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> DeslogaUsuario()
        {
            var resultadoIdentity = _signInManager.SignOutAsync();

            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();

            return Result.Fail("Logout falhou");
        }
    }
}
