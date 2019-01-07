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
    /// sqlserver provider backhandle settings
    /// </summary>
    public class SqlServerEmailProvider : IEmailProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;

        public SqlServerEmailProvider(IConfiguration configuration)
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
            var sql = $@"select top 10 * from(
select  ROW_NUMBER() OVER (  ORDER BY id) AS rownum ,* from [emailsettings] 
)a where rownum>{(pageIndex - 1) * 10}";           
            List<EmailSetting> list = null;
            using (var con = new SqlConnection(_connectionString))
            {
                list = con.Query<EmailSetting>(sql).ToList();
            }
            int total = 0;
            sql = $"select count(*) from emailsettings";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = "select * from emailsettings where keyname=@keyname and  validate=1";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = "select * from emailsettings where validate=1";
            using (var con = new SqlConnection(_connectionString))
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
            var sql = @"INSERT INTO emailsettings(
	host, port, fromaddress, username, password, subject, body, toaddresses, iscompress, validate, compresspassword)
	VALUES (@host, @port, @fromaddress, @username, @password, @subject, @body, @toaddresses, @iscompress, @validate, @compresspassword);";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, emailSetting) > 0;
            }
        }
        /// <summary>
        /// modify emailSetting
        /// </summary>
        /// <param name="emailSetting">emailSetting</param>
        /// <returns></returns>
        public bool ModifyEmailSetting(EmailSetting  emailSetting)
        {
            var sql = @"UPDATE emailsettings
	SET  host=@host, port=@port, fromaddress=@fromaddress, username=@username, password=@password, subject=@subject, body=@body,
	toaddresses=@toaddresses, iscompress=@iscompress, validate=@validate, compresspassword=@compresspassword
	WHERE id=@id;";
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, new { id }) > 0;
            }
        }
    }
}
