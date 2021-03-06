﻿using Dapper;
using Microsoft.Extensions.Configuration;
using SealthModel;
using SealthProvider;
using StealthQuartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// sqlserver quartz provider
    /// </summary>
    public class SqlServerQuartzProvider : IQuartzProvider
    {
        /// <summary>
        /// sqlserver connection string
        /// </summary>
        readonly string _connectionString;
        public SqlServerQuartzProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// add QuartzSetting
        /// </summary>
        /// <param name="quartzSetting">QuartzSetting</param>
        /// <returns></returns>
        public bool AddQuartzSetting(QuartzSetting quartzSetting)
        {
            var sql = @"INSERT INTO quartzsettings(
	 keyname, typename, cronexpression, validate,createon)
	VALUES (@keyname, @typename, @cronexpression, @validate,@createon);";
            using (var con = new SqlConnection(_connectionString))
            {
                quartzSetting.CreateOn = DateTime.Now;
                return con.Execute(sql, quartzSetting) > 0;
            }
        }

        /// <summary>
        /// get all QuartzSetting
        /// </summary>
        /// <param name="pageIndex">page index</param>
        /// <returns></returns>
        public (List<QuartzSetting> list, int total) GetAllQuartzSetting(int pageIndex = 1)
        {
            var sql = $@"select top 10 * from(
select  ROW_NUMBER() OVER (  ORDER BY id) AS rownum ,* from [quartzsettings] 
)a where rownum>{(pageIndex - 1) * 10}";           
            List<QuartzSetting> list = null;
            using (var con = new SqlConnection(_connectionString))
            {
                list = con.Query<QuartzSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from quartzsettings";
            using (var con = new SqlConnection(_connectionString))
            {
                total = con.ExecuteScalar<int>(sql);
            }
            return (list, total);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        public List<QuartzSetting> GetQuartzSetting()
        {
            var sql = "select * from quartzsettings where validate=1";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<QuartzSetting>(sql).ToList();
            }
        }

        /// <summary>
        /// modify QuartzSetting
        /// </summary>
        /// <param name="quartzSetting"></param>
        /// <returns></returns>
        public bool ModifyQuartzSetting(QuartzSetting quartzSetting)
        {
            var sql = @"UPDATE quartzsettings
	SET keyname=@keyname, typename=@typename, cronexpression=@cronexpression, validate=@validate
	WHERE id=@id;";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, quartzSetting) > 0;
            }
        }
        /// <summary>
        /// delete QuartzSetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveQuartzSetting(int id)
        {
            var sql = @"DELETE FROM quartzsettings where id=@id;";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}
