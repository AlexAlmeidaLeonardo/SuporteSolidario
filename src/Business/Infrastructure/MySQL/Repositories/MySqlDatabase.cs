using SuporteSolidarioBusiness.Application.Repositories;

namespace SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

public class MySqlDatabase : IDatabase
{
    private readonly SuporteSolidarioDbContext _context;

    public MySqlDatabase(SuporteSolidarioDbContext context)
    {
        _context = context;
    }

    public bool ServidorExiste()
    {
        return _context.Database.CanConnect();
    }

    public bool Criar()
    {
        return _context.CreateDatabase();
    }

    public bool DatabaseExiste()
    {
        return _context.DatabaseExists();
    }
}