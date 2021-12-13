using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace Lineas.services
{
    public class Database
    {
        private SqlConnection connection = new SqlConnection();
        public Database(string connectionStringParam) {
            connection.ConnectionString = connectionStringParam;
        }
        public void connect() {
            if (!isConnected()){
                try {
                    connection.Open();
                } catch (Exception ex) {
                    System.Console.WriteLine("Unable to connect to DB: " + ex.Message);
                }
            }
        }
        public bool isConnected() {
            return connection.State == System.Data.ConnectionState.Open;
        }
        public SqlConnection getConnection() {
            return connection;
        }
    }
}
