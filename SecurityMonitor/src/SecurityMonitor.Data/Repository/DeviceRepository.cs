using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SecurityMonitor.Core.Models;
using Dapper;
using System.Data;

namespace SecurityMonitor.Data.Repository
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public static class StoredProcedures
        {
            public const string GetAll = "usp_GetAllDevices";
        }
        public DeviceRepository(IConfiguration config)
            : base(config)
        {
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                return await conn.QueryAsync<Device>(StoredProcedures.GetAll, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
