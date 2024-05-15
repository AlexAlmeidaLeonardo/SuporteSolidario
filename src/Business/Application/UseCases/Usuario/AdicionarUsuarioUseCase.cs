namespace SuporteSolidarioBusiness.Application.UseCases;

using System.Security.Policy;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Entities;

public class AdicionarUsuarioUseCase
{
    private ICryptoService _cryptoService;
    private IUsuarioRepository _repo;
    private UsuarioEntity _dto;

    public AdicionarUsuarioUseCase(ICryptoService cryptoService, IUsuarioRepository usuarioRepository, UsuarioEntity entity)
    {
        this._cryptoService = cryptoService;
        this._repo = usuarioRepository;
        this._dto = entity;
    }

    public void Execute()
    {
        if(_dto.Password1 != _dto.Password2)
        {
            throw new PolicyException("Senhas não conferem!");
        }

        if(String.IsNullOrEmpty(_dto.Password1))
        {
            throw new PolicyException("Senhas inválidas!");
        }

        if(_dto.Login == "master" && _repo.ExistemUsuarios())
        {
            throw new PolicyException("Login inválido!");
        }

        bool jaExiste = _repo.LoginExiste(_dto.Login);
        if (jaExiste)
        {
            throw new PolicyException($"Usuário com login \"{_dto.Login}\" já existe!");
        }

        HashSaltDTO hashSaltDTO = _cryptoService.Crypt(_dto.Password1);

        _dto.Ativo = true;

        _repo.Adicionar(_dto, hashSaltDTO);
    }
}