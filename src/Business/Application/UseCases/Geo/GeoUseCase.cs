namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Services;

public class GeoUseCase
{
    private readonly IGeoLocalizacaoService _geolocalizacaoService;
    private readonly string _endereco;

    public GeoUseCase(IGeoLocalizacaoService geolocalizacaoService, string endereco)
    {
        _geolocalizacaoService = geolocalizacaoService;
        _endereco = endereco;
    }

    public GeoLocalizacaoDTO Executar()
    {
        return _geolocalizacaoService.GetGeolocalizacao(_endereco);
    }
}