using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosApi.Services
{
    public class LogoutService
    {

        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signinManager)
        {
            this._signInManager = signinManager;
        }

        public Result DeslogaUsuario()
        {
            var resultadoIdentity = _signInManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou.");
        }
    }
}
