using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;
using System.Collections.Generic;

namespace TheBook.Base.DataAccessLayer
{
    public class SqlDataBlock : IDisposable
    { 

        private SqlConnection cnn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataSet ds;

        public int ExecuteNonQuery(string ConnectionString, CommandType CommandType, string CommandText)
        {
            return ExecuteNonQuery(ConnectionString, CommandType, CommandText, null);
        }
        public object ExecuteScalar(string ConnectionString, CommandType CommandType, string CommandText)
        {
            return ExecuteScalar(ConnectionString, CommandType, CommandText, null);
        }
        public SqlDataReader ExecuteReader(string ConnectionString, CommandType CommandType, string CommandText)
        {
            return ExecuteReader(ConnectionString, CommandType, CommandText, null);
        }
        public DataTable ExecuteDataTable(string ConnectionString, CommandType CommandType, string CommandText)
        {
            return ExecuteDataTable(ConnectionString, CommandType, CommandText, null);
        }
        public DataSet ExecuteDataSet(string ConnectionString, CommandType CommandType, string CommandText)
        {
            return ExecuteDataSet(ConnectionString, CommandType, CommandText, null);
        }

        public int ExecuteNonQuery(string ConnectionString, CommandType CommandType, string CommandText, List<SqlParameter> parameters)
        {
            cnn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(CommandText, cnn);
            cmd.CommandType = CommandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            OpenConnection();
            int result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }
        public object ExecuteScalar(string ConnectionString, CommandType CommandType, string CommandText, List<SqlParameter> parameters)
        {
            cnn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(CommandText, cnn);
            cmd.CommandType = CommandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            OpenConnection();
            object result = cmd.ExecuteScalar();
            CloseConnection();
            return result;
        }
        public SqlDataReader ExecuteReader(string ConnectionString, CommandType CommandType, string CommandText, List<SqlParameter> parameters)
        {
            cnn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(CommandText, cnn);
            cmd.CommandType = CommandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            OpenConnection();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public DataTable ExecuteDataTable(string ConnectionString, CommandType CommandType, string CommandText, List<SqlParameter> parameters)
        {
            cnn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(CommandText, cnn);
            da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public DataSet ExecuteDataSet(string ConnectionString, CommandType CommandType, string CommandText, List<SqlParameter> parameters)
        {
            cnn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(CommandText, cnn);
            da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        private void OpenConnection()
        {
            if (cnn.State == ConnectionState.Closed) cnn.Open();
        }

        private void CloseConnection()
        {
            if (cnn.State == ConnectionState.Open) cnn.Close();
        }

        void cnn_StateChange(object sender, StateChangeEventArgs e)
        {
            StreamWriter writer = new StreamWriter(new FileStream("C:\\Connection.txt", FileMode.Append, FileAccess.Write));
            writer.WriteLine(DateTime.Now.ToString() + "=> Previous Status : " + e.OriginalState + " Connection Status : " + e.CurrentState);
            writer.Close();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}