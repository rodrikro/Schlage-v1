using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Administrador.Modulos.configuration {
    class dbTester {
        public static bool tryConnectionString(string connectionString) {
            try {
                if (connectionString == null) {
                    return false;
                } else {
                    var testConnection = new SqlConnection(connectionString.ToString());
                    testConnection.Open();
                    if (testConnection.State == ConnectionState.Open) {
                        testConnection.Close();
                        return true;
                    } else {
                        return false;
                    }
                }
            } catch (Exception ex) {
                return false;
            }   
        }
    }
}
