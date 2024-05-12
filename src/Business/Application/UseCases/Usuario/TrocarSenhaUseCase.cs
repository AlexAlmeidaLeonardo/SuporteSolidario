namespace SuporteSolidarioBusiness.Application.UseCases;

using System.Security.Policy;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;

public class TrocarSenhaUseCase
{
    private ICryptoService _cryptoService;
    private IUsuarioRepository _repo;
    private LoginDTO _dto;

    public TrocarSenhaUseCase(ICryptoService cryptoService, IUsuarioRepository repo, LoginDTO dto)
    {
        this._cryptoService = cryptoService;
        this._repo = repo;
        this._dto = dto;
    }

    internal void Execute()
    {
        if(_dto.Password1 != _dto.Password2)
        {
            throw new PolicyException("Senhas não conferem!");
        }

        if(String.IsNullOrEmpty(_dto.Password1))
        {
            throw new PolicyException("Senhas inválidas!");
        }

        bool usuarioExiste = _repo.LoginExiste(_dto.Login);
        if (!usuarioExiste)
        {
            throw new PolicyException($"Usuário com login \"{_dto.Login}\" não existe!");
        }

        HashSaltDTO hashSaltDTO = _cryptoService.Crypt(_dto.Password1);

        _repo.Atualizar(_dto, hashSaltDTO);
    }
}