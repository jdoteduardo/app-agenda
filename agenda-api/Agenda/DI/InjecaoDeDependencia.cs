using Agenda.Interfaces;
using Agenda.Mappings;
using Agenda.Repository;
using Agenda.Service;
using Agenda.ViewModel;
using FluentValidation;

namespace Agenda.DI
{
    public static class InjecaoDeDependencia
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappgings));
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IValidator<AgendaViewModel>, AgendaValidator>();
            services.AddScoped<IValidator<UsuarioViewModel>, UsuarioValidator>();

            return services;
        }
    }
}
