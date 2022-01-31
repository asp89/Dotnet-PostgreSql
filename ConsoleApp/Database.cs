using System;
using System.Data;
using Npgsql;

namespace ConsoleApp
{
    public class Database
    {
        private const string connectionString = "Host=localhost;Username=postgres;Password=0809;Database=LearnPostgres";

        public Database()
        {

        }
        public NpgsqlConnection CreateConnection()  
        {  
            var conn = new NpgsqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;  
        }

        public T ExecuteScalar<T>(T commandText)
        {
            using (var connection = CreateConnection())
            {
                return ExecuteScalar(connection, commandText);
            }
        }

        private T ExecuteScalar<T>(NpgsqlConnection connection, T defaultValue)
        {
            using (var command = new NpgsqlCommand(defaultValue.ToString(), connection))
            using (var ds = command.ExecuteReader())
            {
                if (ds.Read())
                {
                    if (ds.IsDBNull(0))
                        return defaultValue;

                    return (T)ds.GetValue(0);
                }
            }
            return defaultValue;
        }
    }
}