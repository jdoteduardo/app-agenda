using Agenda.Domain;

namespace Agenda.Interfaces
{
    public interface IAgendaRepository
    {
        Task<AgendaModel> BuscarPorId(int id);
        Task<IEnumerable<AgendaModel>> ListarAgendas();
        Task<AgendaModel> CriarAgenda(AgendaModel agenda);
        Task<AgendaModel> EditarAgenda(AgendaModel agenda);
        Task DeletarAgenda(AgendaModel agenda);
    }
}
