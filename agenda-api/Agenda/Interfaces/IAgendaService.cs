using Agenda.ViewModel;

namespace Agenda.Interfaces
{
    public interface IAgendaService
    {
        Task<AgendaViewModel> BuscarPorId(int id);
        Task<IEnumerable<AgendaViewModel>> ListarAgendas();
        Task<AgendaViewModel> CriarAgenda(AgendaViewModel agendaViewModel);
        Task<AgendaViewModel> EditarAgenda(AgendaViewModel agendaViewModel);
        Task DeletarAgenda(int id);
    }
}
