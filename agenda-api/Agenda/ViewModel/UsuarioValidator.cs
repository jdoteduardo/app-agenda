using Agenda.Interfaces;
using Agenda.Repository;
using FluentValidation;

namespace Agenda.ViewModel
{
    public class UsuarioValidator : AbstractValidator<UsuarioViewModel>
    {
        private readonly IContaRepository _contaRepository;

        public UsuarioValidator(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;

            RuleSet("Login", () =>
            {
                RuleFor(p => p.Login)
                .MustAsync(UsuarioExistente).WithMessage("Usuário não existe");
            });

            RuleSet("Cadastrar", () =>
            {
                RuleFor(p => p.Login)
                .NotEmpty().WithMessage("Login não pode ser vazio")
                .NotNull().WithMessage("Informe o nome do login");

                RuleFor(p => p.Senha)
                .NotEmpty().WithMessage("Senha não pode ser vazia")
                .NotNull().WithMessage("Informe a senha");
            });
        }

        private async Task<bool> UsuarioExistente(string login, CancellationToken token) => await _contaRepository.VerificarExistenciaUsuario(login) != null;
    }
}
