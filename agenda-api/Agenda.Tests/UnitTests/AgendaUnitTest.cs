using Agenda.Context;
using Agenda.Controllers;
using Agenda.Interfaces;
using Agenda.Mappings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Tests.UnitTests
{
    public class AgendaUnitTest
    {
        protected readonly AgendaController _controller;
        protected readonly Mock<IAgendaService> _mockAgendaService;
        protected readonly Mock<IAgendaRepository> _mockAgendaRepository;
        protected readonly IMapper _mapper;

        public AgendaUnitTest()
        {
            // Mock do IAgendaService
            _mockAgendaService = new Mock<IAgendaService>();

            // Mock do IAgendaRepository
            _mockAgendaRepository = new Mock<IAgendaRepository>();

            // Passando o mock de IAgendaService e IValidator<AgendaViewModel> para o controlador
            _controller = new AgendaController(_mockAgendaService.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappgings());
            });

            _mapper = config.CreateMapper();
        }
    }
}
