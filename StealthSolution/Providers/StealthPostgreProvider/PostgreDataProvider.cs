using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SealthModel;
using SealthProvider;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgre data provider
    /// </summary>
    public class PostgreDataProvider : IDataProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public PostgreDataProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public bool AddDataSetting(DataSetting dataSetting)
        {
            throw new System.NotImplementedException();
        }

        public List<DataSetting> GetAllDataSetting()
        {
            throw new System.NotImplementedException();
        }

        public List<DataSetting> GetDataSettings()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// get datasettings 
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public IEnumerable<DataSetting> GetDataSettings(string keyName)
        {
            var sql = @"select datasettings.*,datasqls.sql,datasqls.transactionno from datasettings join datasqls
on datasettings.id=datasqls.datasettingid and datasettings.validate=true
and datasqls.validate=true
where datasqls.keyname=@keyname;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<DataSetting>(sql, new { keyname = keyName });
            }
        }

        public bool ModifyDataSetting(DataSetting dataSetting)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveDataSetting(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
