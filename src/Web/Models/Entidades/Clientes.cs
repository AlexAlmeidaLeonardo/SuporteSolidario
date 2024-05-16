public class Cliente: BaseModel
{
    public long id_usuario { get; set; }
    public string nome { get; set; }
    public string sobrenome { get; set; }
    public string cpf { get; set; }
    public string cep { get; set; }
    public string logradouro { get; set; }
    public string endereco { get; set; }
    public double numero { get; set; }
    public string bairro { get; set; }
    public string cidade { get; set; }
    public string estado { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string ddd_telefone { get; set; }
    public string telefone { get; set; }
    public string ddd_celular { get; set; }
    public string celular { get; set; }
    public string email { get; set; }
    public bool ativo { get; set; }
    public bool possui_coordenadas_gps { get; set; }
    public string criacao { get; set; }
    public string alteracao { get; set; }
    public string inativacao { get; set; }
}