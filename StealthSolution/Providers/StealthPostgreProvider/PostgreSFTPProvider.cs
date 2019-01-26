using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SealthModel;
using SealthProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgresql provider backhandle settings
    /// </summary>
    public class PostgreSFTPProvider : ISFTPProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public PostgreSFTPProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        public (List<SFTPSetting> list, int total) GetAllSFTPSetting(int pageIndex = 1)
        {
            var sql = $"select * from sftpettings order by id limit 10  offset  {(pageIndex - 1) * 10}";
            List<SFTPSetting> list = null;
            using (var con = new NpgsqlConnection(_connectionString))
            {
                list = con.Query<SFTPSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from sftpettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                total = con.ExecuteScalar<int>(sql);
            }
            return (list, total);
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
        /// get a sftpsetting
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public SFTPSetting GetSFTPSetting(string keyName)
        {
            var sql = "select * from sftpettings where validate=true and keyname=@keyname";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<SFTPSetting>(sql, new { keyname = keyName }).FirstOrDefault();
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
		(keyname,host,port,username,password,certificatepath,transferdirectory,transferfileprefix,filename,fileencoding,validate,createon)VALUES
        (@keyname,@host,@port,@username,@password,@certificatepath,@transferdirectory,@transferfileprefix,@filename,@fileencoding,@validate,@createon)";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                sFTPSetting.CreateOn = DateTime.Now;
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
	SET  keyname=@keyname,host =@host,port = @port,username = @username,password = @password,certificatepath =@certificatepath
		,transferdirectory = @transferdirectory,transferfileprefix = @transferfileprefix,filename=@filename,fileencoding=@fileencoding,validate = @validate	
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
