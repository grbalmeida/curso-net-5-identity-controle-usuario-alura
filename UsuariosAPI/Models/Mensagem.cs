using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId, string codigo)
        {
            Destinatarios = new List<MailboxAddress>();
            Destinatarios.AddRange(destinatarios.Select(d => new MailboxAddress(d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
