using Agenda.Domain;
using Agenda.Interfaces;
using FluentValidation;

namespace Agenda.ViewModel
{
    public class AgendaValidator : AbstractValidator<AgendaViewModel>
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaValidator(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;

            RuleSet("Buscar", () =>
            {
                RuleFor(p => p.Id)
                .MustAsync(AgendaExistente).WithMessage("Agenda não existe");
            });

            RuleSet("Cadastrar", () =>
            {
                RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazio")
                .NotNull().WithMessage("Informe o nome do contato");
            });

            RuleSet("Editar", () =>
            {
                RuleFor(p => p.Id)
                .MustAsync(AgendaExistente).WithMessage("Agenda não existe");
            });

            RuleSet("Deletar", () =>
            {
                RuleFor(p => p.Id)
                .MustAsync(AgendaExistente).WithMessage("Agenda não existe");
            });
        }

        private async Task<bool> AgendaExistente(int id, CancellationToken token) => await _agendaRepository.BuscarPorId(id) != null;
    }
}
