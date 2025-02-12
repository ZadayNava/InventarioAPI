using Microsoft.EntityFrameworkCore;
namespace Inventario.Data
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options)
        : base(options)
        {
        }

    }
}
