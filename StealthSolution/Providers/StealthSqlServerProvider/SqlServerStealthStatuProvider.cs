using Dapper;
using Microsoft.Extensions.Configuration;
using SealthModel;
using SealthProvider;
using StealthQuartz;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// sqlserver provider status setting
    /// </summary>
     public class SqlServerStealthStatuProvider : IStealthStatuProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;
        public SqlServerStealthStatuProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        /// <summary>
        /// get all stealths statu
        /// </summary>
        /// <returns></returns>
        public List<StealthStatu> GetAllStealthsStatus()
        {
            var sql = "select * from stealthstatus";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<StealthStatu>(sql).ToList();
            }
        }

        /// <summary>
        /// set status success
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool SetSuccess(string keyName)
        {
            return SetStatus(keyName, 2);
        }
        /// <summary>
        /// set status start
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool SetStart(string keyName)
        {
            return SetStatus(keyName, 1);
        }
        /// <summary>
        /// set status failed
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool SetFailed(string keyName)
        {
            return SetStatus(keyName, 3);
        }
        /// <summary>
        /// set status
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        bool SetStatus(string keyName, int status)
        {
            var sql = @"UPDATE stealthstatus SET status=@status, modifytime=@modifytime WHERE keyname=@keyname;
INSERT INTO stealthstatus(keyname,status,modifytime) SELECT @keyname,@status,now()
WHERE NOT EXISTS(SELECT 1 FROM stealthstatus WHERE keyname=@keyname);";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, new { keyname = keyName, status = status }) > 0;
            }
        }       
    }
}
