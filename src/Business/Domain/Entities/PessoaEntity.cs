namespace SuporteSolidarioBusiness.Domain.Entities;

public class PessoaEntity: BaseEntity
{
    public long IdUsuario { get; set; }

    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    
    // Endereço
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Endereco { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Dados para contato
    public string DddTelefone { get; set; }
    public string Telefone { get; set; }
    public string DddCelular { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }

    
    // Dados de controle
    public bool Ativo { get; set; }
    public bool PossuiCoordenadasGps { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime Alteracao { get; set; }
    public DateTime? Inativacao { get; set; }
}