﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using SealthModel;
using SealthProvider;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// SqlServer data provider
    /// </summary>
    public class SqlServerDataProvider : IDataProvider
    {
        /// <summary>
        /// SqlServer connection string
        /// </summary>
        readonly string _connectionString;

        public SqlServerDataProvider(IConfiguration configuration)
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
            var sql = @"INSERT INTO public.datasqls(
    id, keyname, datasettingid, sql, transactionno, groupno, validate, createon)
	VALUES(@keyname, @datasettingid, @sql, @transactionno, @groupno, @validate); ";
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<DataSetting>(sql).ToList();
            }
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
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<DataSetting>(sql, new { keyname = keyName });
            }
        }
        /// <summary>
        /// modify DataSetting
        /// </summary>
        /// <param name="dataSetting"></param>
        /// <returns></returns>
        public bool ModifyDataSetting(DataSetting dataSetting)
        {
            var sql = @"UPDATE public.datasqls
	 keyname=@keyname, datasettingid=@datasettingid, sql=@sql, transactionno=@transactionno, groupno=@groupno, validate=@validate
	WHERE id=@id;";
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}