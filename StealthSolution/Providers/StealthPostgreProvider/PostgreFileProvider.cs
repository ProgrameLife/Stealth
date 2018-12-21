using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SealthModel;
using SealthProvider;
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
        public List<FileSetting> GetAllFileSetting()
        {
            var sql = "select * from filesettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<FileSetting>(sql).ToList();
            }
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
            var sql = @"INSERT INTO public.filesettings(keyname, filename, filepath, fileencoding, validate)
	VALUES (@keyname, @filename, @filepath, @fileencoding, @validate);";
            using (var con = new NpgsqlConnection(_connectionString))
            {
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
