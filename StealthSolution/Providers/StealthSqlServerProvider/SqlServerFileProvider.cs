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
    /// SqlServer provider backhandle settings
    /// </summary>
    public class SqlServerFileProvider : IFileProvider
    {
        /// <summary>
        /// SqlServer connection string
        /// </summary>
        readonly string _connectionString;

        public SqlServerFileProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// get all fileSetting
        /// </summary>
        /// <returns></returns>
        public (List<FileSetting> list, int total) GetAllFileSetting(int pageIndex=1)
        {
            var sql = $@"select top 10 * from(
select  ROW_NUMBER() OVER (  ORDER BY id) AS rownum ,* from [filesettings] 
)a where rownum>{(pageIndex - 1) * 10}";
          
            List<FileSetting> list = null;
            using (var con = new SqlConnection(_connectionString))
            {
                list = con.Query<FileSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from filesettings";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = "select * from filesettings where keyname=@keyname and  validate=1";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = "select * from filesettings where validate=1";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = @"INSERT INTO filesettings(keyname, filename, filepath, fileencoding, validate)
	VALUES (@keyname, @filename, @filepath, @fileencoding, @validate);";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = @"UPDATE filesettings
	SET keyname=@keyname, filename=@filename, filepath=@filepath, fileencoding=@fileencoding, validate=@validate
	WHERE id=@id;";
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }   
    }
}
