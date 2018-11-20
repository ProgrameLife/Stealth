using Dapper;
using Microsoft.Extensions.Configuration;
using SealthModel;
using SealthProvider;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// postgresql provider backhandle settings
    /// </summary>
    public class SqlServerSFTPProvider : ISFTPProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public SqlServerSFTPProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        public List<SFTPSetting> GetAllSFTPSetting()
        {

            var sql = "select * from sftpettings";
            using (var con = new SqlConnection(_connectionString))
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
        /// <summary>
        /// add sftpsetting
        /// </summary>
        /// <param name="sFTPSetting">sftpsetting</param>
        /// <returns></returns>
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
        /// <summary>
        /// modify sftpsetting
        /// </summary>
        /// <param name="sFTPSetting">sftpsetting</param>
        /// <returns></returns>
        public bool ModifySFTPSetting(SFTPSetting sFTPSetting)
        {
            var sql = @"UPDATE sftpettings
	SET  host =@host,port = @port,username = @username,password = @password,certificatepath =@certificatepath
		,transferdirectory = @transferdirectory,transferfileprefix = @transferfileprefix,validate = @validate	
	WHERE id=@id";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, sFTPSetting) > 0;
            }
        }
        /// <summary>
        /// remove sftpsetting
        /// </summary>
        /// <param name="id">sftpsetting id</param>
        /// <returns></returns>
        public bool RemoveSFTPSetting(int id)
        {
            var sql = @"DELETE FROM sftpettings	WHERE id=@id";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}
