using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SealthModel;
using SealthProvider;
using StealthQuartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgresql quartz provider
    /// </summary>
    public partial class PostgreQuartzProvider : IQuartzProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;
        public PostgreQuartzProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// add QuartzSetting
        /// </summary>
        /// <param name="quartzSetting"></param>
        /// <returns></returns>
        public bool AddQuartzSetting(QuartzSetting quartzSetting)
        {
            var sql = @"INSERT INTO public.quartzsettings(
	 keyname, typename, cronexpression, validate,createon)
	VALUES (@keyname, @typename, @cronexpression, @validate,@createon);";
            using (var con = new NpgsqlConnection(_connectionString))
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
            var sql = $"select * from quartzsettings order by id  limit 10  offset  {(pageIndex - 1) * 10}";
            List<QuartzSetting> list = null;
            using (var con = new NpgsqlConnection(_connectionString))
            {
                list = con.Query<QuartzSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from quartzsettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                total = con.ExecuteScalar<int>(sql);
            }
            return (list, total);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// query QuartzSetting list
        /// </summary>
        /// <returns></returns>
        public List<QuartzSetting> GetQuartzSetting()
        {
            var sql = "select * from quartzsettings where validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
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
            var sql = @"UPDATE public.quartzsettings
	SET keyname=@keyname, typename=@typename, cronexpression=@cronexpression, validate=@validate
	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
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
            var sql = @"DELETE FROM public.quartzsettings where id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}
