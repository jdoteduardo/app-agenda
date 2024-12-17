using Agenda.Context;
using Agenda.Domain;
using Agenda.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repository
{
    public class ContaRepository : IContaRepository
    {
        public readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarUsuario(Usuario usuario)
        {
            var user = await _context.Usuarios
                .AsNoTracking()
                .Where(a => a.Login == usuario.Login && a.Senha == usuario.Senha)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            var user = await _context.Usuarios
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> VerificarExistenciaUsuario(string login)
        {
            var user = await _context.Usuarios
            .AsNoTracking()
            .Where(a => a.Login.ToUpper() == login.ToUpper())
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task CadastrarRefreshToken(UsuarioRefreshToken usuarioToken)
        {
            _context.Add(usuarioToken);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarRefreshToken(UsuarioRefreshToken usuarioToken)
        {
            _context.Update(usuarioToken);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerificarExistenciaUsuarioToken(int usuarioId)
        {
            var user = await _context.UsuarioRefreshTokens
            .AsNoTracking()
            .Where(u => u.UsuarioId == usuarioId)
            .FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<UsuarioRefreshToken> BuscarUsuarioPorToken(string token)
        {
            var user = await _context.UsuarioRefreshTokens
            .AsNoTracking()
            .Where(u => u.RefreshToken == token)
            .FirstOrDefaultAsync();

            return user;
        }
    }
}
