using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> usuario)
        {

            //aplicando as claims que eu quero guardar
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username",usuario.UserName),
                new Claim("id",usuario.Id.ToString()),
            };

            //criando a chave e credenciais
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fedaf7d8863b48e197b9287d492b708e"));
            var credenciais = new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);


            //configurando as informações do token
            var token = new JwtSecurityToken(
                claims:direitosUsuario,
                signingCredentials:credenciais,
                expires:DateTime.UtcNow.AddHours(1)
                );

            //gerando o token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
