namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Enums;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Entities;
using System;
using  Microsoft.Extensions.DependencyInjection;

public class InicializarDatabaseUseCase
{
    private readonly IServiceProvider _serviceProvider;

    public InicializarDatabaseUseCase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Execute()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            IDatabase _database = scope.ServiceProvider.GetRequiredService<IDatabase>();

            /*
            // Verifica se o servidor de banco de dados está rodando
            bool retorno = _database.ServidorExiste();
            if(!retorno)
            {
                throw new Exception("Não foi possível conectar-se ao servidor de banco de dados!");
            }
            */
            
            // Verifica se o banco de dados existe
            bool retorno = _database.DatabaseExiste();
            if(! retorno)
            {
                retorno =_database.Criar();
                if (!retorno)
                    throw new Exception("Não foi possível criar a base de dados!");
            }

            ICryptoService _cryptoService = scope.ServiceProvider.GetRequiredService<ICryptoService>();;
            IUsuarioRepository _usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();;
        
            // Verifica se existe algum usuário cadastrado
            if(! _usuarioRepository.ExistemUsuarios())
            {
                // Se não existir nenhum, cadastra um usuário master a senha "parangaricotirimirruaro"
                UsuarioEntity usuarioMaster = new UsuarioEntity
                {
                    Login = "master",
                    Password1 = "123",
                    Password2 = "123",
                    Email = "aal2005@gmail.com",
                    Celular = "11993153668",
                    TipoDeUsuario = TipoUsuario.Master
                };

                AdicionarUsuarioUseCase adicionarUsuario = new AdicionarUsuarioUseCase(_cryptoService, _usuarioRepository, usuarioMaster);
                adicionarUsuario.Execute();
            }
        }
    }
}