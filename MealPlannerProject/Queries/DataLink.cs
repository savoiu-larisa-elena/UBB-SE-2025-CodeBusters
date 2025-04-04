using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MealPlannerProject.Exceptions;
using System.Data;

namespace MealPlannerProject.Queries
{
    public sealed partial class DataLink : IDisposable
    {
        private static readonly Lazy<DataLink> instance = new(() => new DataLink());
        private readonly string connectionString;
        private SqlConnection? sqlConnection;
        private bool disposedValue;

        private DataLink()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                string? localDataSource = config["LocalDataSource"];
                string? initialCatalog = config["InitialCatalog"];

                if (string.IsNullOrEmpty(localDataSource) || string.IsNullOrEmpty(initialCatalog))
                {
                    throw new ArgumentNullException("LocalDataSource or InitialCatalog is null or empty");
                }

                connectionString = $"Data Source={localDataSource};Initial Catalog={initialCatalog};Integrated Security=True; TrustServerCertificate = True";

                using var connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Failed to establish database connection. Please check your connection settings.", ex);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Failed to initialize database connection.", ex);
            }
        }

        public static DataLink Instance => instance.Value;

        private SqlConnection GetConnection()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException("DataLink");
            }

            if (sqlConnection == null || sqlConnection.State != System.Data.ConnectionState.Open)
            {
                sqlConnection = new SqlConnection(connectionString);
            }

            return sqlConnection;
        }

        public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteScalar operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteScalar operation: {ex.Message}", ex);
            }
        }


        public int ExecuteQuery(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }
                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteQuery operation: {ex.Message}", ex);
            }
        }

        public T? ExecuteBool<T>(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }
                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteBool operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteBool operation: {ex.Message}", ex);
            }
        }

        public DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteReader operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteReader operation: {ex.Message}", ex);
            }
        }

        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[] sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sqlConnection != null)
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                        sqlConnection.Dispose();
                        sqlConnection = null;
                    }
                }
                disposedValue = true;
            }
        }

        public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text  // Always a raw SQL query
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new DatabaseOperationException($"Database error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException($"Error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
        }


    }
}
