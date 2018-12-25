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
    public class SqlServerQuartzProvider : IQuartzProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;
        public SqlServerQuartzProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddQuartzSetting(QuartzEntity quartzEntity)
        {
            throw new System.NotImplementedException();
        }

        public List<QuartzEntity> GetAllQuartzSetting()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        public List<QuartzEntity> GetQuartzSetting()
        {
            var sql = "select * from quartzsettings where validate=true";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<QuartzEntity>(sql).ToList();
            }
        }

        public bool ModifyQuartzSetting(QuartzEntity quartzEntity)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveQuartzSetting(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
