using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Entities;

namespace SuporteSolidarioBusiness.Application.UseCases;

public class EfetuarLoginUseCase
{
    private readonly ICryptoService _cryptoService;
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _repo;

    private readonly LoginDTO _dto;


    public EfetuarLoginUseCase(ICryptoService cryptoService, ITokenService tokenService, IAuthRepository repo, LoginDTO dto)
    {
        _cryptoService = cryptoService;
        _tokenService = tokenService;
        _repo = repo;
        _dto = dto;
    }

    public List<Claim> Execute()
    {
        if (String.IsNullOrEmpty(_dto.Login) || String.IsNullOrEmpty(_dto.Password1))
        {
            throw new UnauthorizedAccessException("Login/senha inválido!");
        }

        HashSaltDTO hashSalt = _repo.GetSalt(_dto);
        if (hashSalt is null)
        {
            throw new UnauthorizedAccessException("Login/senha inválido!");
        }

        bool passwordCorreto = _cryptoService.VerificaPassword(_dto.Password1, hashSalt.Hash, hashSalt.Salt);
        if (!passwordCorreto)
        {
            throw new UnauthorizedAccessException("Login/senha inválido!");
        }

        UsuarioEntity usuario = _repo.GetUserByLogin(_dto.Login);
        if (usuario == null)
            throw new UnauthorizedAccessException("Login/senha inválido!");

        // Armazenando nome, id e cargo nos claims
        List<Claim> lstClaims = new List<Claim>();
        lstClaims.Add(new Claim(ClaimTypes.Name, usuario.Login));
        lstClaims.Add(new Claim("IdUsuario", usuario.Id.ToString()));
        lstClaims.Add(new Claim(ClaimTypes.Role, usuario.TipoDeUsuario.ToString()));

        return lstClaims;
    }

}