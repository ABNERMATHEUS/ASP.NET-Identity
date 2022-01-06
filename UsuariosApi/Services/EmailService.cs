using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId,  string code)
        {
            Messagem messagem = new Messagem(destinatario, assunto, usuarioId, code);
            var mensagemDeEmail = CriarCorpoDoEmail(messagem);
            Enviar(mensagemDeEmail);
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"),true);

                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(mensagemDeEmail);

                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        
        private MimeMessage CriarCorpoDoEmail(Messagem messagem)
        {
            var messageEmail = new MimeMessage();
            messageEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            messageEmail.To.AddRange(messagem.Destinatario);
            messageEmail.Subject = messagem.Assunto;
            messageEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = messagem.Conteudo
            };
            return messageEmail;

        }
    }
}
