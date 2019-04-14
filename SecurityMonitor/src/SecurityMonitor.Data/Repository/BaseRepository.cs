using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SecurityMonitor.Data.Repository
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _config;
        public BaseRepository(IConfiguration config)
        {
            _config = config;
        }

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("Default"));
            }
        }
    }
}
