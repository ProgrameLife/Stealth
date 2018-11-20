using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SealthModel;
using SealthProvider;
using StealthQuartz;
using System.Collections.Generic;
using System.Linq;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgresql provider
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
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        public List<QuartzEntity> GetQuartzEntity()
        {
            var sql = "select * from quartzsettings where validate=true";
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<QuartzEntity>(sql).ToList();
            }
        }



    }
}
