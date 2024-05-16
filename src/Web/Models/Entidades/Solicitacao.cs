public class Solicitacoes: BaseModel
{
    public long id_cliente { get; set; }
    public long id_servico { get; set; }
    public string data { get; set; }
    public string detalhes { get; set; }
    public string data_servico { get; set; }
    public bool em_aberto { get; set; }
    public string bairro { get; set; }
    public string cep { get; set; }
    public string cidade { get; set; }
    public string endereco { get; set; }
    public string estado { get; set; }
    public string logradouro { get; set; }
    public long latitude { get; set; }
    public long longitude { get; set; }
    public double numero { get; set; }
}