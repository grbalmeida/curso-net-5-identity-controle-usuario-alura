using System.Collections.Generic;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        public List<?> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId, string codigo)
        {
            Destinatario = ??;
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
