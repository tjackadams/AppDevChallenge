using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SecurityMonitor.Core.Models;

namespace SecurityMonitor.Data.Repository
{
    public class DeviceEventRepository : BaseRepository
    {
        public DeviceEventRepository(IConfiguration config)
            : base(config)
        {
        }

        public static class StoredProcedures
        {
            public const string GetAllLatest = "usp_GetAllLatestDeviceEvents";

        }


        public async Task<IEnumerable<DeviceEvent>> GetAllLatest()
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                return await conn.QueryAsync<DeviceEvent>(StoredProcedures.GetAllLatest, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
