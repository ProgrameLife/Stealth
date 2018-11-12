using Dapper;
using Npgsql;
using StealthQuartz.Entity;
using System.Collections.Generic;
using System.Linq;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgresql provider backhandle settings
    /// </summary>
    partial class PostgreProvider
    {
        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        public List<SFTPSetting> GetAllSFTPSetting()
        {
            var sql = "select * from sftpettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<SFTPSetting>(sql).ToList();
            }
        }


    }
}
