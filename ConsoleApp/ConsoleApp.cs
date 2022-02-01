using System;
using Npgsql;
using System.Collections.Generic;
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
                FetchRecords();
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
            string insertSQL = string.Format
            (
                @"INSERT INTO public.patients(name, address, city, age, gender) 
                VALUES(@name, @address, @city, @age, @gender);"
            );

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", "Mathew Bane");
            parameters.Add("@address", "879 Street");
            parameters.Add("@city", "Washington");
            parameters.Add("@age", 31);
            parameters.Add("@gender", "Male");

            int response = Database.ExecuteNonQuery(insertSQL, parameters);
            string message = response == 1 ? "Successfully Inserted!" : "Failed to insert!";
            Console.WriteLine(message);
        }

        private void FetchRecords()
        {
            string sqlQuery = "Select id, name, address, city, age, gender from public.patients";
            var records = Database.ExecuteQuery(sqlQuery);
            string message = $"Fetched {records.Count} records";
            Console.WriteLine(message);
        }
    }
}