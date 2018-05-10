﻿using BeatPulse.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeatPulse.Sqlite
{
    public class SqliteLiveness : IBeatPulseLiveness
    {
        public string Name => nameof(SqliteLiveness);

        public string Path { get; }

        private string _connectionString;
        private string _healthQuery;

        public SqliteLiveness(string connectionString, string healthQuery, string defaultPath)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _healthQuery = healthQuery ?? throw new ArgumentException(nameof(healthQuery));
            Path = defaultPath ?? throw new ArgumentNullException(nameof(defaultPath));
        }
        public async Task<(string, bool)> IsHealthy(HttpContext context, bool isDevelopment, CancellationToken cancellationToken = default)
        {
            SqliteConnection connection = null;

            try
            {
                using (connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = _healthQuery;
                    using (var reader = selectCmd.ExecuteReader())
                    {
                        await reader.ReadAsync();
                    }

                    return (BeatPulseKeys.BEATPULSE_HEALTHCHECK_DEFAULT_OK_MESSAGE, true);
                }
            }
            catch (Exception ex)
            {
                var message = !isDevelopment ? string.Format(BeatPulseKeys.BEATPULSE_HEALTHCHECK_DEFAULT_ERROR_MESSAGE, Name)
                        : $"Exception {ex.GetType().Name} with message ('{ex.Message}')";

                return (message, false);
            }
        }
    }
}
