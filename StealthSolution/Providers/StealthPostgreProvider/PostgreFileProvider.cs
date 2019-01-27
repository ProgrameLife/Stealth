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
    public class PostgreFileProvider : IFileProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public PostgreFileProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// get all fileSetting
        /// </summary>
        /// <returns></returns>
        public (List<FileSetting> list, int total) GetAllFileSetting(int pageIndex=1)
        {   
            var sql = $"select * from filesettings order by id  limit 10  offset  {(pageIndex - 1) * 10}";
            List<FileSetting> list = null;
            using (var con = new NpgsqlConnection(_connectionString))
            {
                list = con.Query<FileSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from filesettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                total = con.ExecuteScalar<int>(sql);
            }
            return (list, total);
        }
        /// <summary>
        /// get a fileSetting by keyname
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public FileSetting GetFileSetting(string keyName)
        {
            var sql = "select * from filesettings where keyname=@keyname and  validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<FileSetting>(sql, new { keyname = keyName }).FirstOrDefault();
            }
        }

        /// <summary>
        /// get all validate fileSetting
        /// </summary>
        /// <returns></returns>
        public List<FileSetting> GetFileSettings()
        {
            var sql = "select * from filesettings where validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<FileSetting>(sql).ToList();
            }
        }
        /// <summary>
        /// add emailSetting
        /// </summary>
        /// <param name="fileSetting">fileSetting</param>
        /// <returns></returns>
        public bool AddFileSetting(FileSetting fileSetting)
        {
            var sql = @"INSERT INTO public.filesettings(keyname, filename, filepath, fileencoding, validate,createon)
	VALUES (@keyname, @filename, @filepath, @fileencoding, @validate,@createon);";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                fileSetting.CreateOn = DateTime.Now;
                return con.Execute(sql, fileSetting) > 0;
            }
        }
        /// <summary>
        /// modify fileSetting
        /// </summary>
        /// <param name="fileSetting">fileSetting</param>
        /// <returns></returns>
        public bool ModifyFileSetting(FileSetting fileSetting)
        {
            var sql = @"UPDATE public.filesettings
	SET keyname=@keyname, filename=@filename, filepath=@filepath, fileencoding=@fileencoding, validate=@validate
	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, fileSetting) > 0;
            }
        }
        /// <summary>
        /// remove fileSetting
        /// </summary>
        /// <param name="id">fileSetting id</param>
        /// <returns></returns>
        public bool RemoveFileSetting(int id)
        {
            var sql = @"DELETE FROM filesettings	WHERE id=@id";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }   
    }
}
