using SqlServer.LocalDb.Exceptions;
using SqlServer.LocalDb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlServer.LocalDb
{
    public static class LocalDb
    {
        public static string GetConnectionString(string databaseName)
        {
            return $"Server=(localdb)\\mssqllocaldb;Database={databaseName};Integrated Security=true";
        }

        public static SqlConnection GetConnection(string databaseName, IEnumerable<InitializeStatement> initializeStatements)
        {
            return GetConnection(databaseName, (cn) =>
            {
                ExecuteInitializeStatements(cn, initializeStatements);
            });
        }

        private static void ExecuteInitializeStatements(SqlConnection cn, IEnumerable<InitializeStatement> statements)
        {
            foreach (var statement in statements)
            {
                if (ObjectExists(cn, statement.ObjectName))
                {
                    Execute(cn, statement.ResolveDropStatement());
                }

                Execute(cn, statement.ResoveCreateStatement());
            }
        }

        private static void Execute(SqlConnection cn, string statement)
        {
            using (var cmd = new SqlCommand(statement, cn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static SqlConnection GetConnection(string databaseName, Action<SqlConnection> initialize = null)
        {
            try
            {
                string connectionString = GetConnectionString(databaseName);
                var result = new SqlConnection(connectionString);
                result.Open();

                try
                {
                    initialize?.Invoke(result);
                }
                catch (Exception exc)
                {
                    throw new InitializationException($"Initialization error: {exc.Message}");
                }
                
                return result;
            }
            catch (InitializationException)
            {
                throw;
            }
            catch (Exception)
            {
                if (TryCreateDbIfNotExists(databaseName))
                {
                    return GetConnection(databaseName, initialize);
                }
                else
                {
                    // don't know what else to do, re-create the original error
                    throw;
                }
            }
        }

        public static bool TryDropDatabase(string databaseName, out string message)
        {
            try
            {
                using (var cn = new SqlConnection(GetConnectionString("master")))
                {
                    Execute(cn, $"DROP DATABASE [{databaseName}]");                    
                    message = null;
                    return true;                    
                }
            }
            catch (Exception exc)
            {
                message = exc.Message;
                return false;
            }
        }

        private static bool TryCreateDbIfNotExists(string databaseName)
        {
            try
            {
                using (var cn = new SqlConnection(GetConnectionString("master")))
                {
                    cn.Open();
                    if (!DatabaseExists(cn, databaseName))
                    {
                        Execute(cn, $"CREATE DATABASE [{databaseName}]");
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool DatabaseExists(SqlConnection cn, string databaseName)
        {
            using (var cmd = new SqlCommand("SELECT 1 FROM [sys].[databases] WHERE [Name]=@name", cn))
            {
                cmd.Parameters.AddWithValue("name", databaseName);
                var result = cmd.ExecuteScalar();
                return result?.Equals(1) ?? false;
            }
        }

        public static bool ObjectExists(SqlConnection connection, string objectName)
        {
            try
            {
                using (var cmd = new SqlCommand($"SELECT OBJECT_ID('{objectName}')", connection))
                {
                    var result = cmd.ExecuteScalar();
                    return !result?.Equals(DBNull.Value) ?? false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ExecuteIfExists(SqlConnection connection, string objectName, string execute)
        {
            if (ObjectExists(connection, objectName))
            {
                using (var cmd = new SqlCommand(execute, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
