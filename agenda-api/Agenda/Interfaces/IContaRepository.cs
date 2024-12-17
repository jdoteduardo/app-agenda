using Agenda.Domain;
using Agenda.ViewModel;

namespace Agenda.Interfaces
{
    public interface IContaRepository
    {
        Task<Usuario> BuscarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorId(int id);
        Task<Usuario> CadastrarUsuario(Usuario usuario);
        Task<Usuario> VerificarExistenciaUsuario(string login);

        Task CadastrarRefreshToken(UsuarioRefreshToken usuarioToken);
        Task AtualizarRefreshToken(UsuarioRefreshToken usuarioToken);
        Task<bool> VerificarExistenciaUsuarioToken(int usuarioId);
        Task<UsuarioRefreshToken> BuscarUsuarioPorToken(string token);
    }
}
