namespace SuporteSolidarioBusiness.Application.UseCases;

using SuporteSolidarioBusiness.Domain.Entities;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Application.DTOs;

public class BuscarColaboradoresMaisProximosUseCase
{

    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IGeoLocalizacaoService _geolocalizacaoService;

    private readonly ClienteEntity entity;
    private readonly double raioMaximo;


    public BuscarColaboradoresMaisProximosUseCase(IGeoLocalizacaoService geolocalizacaoService, IColaboradorRepository repoColaborador, ClienteEntity entity, double raioMaximo)
    {
        this.entity = entity;
        this.raioMaximo = raioMaximo;
        this._geolocalizacaoService = geolocalizacaoService;
        this._colaboradorRepository = repoColaborador;
    }

    public List<ColaboradorEntity> Execute()
    {
        IEnumerable<ColaboradorEntity> lstDtos = _colaboradorRepository.Todos();

        List<(long Id, double Distancia, ColaboradorEntity Colaborador)> lstDistancias = [];

        GeoLocalizacaoDTO localizacaoCliente = new GeoLocalizacaoDTO
        {
            Latitude = entity.Latitude,
            Longitude = entity.Longitude
        };

        foreach (ColaboradorEntity item in lstDtos)
        {
            GeoLocalizacaoDTO localizacaoColaborador = new GeoLocalizacaoDTO
            {
                Latitude = item.Latitude,
                Longitude = item.Longitude
            };
            
            double distancia = _geolocalizacaoService.GetDistancia(localizacaoCliente, localizacaoColaborador);

            if(distancia <= raioMaximo)
            {
                var tupla = (Id: item.Id, Distancia: distancia, item);
                lstDistancias.Add( tupla );
            }
        }

        lstDistancias = lstDistancias.OrderBy(x => x.Distancia).ToList();
        
        List<ColaboradorEntity> lstColaboradoresMaisProximos = [];
        foreach(var item in lstDistancias)
        {
            lstColaboradoresMaisProximos.Add(item.Colaborador);
        }

        return lstColaboradoresMaisProximos;
    }
}