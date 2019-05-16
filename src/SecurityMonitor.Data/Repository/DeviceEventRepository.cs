using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SecurityMonitor.Core;
using SecurityMonitor.Core.Models;

namespace SecurityMonitor.Data.Repository
{
    public class DeviceEventRepository : BaseRepository, IDeviceEventRepository
    {
        public DeviceEventRepository(IConfiguration config)
            : base(config)
        {
        }

        public static class StoredProcedures
        {
            public const string GetAllLatest = "usp_GetAllLatestDeviceEvents";
            public const string Insert = "usp_InsertDeviceEvent";
        }


        public async Task<IEnumerable<DeviceEvent>> GetAllLatest()
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                return await conn.QueryAsync<DeviceEvent>(StoredProcedures.GetAllLatest, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task AddAsync(int deviceId, Guid id, DateTimeOffset eventTime, Status status)
        {
            using(var conn = Connection)
            {
                await conn.OpenAsync();

                using(var tx = conn.BeginTransaction())
                {
                    await conn.ExecuteAsync(StoredProcedures.Insert, new { DeviceId = deviceId, Id = id, EventTime = eventTime, Status = status },tx, commandType: CommandType.StoredProcedure);

                    tx.Commit();
                }
            }
        }
    }
}
