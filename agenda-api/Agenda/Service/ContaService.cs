using Agenda.Cryptography;
using Agenda.Domain;
using Agenda.Interfaces;
using Agenda.Repository;
using Agenda.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Agenda.Service
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ContaService(IContaRepository contaRepository, IMapper mapper, IConfiguration configuration)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UsuarioViewModel> CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioViewModel);
            usuario.Senha = Criptografia.HashPassword(usuario.Senha);
            var data = await _contaRepository.CadastrarUsuario(usuario);
            return _mapper.Map<UsuarioViewModel>(data);
        }

        public async Task<UsuarioTokenViewModel> GerarTokenRefresh(string token)
        {
            var usuarioToken = await _contaRepository.BuscarUsuarioPorToken(token);
            var usuario = await _contaRepository.BuscarUsuarioPorId(usuarioToken.UsuarioId);

            if (DateTime.Now > usuarioToken.DataExpiracao.Date)
            {
                throw new ApplicationException("Token expirado");
            }

            return await GenerateJwtToken(usuario, true);
        }

        public async Task<UsuarioTokenViewModel> Login(UsuarioViewModel usuarioViewModel)
        {
            var usuarioModel = _mapper.Map<Usuario>(usuarioViewModel);
            var usuario = await _contaRepository.VerificarExistenciaUsuario(usuarioModel.Login);
            var atualizaRefreshToken = await _contaRepository.VerificarExistenciaUsuarioToken(usuario.Id);

            if (usuario is null || !Criptografia.VerifyPassword(usuario.Senha, usuarioModel.Senha))
            {
                throw new ApplicationException("Usuário ou senha incorretos");
            }

            return await GenerateJwtToken(usuario, atualizaRefreshToken);
        }


        private async Task<UsuarioTokenViewModel> GenerateJwtToken(Usuario user, bool atualizar)
        {
            var claims = new[]
            {
                new Claim("usuario", user.Login)
            };

            var keyRefreshToken = Guid.NewGuid().ToString();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var usuarioToken = new UsuarioRefreshToken()
            {
                UsuarioId = user.Id,
                DataExpiracao = DateTime.UtcNow.AddMinutes(60),
                RefreshToken = keyRefreshToken
            };

            if (!atualizar)
                await _contaRepository.CadastrarRefreshToken(usuarioToken);
            else
                await _contaRepository.AtualizarRefreshToken(usuarioToken);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UsuarioTokenViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = keyRefreshToken
            };
        }

    }
}
