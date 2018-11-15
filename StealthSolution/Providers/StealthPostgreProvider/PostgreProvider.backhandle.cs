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

        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        public List<SFTPSetting> GetSFTPSettings()
        {
            var sql = "select * from sftpettings where validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<SFTPSetting>(sql).ToList();
            }
        }

        public bool AddSFTPSetting(SFTPSetting sFTPSetting)
        {
            var sql = @"INSERT INTO sftpettings
		(host,port,username,password,certificatepath,transferdirectory,transferfileprefix,validate,createon)VALUES
        (@host,@port,@username,@password,@certificatepath,@transferdirectory,@transferfileprefix,@validate,@createon)";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, sFTPSetting) > 0;
            }
        }

        public bool ModifySFTPSetting(SFTPSetting sFTPSetting)
        {
            var sql = @"INSERT INTO sftpettings
		(host,port,username,password,certificatepath,transferdirectory,transferfileprefix,validate,createon)VALUES
        (@host,@port,@username,@password,@certificatepath,@transferdirectory,@transferfileprefix,@validate,@createon)";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, sFTPSetting) > 0;
            }
        }
    }
}
