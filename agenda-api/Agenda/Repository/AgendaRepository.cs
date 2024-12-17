using Agenda.Context;
using Agenda.Domain;
using Agenda.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        public readonly AppDbContext _context;

        public AgendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AgendaModel> BuscarPorId(int id)
        {
            var agenda = await _context.Agendas
                .AsNoTracking()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return agenda;
        }

        public async Task<IEnumerable<AgendaModel>> ListarAgendas()
        {
            var agendas = await _context.Agendas
                .AsNoTracking()
                .ToListAsync();

            return agendas;
        }

        public async Task<AgendaModel> CriarAgenda(AgendaModel agenda)
        {
            _context.Add(agenda);
            await _context.SaveChangesAsync();

            return agenda;
        }

        public async Task DeletarAgenda(AgendaModel agenda)
        {
            _context.Remove(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task<AgendaModel> EditarAgenda(AgendaModel agenda)
        {
            _context.Update(agenda);
            await _context.SaveChangesAsync();

            return agenda;
        }
    }
}
