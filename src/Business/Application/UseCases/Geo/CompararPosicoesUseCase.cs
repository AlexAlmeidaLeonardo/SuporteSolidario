namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Services;

public class CompararPosicoesUseCase
{
    private readonly GeoLocalizacaoDTO _origem, _destino;
    private readonly IGeoLocalizacaoService _geolocalizacaoService;

    public CompararPosicoesUseCase(GeoLocalizacaoDTO origem, GeoLocalizacaoDTO destino, IGeoLocalizacaoService geolocalizacaoService)
    {
        _origem = origem;
        _destino = destino;
        _geolocalizacaoService = geolocalizacaoService;
    }

    public double Execute()
    {
        var distancia = _geolocalizacaoService.GetDistancia(_origem, _destino);
        return distancia;
    }

}