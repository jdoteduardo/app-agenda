using Agenda.Domain;
using Agenda.Interfaces;
using Agenda.ViewModel;
using AutoMapper;

namespace Agenda.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;

        public AgendaService(IAgendaRepository agendaRepository, IMapper mapper)
        {
            _agendaRepository = agendaRepository;
            _mapper = mapper;
        }

        public async Task<AgendaViewModel> BuscarPorId(int id)
        {
            var agenda = await _agendaRepository.BuscarPorId(id);
            return _mapper.Map<AgendaViewModel>(agenda);
        }

        public async Task<IEnumerable<AgendaViewModel>> ListarAgendas()
        {
            var agendas = await _agendaRepository.ListarAgendas();
            return _mapper.Map<IEnumerable<AgendaViewModel>>(agendas);
        }

        public async Task<AgendaViewModel> CriarAgenda(AgendaViewModel agendaViewModel)
        {
            var agenda = _mapper.Map<AgendaModel>(agendaViewModel);
            var data = await _agendaRepository.CriarAgenda(agenda);
            return _mapper.Map<AgendaViewModel>(data);
        }

        public async Task DeletarAgenda(int id)
        {
            var agenda = await _agendaRepository.BuscarPorId(id);
            await _agendaRepository.DeletarAgenda(agenda);
        }

        public async Task<AgendaViewModel> EditarAgenda(AgendaViewModel agendaViewModel)
        {
            var agenda = _mapper.Map<AgendaModel>(agendaViewModel);
            var data = await _agendaRepository.EditarAgenda(agenda);
            return _mapper.Map<AgendaViewModel>(data);
        }
    }
}
