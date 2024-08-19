using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Connection
{
    public class RepositoryBase : IDisposable
    {
        private readonly IDbConnection _conn;

        public RepositoryBase(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _conn = new SqlConnection(connectionString);
        }

        public IDbConnection Connection
        {
            get
            {
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }

                return _conn;
            }
        }

        public void Dispose()
        {
            _conn?.Dispose();
        }
    }
}
