using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autopilot.Models;
using Microsoft.Data.Sqlite;

namespace autopilot.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source = app.db";
        public async Task<List<RecordModel>> GetAllRecordsAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Records (Id INTEGER PRIMARY KEY AUTOINCREMENT, ShortName TEXT, FullName TEXT, Additional0 TEXT, Additional1 TEXT, Additional2 TEXT, Additional3 TEXT, Additional4 TEXT, Additional5 TEXT, Additional6 TEXT);";
            await command.ExecuteNonQueryAsync();

            command.CommandText = "SELECT * FROM Records";
            var result = new List<RecordModel>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new RecordModel()
                {
                    Id = reader.GetInt32(0),
                    ShortName = reader.GetString(1),
                    FullName = reader.GetString(2),
                    Additional0 = reader.GetString(3),
                    Additional1 = reader.GetString(4),
                    Additional2 = reader.GetString(5),
                    Additional3 = reader.GetString(6),
                    Additional4 = reader.GetString(7),
                    Additional5 = reader.GetString(8),
                    Additional6 = reader.GetString(9)
                });
            }
            return result;
        }
    }
}
