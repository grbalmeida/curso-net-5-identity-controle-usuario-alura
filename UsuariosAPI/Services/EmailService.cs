using System;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        public async Task EnviarEmail(string[] destinarios, string assunto, int usuarioId, string code)
        {
            var mensagem = new Mensagem(destinarios, assunto, usuarioId, code);
        }
    }
}
