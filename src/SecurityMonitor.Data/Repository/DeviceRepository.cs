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
            public const string Get = "usp_GetDevice";
            public const string GetAll = "usp_GetAllDevices";
            public const string Insert = "usp_InsertDevice";
            public const string InsertOrUpdate = "usp_InsertOrUpdateDevice";
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

        public async Task Add(Device device)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tx = conn.BeginTransaction())
                {
                    await conn.ExecuteAsync(StoredProcedures.Insert, device, tx, commandType: CommandType.StoredProcedure);

                    tx.Commit();
                }
            }
        }

        public async Task<Device> Get(int id)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                return await conn.QueryFirstAsync<Device>(StoredProcedures.Get, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        Task<Device> IDeviceRepository.Add(Device device) => throw new System.NotImplementedException();
        public async Task AddOrUpdateAsync(int id, string name, decimal lat, decimal lng)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tx = conn.BeginTransaction())
                {
                    await conn.ExecuteAsync(StoredProcedures.InsertOrUpdate, new { Id = id, Name = name, Latitude = lat, Longitude = lng }, tx, commandType: CommandType.StoredProcedure);

                    tx.Commit();
                }
            }
        }
    }
}
