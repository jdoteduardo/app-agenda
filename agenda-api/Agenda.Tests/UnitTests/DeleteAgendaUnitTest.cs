using Agenda.Domain;
using Agenda.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Tests.UnitTests
{
    public class DeleteAgendaUnitTest : AgendaUnitTest
    {
        [Fact]
        public async Task Test_DeleteAgenda_ReturnsOkResult()
        {
            var agendaViewModel = new AgendaViewModel
            {
                Id = 1,
                Nome = "eduardo",
                Email = "eduardo@email.com",
                Telefone = "88888888"
            };

            _mockAgendaRepository.Setup(repository => repository.BuscarPorId(agendaViewModel.Id))
                .ReturnsAsync(_mapper.Map<AgendaModel>(agendaViewModel));

            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            _mockAgendaService.Setup(service => service.DeletarAgenda(agendaViewModel.Id));

            var result = await _controller.DeletarAgenda(agendaViewModel.Id, validator);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Test_PostAgenda_ReturnsBadRequest()
        {
            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            var agenda = new AgendaViewModel
            {
                Id = 99,
                Nome = "joao",
                Email = "joao@email.com",
                Telefone = "88888888"
            };

            _mockAgendaService.Setup(service => service.DeletarAgenda(agenda.Id));

            var result = await _controller.DeletarAgenda(agenda.Id, validator);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
