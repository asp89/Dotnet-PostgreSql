using System;
using Npgsql;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ConsoleApp
    {
        Database Database;
        private const string connectionString = "Host=localhost;Username=postgres;Password=0809;Database=LearnPostgres";
        public ConsoleApp(Database database)
        {
            Database = database;
        }

        public async Task Run()
        {
            try
            {
                GetPostgreSqlVersion();
                WriteRecords();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            await Task.CompletedTask;
        }
        private void GetPostgreSqlVersion()
        {
            string sqlQuery = "SELECT version()";
            var response = Database.ExecuteScalar(sqlQuery).ToString();
            Console.WriteLine($"PostgreSQL version: {response}");
        }

        private void WriteRecords()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var insertSQL = string.Format
            (
                @"INSERT INTO public.patients(name, address, city, age, gender) 
                VALUES('{0}', '{1}', '{2}','{3}', '{4}');",
                "John Doe", "123 Street", "New York", "30", "Male"
            );
            var cmd = new NpgsqlCommand(insertSQL, conn);
            cmd.ExecuteNonQuery();
        }
    }
}