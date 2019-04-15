using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SecurityMonitor.Core.Models;

namespace SecurityMonitor.Data.Repository
{
    public class AlarmRepository : BaseRepository, IAlarmRepository
    {
        public AlarmRepository(IConfiguration config)
            : base(config)
        {
        }

        public static class StoredProcedures
        {
            public const string GetAllLatest = "usp_GetAllLatestAlarms";
        }

        public async Task<IEnumerable<Alarm>> GetAllLatest()
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                return await conn.QueryAsync<Alarm>(StoredProcedures.GetAllLatest, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
