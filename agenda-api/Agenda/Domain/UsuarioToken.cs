namespace Agenda.Domain
{
    public class UsuarioRefreshToken
    {
        public int UsuarioId { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string RefreshToken { get; set; }
    }
}
