using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {

        private readonly CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario([FromBody]CreateUsuarioDto createUsuarioDto)
        {
            Result resultado = _cadastroService.CadastraUsuario(createUsuarioDto);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Reasons);
        }

        [HttpGet("/ativa")]
        public IActionResult AtiveContaUsuario([FromQuery] AtivaContaRequest ativaContaRequest)
        {
            Result resultado = _cadastroService.AtiveContaUsuario(ativaContaRequest);
            if(resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Reasons);
        }
    }
}
