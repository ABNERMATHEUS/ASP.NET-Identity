using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario([FromBody] LoginRequest request)
        {
            Result resultado = _loginService.LogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Reasons);
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaResetSenhaUsuario(request);
            if(resultado.IsFailed)return Unauthorized(resultado.Errors);
            return Ok(resultado.Reasons);
        }

        [HttpPost("/efetuar-reset")]
        public IActionResult EfetuaResetSenhaUsuario(EfetuaResetRequest request)
        {
            Result resultado = _loginService.EfetuaResetSenhaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Reasons);
        }
    }
}
