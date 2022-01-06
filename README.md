
# Curso de .NET 5 e Identity: Implementando controle de usuário
- Entender conceitos de organização e separação de código
- Como utilizar o Identity para cadastrar usuários no sistema
- Como utilizar o Identity para autenticar usuários no sistema
- Como utilizar o Identity para implementar recursos como ativação de conta
- Como disparar e-mails com o ASP NET


Link certificado👇

 [![NuGet](https://img.shields.io/static/v1?label=CERTIFICADO_ALURA&message=VERIFICADO&color=blue)](https://cursos.alura.com.br/certificate/89658cdb-ebf7-414b-8db4-55494ab6cde3)
 
 
#### Para funcionar é necessário aplicar 
Inicialmente, acesse o diretório do projeto UsuariosApi através do seu terminal e execute o comando ``dotnet user-secrets init``

Depois

`````
dotnet user-secrets set “EmailSettings:From” “<SEU-EMAIL>”
dotnet user-secrets set “EmailSettings:SmtpServer” “smtp.gmail.com”
dotnet user-secrets set “EmailSettings:Port” 465
dotnet user-secrets set “EmailSettings:Port” “<SUA-SENHA>”
````
