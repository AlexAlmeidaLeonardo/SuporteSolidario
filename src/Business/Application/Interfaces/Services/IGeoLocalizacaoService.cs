namespace SuporteSolidarioBusiness.Application.Services;

using SuporteSolidarioBusiness.Application.DTOs;

public interface IGeoLocalizacaoService
{
    GeoLocalizacaoDTO GetGeolocalizacao(string endereco);

    double GetDistancia(GeoLocalizacaoDTO ponto1, GeoLocalizacaoDTO ponto2);

    List<GeoLocalizacaoDTO> GetLocalizacoesAleatorias(int quantidade);
}