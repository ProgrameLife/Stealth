using Dapper;
using SealthModel;
using SealthProvider;
using StealthQuartz;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// postgresql provider
    /// </summary>
    public class SqlServerProvider : IQuartzProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;
        public SqlServerProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        public List<QuartzEntity> GetQuartzEntity()
        {
            var sql = "select * from quartzsettings where validate=true";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<QuartzEntity>(sql).ToList();
            }
        }
    }
}
