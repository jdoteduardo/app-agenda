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
    public class PutAgendaUnitTest : AgendaUnitTest
    {
        [Fact]
        public async Task Test_PutAgenda_ReturnsOkResult()
        {
            var agendaViewModel = new AgendaViewModel
            {
                Id = 1,
                Nome = "eduardo editado",
                Email = "eduardo@email.com",
                Telefone = "88888888"
            };

            _mockAgendaRepository.Setup(repository => repository.BuscarPorId(agendaViewModel.Id))
                .ReturnsAsync(_mapper.Map<AgendaModel>(agendaViewModel));

            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            _mockAgendaService.Setup(service => service.EditarAgenda(agendaViewModel))
                              .ReturnsAsync(agendaViewModel);

            var result = await _controller.EditarAgenda(agendaViewModel, validator);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task Test_PostAgenda_ReturnsBadRequest()
        {
            var agendaViewModel = new AgendaViewModel
            {
                Id = 1,
                Nome = "eduardo editado",
                Email = "eduardo@email.com",
                Telefone = "88888888"
            };

            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            _mockAgendaService.Setup(service => service.EditarAgenda(agendaViewModel))
                              .ReturnsAsync(agendaViewModel);

            var result = await _controller.EditarAgenda(agendaViewModel, validator);
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
