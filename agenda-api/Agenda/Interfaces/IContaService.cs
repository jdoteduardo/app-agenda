using Agenda.ViewModel;

namespace Agenda.Interfaces
{
    public interface IContaService
    {
        Task<UsuarioViewModel> CadastrarUsuario(UsuarioViewModel usuarioViewModel);
        Task<UsuarioTokenViewModel> Login(UsuarioViewModel usuarioViewModel);
        Task<UsuarioTokenViewModel> GerarTokenRefresh(string token);
    }
}
