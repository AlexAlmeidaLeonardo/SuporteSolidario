namespace SuporteSolidarioBusiness.Application.UseCases;

using System.Security.Authentication;
using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Domain.Enums;
using SuporteSolidarioBusiness.Application.DTOs;

public class AdicionarColaboradorUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IColaboradorRepository _repo;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ColaboradorEntity _input;
    private readonly IGeoLocalizacaoService _geolocalizacaoService;

    public AdicionarColaboradorUseCase(IGeoLocalizacaoService geolocalizacaoService, ITokenService tokenService, IColaboradorRepository repo, IUsuarioRepository usuarioRepository, ColaboradorEntity input)
    {
        _tokenService = tokenService;
        _repo = repo;
        _usuarioRepository = usuarioRepository;
        _input = input;
        _geolocalizacaoService = geolocalizacaoService;
    }

    public ColaboradorEntity Execute()
    {
        UsuarioEntity usuarioEntity = _usuarioRepository.Ler(_input.IdUsuario);

        // Verifica se o usuário é do tipo correto
        if (usuarioEntity.TipoDeUsuario == TipoUsuario.Cliente)
        {
            throw new Exception($"Usuário \"{usuarioEntity.Login}\" é do tipo \"Cliente\".");
        }

        // Verifica se o usuario logado já possui cadastro (neste caso, se ele tem cadastro de cliente)
        bool possuiCadastro = _repo.ExisteLogin(usuarioEntity.Id);
        if(possuiCadastro)
        {
            throw new Exception($"Usuário \"{usuarioEntity.Login}\" já possui cadastro no sistema.");
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
        _input.IdUsuario = usuarioEntity.Id;
        _input.Ativo = true;
        _input.Email = String.IsNullOrEmpty(_input.Email) ? usuarioEntity.Email : _input.Email;
        _input.Criacao = DateTime.Now;
        _input.Alteracao = _input.Criacao;

        ColaboradorEntity retorno = _repo.Adicionar(_input);

        return retorno;
    }
}