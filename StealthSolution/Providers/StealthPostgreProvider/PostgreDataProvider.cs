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
        /// <summary>
        /// add DataSetting
        /// </summary>
        /// <param name="dataSetting">DataSetting</param>
        /// <returns>Returns whether the add result was successful,result is true or false</returns>
        public bool AddDataSetting(DataSetting dataSetting)
        {
            var sql = @"INSERT INTO public.datasettings(
     keyname,connectionstrings , validate)
	VALUES(@keyname,@connectionstrings , @validate); ";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, dataSetting) > 0;
            }
        }
        /// <summary>
        /// get all DataSetting
        /// </summary>
        /// <returns></returns>
        public List<DataSetting> GetAllDataSetting()
        {
            var sql = @"select * from datasettings;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<DataSetting>(sql).ToList();
            }
        }
        /// <summary>
        /// get validate DataSettings
        /// </summary>
        /// <returns></returns>
        public List<DataSetting> GetDataSettings()
        {
            var sql = @"select * from datasettings where validate=true;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<DataSetting>(sql).ToList();
            }
        }
        /// <summary>
        /// modify DataSetting
        /// </summary>
        /// <param name="dataSetting"></param>
        /// <returns></returns>
        public bool ModifyDataSetting(DataSetting dataSetting)
        {
            var sql = @"UPDATE public.datasettings
	 datasettingid=@datasettingid, sql=@sql, transactionno=@transactionno, groupno=@groupno, validate=@validate
	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, dataSetting) > 0;
            }
        }
        /// <summary>
        /// remove DataSetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveDataSetting(int id)
        {
            var sql = @"DELETE FROM public.datasqls	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }

        #region datasql
        /// <summary>
        /// add DataSetting
        /// </summary>
        /// <param name="dataSetting">DataSetting</param>
        /// <returns>Returns whether the add result was successful,result is true or false</returns>
        public bool AddDataSql(DataSql dataSql)
        {
            var sql = @"INSERT INTO public.datasqls(
    id, keyname, datasettingid, sql, transactionno, groupno, validate, createon)
	VALUES(@keyname, @datasettingid, @sql, @transactionno, @groupno, @validate); ";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, dataSql) > 0;
            }
        }

        /// <summary>
        /// get all DataSetting
        /// </summary>
        /// <returns></returns>
        public List<DataSql> GetAllDataSql()
        {
            var sql = @"select * from datasqls;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<DataSql>(sql).ToList();
            }
        }
        /// <summary>
        /// get validate DataSettings
        /// </summary>
        /// <returns></returns>
        public List<DataSql> GetDataSqls()
        {
            var sql = @"select * from datasqls where validate=true;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<DataSql>(sql).ToList();
            }
        }
        /// <summary>
        /// modify DataSetting
        /// </summary>
        /// <param name="dataSetting"></param>
        /// <returns></returns>
        public bool ModifyDataSql(DataSql dataSql)
        {
            var sql = @"UPDATE public.datasqls
	 datasettingid=@datasettingid, sql=@sql, transactionno=@transactionno, groupno=@groupno, validate=@validate
	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, dataSql) > 0;
            }
        }
        /// <summary>
        /// remove DataSetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveDataSql(int id)
        {
            var sql = @"DELETE FROM public.datasqls	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }

        #endregion


        /// <summary>
        /// get datasetting and datasql
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public DataSettingSql GetDataSettingSqls(string keyName)
        {
            var sql = @"select datasettings.*,datasqls.id as sqlid,datasqls.sql,datasqls.transactionno,
datasqls.validate as sqlvalidate,datasqls.createon as sqlcreateon from datasettings join datasqls
on datasettings.id=datasqls.datasettingid and datasettings.validate=true
and datasqls.validate=true
where datasqls.keyname=@keyname;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<DataSettingSql>(sql, new { keyname = keyName });
            }
        }
    }
}
