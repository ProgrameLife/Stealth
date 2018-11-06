using Npgsql;

namespace StealthPostgreProvider
{
    public class PostgreProvider
    {
        public PostgreProvider(string connectionString)
        {
            var con = new NpgsqlConnection(connectionString);
        }
    }
}
