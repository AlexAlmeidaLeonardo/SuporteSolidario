namespace SuporteSolidarioBusiness.Application.UseCases;

using System.Security.Authentication;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Enums;
using SuporteSolidarioBusiness.Application.DTOs;

public class AdicionarClienteUseCase
{
    private readonly string _tokenAtual;
    private readonly ITokenService _tokenService;
    private readonly IClienteRepository _repo;
    private readonly ClienteEntity _input;
    private readonly IGeoLocalizacaoService _geolocalizacaoService;

    public AdicionarClienteUseCase(string tokenAtual, IGeoLocalizacaoService geolocalizacaoService, ITokenService tokenService, IClienteRepository repo, ClienteEntity input)
    {
        _tokenAtual = tokenAtual;
        _tokenService = tokenService;
        _repo = repo;
        _input = input;
        _geolocalizacaoService = geolocalizacaoService;
    }

    public ClienteEntity Execute()
    {
        // Recupera os dados do usuário através do token do usuário logado
        UsuarioEntity usuarioDto = _tokenService.GetUsuarioDTO(_tokenAtual);
        if(usuarioDto is null)
        {
            throw new AuthenticationException("Efetue o login antes de executar essa operação!");
        }

        // Verifica se o usuário é do tipo correto
        if (usuarioDto.TipoDeUsuario == TipoUsuario.Colaborador)
        {
            throw new Exception($"Usuário \"{usuarioDto.Login}\" é do tipo \"Colaborador\".");
        }

        // Verifica se o usuario logado já possui cadastro (neste caso, se ele tem cadastro de cliente)
        bool possuiCadastro = _repo.ExisteLogin(usuarioDto.Id);
        if(possuiCadastro)
        {
            throw new Exception($"Usuário \"{usuarioDto.Login}\" já possui cadastro no sistema.");
        }

        // Verifica se o endereço informado é válido
        if(String.IsNullOrEmpty(_input.Endereco) || String.IsNullOrEmpty(_input.Logradouro) || String.IsNullOrEmpty(_input.Bairro) || String.IsNullOrEmpty(_input.Cidade) || String.IsNullOrEmpty(_input.Estado))
        {
            throw new Exception("É necessário informar um endereço completo.");
        }

        // Tenta obter as coordenadas do endereço informado
        try
        {
            string endereco = _input.Logradouro + " " + _input.Endereco + ", " + _input.Numero + ", " + _input.Bairro + ", " + _input.Cidade + ", " + _input.Estado;
            
            // Preenche as coordenadas do endereço
            GeoLocalizacaoDTO geolocalizacaoDTO = _geolocalizacaoService.GetGeolocalizacao(endereco);
            _input.Latitude = geolocalizacaoDTO.Latitude;
            _input.Longitude = geolocalizacaoDTO.Longitude;
            _input.PossuiCoordenadasGps = true;
        }
        catch(Exception e)
        {
            // Marcar registro como pendente de coordenadas GPS
            _input.PossuiCoordenadasGps = false;
        }

        // Preenche os dados necessários para o cadastro
        _input.IdUsuario = usuarioDto.Id;
        _input.Ativo = true;
        _input.Email = String.IsNullOrEmpty(_input.Email) ? usuarioDto.Email :_input.Email;
        _input.Criacao = DateTime.Now;
        _input.Alteracao = _input.Criacao;
        
        ClienteEntity retorno = _repo.Adicionar(_input);
        return retorno;
    }
}