using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, RoleManager<IdentityRole<int>> roleManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity,createUsuarioDto.Password).Result;
             _ = _roleManager.CreateAsync(new IdentityRole<int>("ADMIN")).Result;
             _ = _userManager.AddToRoleAsync(usuarioIdentity,"ADMIN").Result;


            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

                //Evitar caracter zuado.
                var encodedCode = HttpUtility.UrlEncode(code.Result);
                
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email},"Link de ativação", usuarioIdentity.Id, encodedCode);
                return Result.Ok().WithSuccess(code.Result);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result AtiveContaUsuario(AtivaContaRequest ativaContaRequest)
        {
            var identityUser = _userManager.Users.FirstOrDefault(x => x.Id == ativaContaRequest.UsuarioId);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, ativaContaRequest.CodigoDeAtivacao).Result;

            if (identityResult.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao ativar conta do usuário.");
        }
    }
}
