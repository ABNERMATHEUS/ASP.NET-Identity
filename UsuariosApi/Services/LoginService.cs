using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {

        private readonly SignInManager<IdentityUser<int>> _signInManager;        
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedUserName == request.UserName.ToUpper());                
                Token token = _tokenService.CreateToken(identityUser,_signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            IdentityUser<int> usuario = RecuperaUsuarioPorEmail(request.Email);
            if (usuario != null)
            {
                string codigoRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(usuario).Result;
                return Result.Ok().WithSuccess(codigoRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");

        }

        public Result EfetuaResetSenhaUsuario(EfetuaResetRequest request)
        {
            IdentityUser<int> usuario = RecuperaUsuarioPorEmail(request.Email);
            IdentityResult identityResult = _signInManager.UserManager.ResetPasswordAsync(usuario,request.Token,request.Password).Result;
            if (identityResult.Succeeded) return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
            return Result.Fail("Houve um erro na operação");
        }


        private IdentityUser<int> RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedEmail == email.ToUpper());
        }

    }
}
