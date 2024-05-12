namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Application.DTOs;
using SuporteSolidarioBusiness.Application.Services;

public class TesteLocalizacoesUseCase
{
    private readonly IGeoLocalizacaoService _geolocalizacaoService;
    private readonly int _quantidade;
    private readonly GeoLocalizacaoDTO _geolocalizacaoDTO;


    public TesteLocalizacoesUseCase(IGeoLocalizacaoService geolocalizacaoService, GeoLocalizacaoDTO geolocalizacaoDTO, int quantidade)
    {
        _geolocalizacaoService = geolocalizacaoService;
        _quantidade = quantidade;
        _geolocalizacaoDTO = geolocalizacaoDTO;    
    }


    public List<PosicaoRelativa> Execute()
    {
        List<GeoLocalizacaoDTO> lstLocalizacoes = _geolocalizacaoService.GetLocalizacoesAleatorias(_quantidade);
        List<PosicaoRelativa> lstPosicoesRelativas = new List<PosicaoRelativa>();

        foreach (GeoLocalizacaoDTO item in lstLocalizacoes)
        {
            double distancia = _geolocalizacaoService.GetDistancia(_geolocalizacaoDTO, item);
            PosicaoRelativa posicaoRelativa = new PosicaoRelativa(item);
            posicaoRelativa.Distancia = distancia;
            lstPosicoesRelativas.Add(posicaoRelativa);
        }

        lstPosicoesRelativas = lstPosicoesRelativas.OrderBy(x => x.Distancia)
                                                   //.Take(20)
                                                   .ToList();

        return lstPosicoesRelativas;
     }


    public class PosicaoRelativa: GeoLocalizacaoDTO
    {
        public PosicaoRelativa(GeoLocalizacaoDTO geolocalizacaoDTO)
        {
            this.Latitude = geolocalizacaoDTO.Latitude;
            this.Longitude = geolocalizacaoDTO.Longitude;
        }

        public double Distancia { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}