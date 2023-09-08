using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public async Task<IActionResult> DeslogaUsuario()
        {
            var resultado = await _logoutService.DeslogaUsuario();

            if (resultado.IsFailed) return Unauthorized(resultado.Errors);

            return Ok(resultado.Successes);
        }
    }
}
