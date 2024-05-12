namespace SuporteSolidarioBusiness.Application.Repositories;

public interface IDatabase
{
    bool ServidorExiste();
    
    bool DatabaseExiste();

    bool Criar();
}