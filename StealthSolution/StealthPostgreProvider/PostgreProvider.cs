namespace StealthPostgreProvider
{

    public class PostgreProvider
    {
        readonly string _connectionString;
        public PostgreProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
