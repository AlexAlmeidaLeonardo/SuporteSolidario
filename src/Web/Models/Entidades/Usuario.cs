public class Usuarios: BaseModel
{
    public double tipo_de_usuario { get; set; }
    public string login { get; set; }
    public string email { get; set; }
    public string celular { get; set; }
    public string hash { get; set; }
    public string salt { get; set; }
    public string criacao { get; set; } //quando for criado, então tempo
    public string alteracao { get; set; }
    public bool ativo { get; set; }
}