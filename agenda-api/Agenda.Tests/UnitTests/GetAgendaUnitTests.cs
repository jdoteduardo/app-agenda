using Agenda.Controllers;
using Agenda.Interfaces;
using Agenda.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Agenda.Tests.UnitTests
{
    public class GetAgendaUnitTests : AgendaUnitTest
    {
        [Fact]
        public async Task Test_GetAgendaById_ReturnsOkResult()
        {
            var validator = new AgendaValidator(_mockAgendaRepository.Object);

            var agendaViewModel = new AgendaViewModel { Id = 2 };
            _mockAgendaService.Setup(service => service.BuscarPorId(2))
                              .ReturnsAsync(agendaViewModel);

            var result = await _controller.BuscarPorId(2);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Test_GetAgendaById_ReturnsBadRequest()
        {
            var result = await _controller.BuscarPorId(-1);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Test_GetAgendaById_ReturnsNotFoundResult()
        {
            var result = await _controller.BuscarPorId(999999);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Test_GetAgendas_ReturnsOkResult()
        {
            var result = await _controller.ListarAgendas();

            var notFoundResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, notFoundResult.StatusCode);
        }
    }
}
