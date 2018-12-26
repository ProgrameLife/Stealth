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
    public class PostgreEmailProvider : IEmailProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public PostgreEmailProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// get all emailSetting
        /// </summary>
        /// <param name="pageIndex">page index</param>
        /// <returns></returns>
        public (List<EmailSetting> list, int total) GetAllEmailSetting(int pageIndex = 1)
        {
            var sql = $"select * from emailsettings order by id  limit 10  offset  {(pageIndex - 1) * 10}";
            List<EmailSetting> list = null;
            using (var con = new NpgsqlConnection(_connectionString))
            {
                list = con.Query<EmailSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from emailsettings";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                total = con.ExecuteScalar<int>(sql);
            }
            return (list, total);
        }
        /// <summary>
        /// get a emailsetting by keyname
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        public EmailSetting GetEmailSetting(string keyName)
        {
            var sql = "select * from emailsettings where keyname=@keyname and  validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<EmailSetting>(sql, new { keyname = keyName }).FirstOrDefault();
            }
        }

        /// <summary>
        /// get all emailSetting
        /// </summary>
        /// <returns></returns>
        public List<EmailSetting> GetEmailSettings()
        {
            var sql = "select * from emailsettings where validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<EmailSetting>(sql).ToList();
            }
        }
        /// <summary>
        /// add emailSetting
        /// </summary>
        /// <param name="emailSetting">emailSetting</param>
        /// <returns></returns>
        public bool AddEmailSetting(EmailSetting emailSetting)
        {
            var sql = @"INSERT INTO public.emailsettings(keyname,
	host, port, fromaddresses, username, password, subject, body, toaddresses, iscompress, validate, compresspassword)
	VALUES (@keyname,@host, @port, @fromaddresses, @username, @password, @subject, @body, @toaddresses, @iscompress, @validate, @compresspassword);";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, emailSetting) > 0;
            }
        }
        /// <summary>
        /// modify emailSetting
        /// </summary>
        /// <param name="emailSetting">emailSetting</param>
        /// <returns></returns>
        public bool ModifyEmailSetting(EmailSetting emailSetting)
        {
            var sql = @"UPDATE public.emailsettings
	SET  host=@host, port=@port, fromaddresses = @fromaddresses, username=@username, password=@password, subject=@subject, body=@body,
	toaddresses=@toaddresses, iscompress=@iscompress, validate=@validate, compresspassword=@compresspassword
	WHERE id=@id;";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, emailSetting) > 0;
            }
        }
        /// <summary>
        /// remove emailSetting
        /// </summary>
        /// <param name="id">emailSetting id</param>
        /// <returns></returns>
        public bool RemoveEmailSetting(int id)
        {
            var sql = @"DELETE FROM emailsettings	WHERE id=@id";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}
