using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Application.DTOs;
using Microsoft.Extensions.Configuration;

public class GeoLocalizacaoGoogleService : IGeoLocalizacaoService
{

    private readonly IConfigurationRoot _configRoot;
    private const double RaioTerra = 6371.0; // Raio da Terra em quilômetros

    public GeoLocalizacaoGoogleService(IConfiguration configuration)
    {
        _configRoot = (IConfigurationRoot)configuration;
    }

    public GeoLocalizacaoDTO GetGeolocalizacao(string endereco)
    {
        var apiKey = _configRoot["Api:ApiKey"];

        string requestUri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + endereco + "&key=" + apiKey;

        WebRequest request = WebRequest.Create(requestUri);
        WebResponse response = request.GetResponse();

        Stream stream = response.GetResponseStream();

        // Criar um StreamReader para ler o stream
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);

        string jsonString = "";

        // Ler o stream byte a byte
        int byteValue;
        while ((byteValue = reader.Read()) != -1)
        {
            jsonString += (char)byteValue;
        }

        reader.Close();

        if (String.IsNullOrEmpty(jsonString))
        {
            throw new Exception("Erro ao obter geolocalização: JSON nulo!" );
        }

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };

        RootObject? rootObject = JsonConvert.DeserializeObject<RootObject>
        (
            jsonString,
            new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }
        );

        if(rootObject == null)
        {
            throw new Exception("Erro ao obter geolocalização: RootObject nulo!" );
        }

        if (rootObject.Status != "OK")
        {
            throw new Exception("Erro ao obter geolocalização: " + rootObject.Status);
        }

        if(rootObject.Results == null || rootObject.Results.Count == 0)
        {
            throw new AddressNotFoundException($"Endereço \"{endereco}\" não encontrado!");
        }

        GeoLocalizacaoDTO geolocalizacao = new GeoLocalizacaoDTO();
        geolocalizacao.Latitude = rootObject.Results[0].Geometry.Location.Lat;
        geolocalizacao.Longitude = rootObject.Results[0].Geometry.Location.Lng;

        return geolocalizacao;
    }


    public double GetDistancia(GeoLocalizacaoDTO ponto1, GeoLocalizacaoDTO ponto2)
    {
        // Convertendo latitudes e longitudes para radianos
        double lat1 = Radianos(ponto1.Latitude);
        double lon1 = Radianos(ponto1.Longitude);
        double lat2 = Radianos(ponto2.Latitude);
        double lon2 = Radianos(ponto2.Longitude);

        // Diferença de latitudes e longitudes
        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        // Fórmula da Haversine para calcular a distância
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));

        // Retornando a distância em quilômetros
        return RaioTerra * c;
    }

    private double Radianos(double graus)
    {
        return graus * Math.PI / 180;
    }

    public List<GeoLocalizacaoDTO> GetLocalizacoesAleatorias(int quantidade)
    {
        const double minLat = -90, maxLat = 90, minLon = -180, maxLon = 180;
        Random random = new Random();

        List<GeoLocalizacaoDTO> coordenadas = new List<GeoLocalizacaoDTO>();

        for (int i = 0; i < quantidade; i++)
        {
            double latitudeAleatoria = GerarValorAleatorio(random, minLat, maxLat);
            double longitudeAleatoria = GerarValorAleatorio(random, minLon, maxLon);

            GeoLocalizacaoDTO coordenada = new GeoLocalizacaoDTO()
            {
                Latitude = latitudeAleatoria,
                Longitude = longitudeAleatoria
            };

            coordenadas.Add(coordenada);
        }

        return coordenadas;
    }

    /// <summary>
    /// Gera um valor aleatório dentro de um intervalo especificado.
    /// </summary>
    /// <param name="min">Valor mínimo (inclusivo).</param>
    /// <param name="max">Valor máximo (exclusivo).</param>
    /// <returns>Valor aleatório dentro do intervalo.</returns>
    private double GerarValorAleatorio(Random random, double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }

}

public class AddressComponent
{
    public string LongName { get; set; }
    public string ShortName { get; set; }
    public List<string> Types { get; set; }
}

public class Geometry
{
    public Bounds Bounds { get; set; }
    public Location Location { get; set; }
    public string LocationType { get; set; }
    public Viewport Viewport { get; set; }
}

public class Bounds
{
    public Location Northeast { get; set; }
    public Location Southwest { get; set; }
}

public class Location
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class Viewport
{
    public Location Northeast { get; set; }
    public Location Southwest { get; set; }
}

public class Result
{
    public List<AddressComponent> AddressComponents { get; set; }
    public string FormattedAddress { get; set; }
    public Geometry Geometry { get; set; }
    public string PlaceId { get; set; }
    public List<string> Types { get; set; }
}

public class RootObject
{
    public List<Result> Results { get; set; }
    public string Status { get; set; }
}
