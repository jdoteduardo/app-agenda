using Agenda.Interfaces;
using Agenda.Service;
using Agenda.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioViewModel usuarioViewModel,
            [FromServices] IValidator<UsuarioViewModel> validator)
        {
            ValidationResult resultValidation = await validator.ValidateAsync(usuarioViewModel, options => options.IncludeRuleSets("Login"));

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var result = await _contaService.Login(usuarioViewModel);

            if (result is null)
                throw new ApplicationException("Erro ao fazer login");

            return Ok(result);
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioViewModel usuarioViewModel,
            [FromServices] IValidator<UsuarioViewModel> validator)
        {
            ValidationResult resultValidation = await validator.ValidateAsync(usuarioViewModel, options => options.IncludeRuleSets("Cadastrar"));

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var result = await _contaService.CadastrarUsuario(usuarioViewModel);

            if (result is null)
                throw new ApplicationException("Erro ao cadastrar usuário");

            return Ok(result);
        }

        [HttpPost]
        [Route("RefreshToken/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _contaService.GerarTokenRefresh(refreshToken);

            if (result is null)
                throw new ApplicationException("Erro ao cadastrar usuário");

            return Ok(result);
        }
    }
}
