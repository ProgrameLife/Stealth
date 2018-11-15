﻿using Dapper;
using Npgsql;
using StealthQuartz;
using StealthQuartz.Entity;
using System.Collections.Generic;
using System.Linq;

namespace StealthPostgreProvider
{
    /// <summary>
    /// postgresql provider
    /// </summary>
    public partial class PostgreProvider : IDBProvider
    {
        /// <summary>
        /// postgresql connection string
        /// </summary>
        readonly string _connectionString;
        public PostgreProvider(string connectionString)
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
            using (var con = new NpgsqlConnection(_connectionString))
            {
                return con.Query<QuartzEntity>(sql).ToList();
            }
        } 



    }
}
