using Dapper;
using Npgsql;
namespace StealthPostgreProvider
{

    public class PostgreProvider
    {
        readonly string _connectionString;
        public PostgreProvider(string connectionString)
        {
            _connectionString = connectionString;
            InitTable();
        }
        /// <summary>
        /// init table
        /// </summary>
        /// <returns></returns>
        public bool InitTable()
        {
            var connBuilder = new NpgsqlConnectionStringBuilder(_connectionString);
            connBuilder.Database = "postgres";
            using (var con = new NpgsqlConnection(connBuilder.ConnectionString))
            {
                var selectDbSql = "SELECT count(*)  FROM pg_catalog.pg_database where datname='StealthDB';"
                var result = con.ExecuteScalar<int>(selectDbSql);
                if (result == 0)
                {
                    var createDBSql = "CREATE DATABASE StealthDB OWNER postgres;";
                    con.Execute(createDBSql);
                }
            }
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var selectSql = "select count(*) as sl from pg_class where relname = 'tablename';";
                var result = con.ExecuteScalar<int>(selectSql);
                if (result == 0)
                {
                    var createTableSql = "";
                    con.Execute(createTableSql);
                }
            }
            return true;
        }
    }
}
