using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Connection
{
    public abstract class RepositoryBase
    {
        protected readonly SqlConnection _conn;

        public RepositoryBase(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conn = new SqlConnection(connectionString);
        }
    }
}
