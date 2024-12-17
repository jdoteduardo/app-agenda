using Agenda.Interfaces;
using Agenda.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    [Route("api/agendas")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            if (id == null || id <= 0)
                return BadRequest("ID de Agenda inválido");

            var agenda = await _agendaService.BuscarPorId(id);

            if (agenda is null)
                return NotFound("Agenda não existe");

            return Ok(agenda);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarAgendas()
        {
            try
            {
                var agenda = await _agendaService.ListarAgendas();

                return Ok(agenda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarAgenda([FromBody] AgendaViewModel agendaViewModel, 
            [FromServices] IValidator<AgendaViewModel> validator)
        {
            ValidationResult resultValidation = await validator.ValidateAsync(agendaViewModel, options => options.IncludeRuleSets("Cadastrar"));

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            var result = await _agendaService.CriarAgenda(agendaViewModel);

            if (result is null)
                throw new ApplicationException("Erro ao cadastrar agenda");

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditarAgenda([FromBody] AgendaViewModel agendaViewModel,
            [FromServices] IValidator<AgendaViewModel> validator)
        {
            ValidationResult resultValidation = await validator.ValidateAsync(agendaViewModel, options => options.IncludeRuleSets("Editar"));

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            var result = await _agendaService.EditarAgenda(agendaViewModel);

            if (result is null)
                throw new ApplicationException("Erro ao editar agenda");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletarAgenda(int id,
            [FromServices] IValidator<AgendaViewModel> validator)
        {
            var agendaViewModel = new AgendaViewModel { Id = id };

            ValidationResult resultValidation = await validator.ValidateAsync(agendaViewModel, options => options.IncludeRuleSets("Deletar"));

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            await _agendaService.DeletarAgenda(id);

            return Ok(new { Mensagem = "Deletado com sucesso" });
        }
    }
}
