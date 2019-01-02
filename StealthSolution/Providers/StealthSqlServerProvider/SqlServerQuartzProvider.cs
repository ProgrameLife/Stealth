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

        public bool AddQuartzSetting(QuartzSetting quartzEntity)
        {
            throw new System.NotImplementedException();
        }

        public (List<QuartzSetting> list, int total) GetAllQuartzSetting(int pageIndex = 1)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        public List<QuartzSetting> GetQuartzSetting()
        {
            var sql = "select * from quartzsettings where validate=true";
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<QuartzSetting>(sql).ToList();
            }
        }

        public bool ModifyQuartzSetting(QuartzSetting quartzEntity)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveQuartzSetting(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
