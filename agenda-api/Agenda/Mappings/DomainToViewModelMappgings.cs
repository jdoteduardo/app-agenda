using Agenda.Domain;
using Agenda.ViewModel;
using AutoMapper;

namespace Agenda.Mappings
{
    public class DomainToViewModelMappgings : Profile
    {
        public DomainToViewModelMappgings()
        {
            CreateMap<AgendaModel, AgendaViewModel>().ReverseMap();
            CreateMap<UsuarioViewModel, Usuario>().ReverseMap();
        }
    }
}
