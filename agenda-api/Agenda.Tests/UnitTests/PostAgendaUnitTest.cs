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
    public class PostAgendaUnitTest : AgendaUnitTest
    {
        [Fact]
        public async Task Test_PostAgenda_ReturnsOkResult()
        {
            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            var agenda = new AgendaViewModel
            {
                Nome = "joao",
                Email = "joao@email.com",
                Telefone = "88888888"
            };

            _mockAgendaService.Setup(service => service.CriarAgenda(agenda))
                              .ReturnsAsync(agenda);

            var result = await _controller.CadastrarAgenda(agenda, validator);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Test_PostAgenda_ReturnsBadRequest()
        {
            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            var agenda = new AgendaViewModel
            {
                Nome = null,
                Email = "joao@email.com",
                Telefone = "88888888"
            };

            _mockAgendaService.Setup(service => service.CriarAgenda(agenda))
                              .ReturnsAsync(agenda);

            var result = await _controller.CadastrarAgenda(agenda, validator);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
