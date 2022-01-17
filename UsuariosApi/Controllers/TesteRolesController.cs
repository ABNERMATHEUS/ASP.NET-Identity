using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    [Route("teste-roles")]
    [ApiController]
    public class TesteRolesController : ControllerBase
    {

        

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult TesteRoleAdmin()
        {
            return Ok(new { status = "Liberado."});
        }   
    }
}
            