using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace Lineas.services
{
    public struct AndonEstado {
        public int id;
        public string linea;
        public string opcion;
        public int status;
    }
    public struct AndonEventos {
        public int id;
        public DateTime datesubm;
        public string linea;
        public string opcion;
        public int tiempototal;
        public int tiempollegada;
        public int tiemporeparacion;
    }
    public struct AndonHistorial {
        public DateTime timeevent;
        public string linea;
        public string opcion;
        public int estado;
    }
    public class AndonDB
    {
        private Database db;
        public AndonDB(ref Database db) {
            this.db = db;
        }

        public AndonEstado GetDBInstanceID(int linea, string opcion){
            var sqlCommand = new SqlCommand("SELECT id, status FROM estados WHERE linea = @linea AND opcion = @opcion", this.db.getConnection());
            sqlCommand.Parameters.Add("@linea", SqlDbType.VarChar, 50).Value = linea.ToString();
            sqlCommand.Parameters.Add("@opcion", SqlDbType.VarChar, 50).Value = opcion;
            sqlCommand.Prepare();
            var reader = sqlCommand.ExecuteReader();
            reader.Read();
            if (reader.HasRows) {
                int dbID = (int)reader["id"];
                var andonEstado = new AndonEstado();
                andonEstado.id = (int)reader["id"];
                andonEstado.status = (int)reader["status"];
                andonEstado.opcion = opcion;
                andonEstado.linea = linea.ToString();
                reader.Close();
                sqlCommand.Dispose();
                return andonEstado;
            }
            var createInstanceCommand = new SqlCommand(@"INSERT INTO estados (linea, opcion, status) VALUES (@linea, @opcion, @status);
                                                         SELECT CAST(scope_identity() AS int);", this.db.getConnection());
            createInstanceCommand.Parameters.Add("@linea", SqlDbType.VarChar, 50).Value = linea.ToString();
            createInstanceCommand.Parameters.Add("@opcion", SqlDbType.VarChar, 50).Value = opcion;
            createInstanceCommand.Parameters.Add("@status", SqlDbType.Int, 2).Value = 0;
            createInstanceCommand.Prepare();
            var dbInstance = (int)createInstanceCommand.ExecuteScalar();
            var newAndonEstado = new AndonEstado();
            newAndonEstado.id = dbInstance;
            newAndonEstado.status = 0;
            newAndonEstado.opcion = opcion;
            newAndonEstado.linea = linea.ToString();
            return newAndonEstado;
        }

        public bool setAndonStatus( int id, int linea, string opcion, int status ) {
            using (SqlTransaction transaction = this.db.getConnection().BeginTransaction()) {
                try {
                    var updateStatusCommand = new SqlCommand("UPDATE estados SET status = @status WHERE id = @id", this.db.getConnection(), transaction);
                    updateStatusCommand.Parameters.Add("@status", SqlDbType.Int, 2).Value = status;
                    updateStatusCommand.Parameters.Add("@id", SqlDbType.Int, 15).Value = id;
                    updateStatusCommand.Prepare();
                    var inHistorialCommand = new SqlCommand("INSERT INTO historial (timeevent, linea, opcion, estado) VALUES (GETDATE(), @linea, @opcion, @status)", this.db.getConnection(), transaction);
                    inHistorialCommand.Parameters.Add("@linea", SqlDbType.VarChar, 50).Value = linea.ToString();
                    inHistorialCommand.Parameters.Add("@opcion", SqlDbType.VarChar, 50).Value = opcion;
                    inHistorialCommand.Parameters.Add("@status", SqlDbType.Int, 2).Value = status;
                    inHistorialCommand.Prepare();

                    updateStatusCommand.ExecuteNonQuery();
                    updateStatusCommand.Dispose();
                    inHistorialCommand.ExecuteNonQuery();
                    inHistorialCommand.Dispose();
                    transaction.Commit();
                    return true;
                } catch (Exception ex) {
                    System.Console.WriteLine("Error:" + ex.Message);
                    return false;
                }
            }
        }
        public int getStatus( int id ) {
            var getStatusCommand = new SqlCommand("SELECT status FROM estados WHERE id = @id", this.db.getConnection());
            getStatusCommand.Parameters.Add("@id", SqlDbType.Int, 15).Value = id;
            getStatusCommand.Prepare();
            var status = (int)getStatusCommand.ExecuteScalar();
            getStatusCommand.Dispose();
            return status;
        }

        public bool calculateAndSaveTotal( string linea, string opcion ) {
            try {
                var getLastTreeEventsCommand = new SqlCommand("SELECT TOP 3 timeevent, estado FROM historial WHERE linea = @linea AND opcion = @opcion ORDER BY timeevent DESC", this.db.getConnection());
                getLastTreeEventsCommand.Parameters.Add("@linea", SqlDbType.VarChar, 50).Value = linea;
                getLastTreeEventsCommand.Parameters.Add("@opcion", SqlDbType.VarChar, 50).Value = opcion;
                getLastTreeEventsCommand.Prepare();
                var eventosReader = getLastTreeEventsCommand.ExecuteReader();
                var tiemposEventos = new Dictionary<int, DateTime>();
                while (eventosReader.Read()) {
                    if (!tiemposEventos.ContainsKey((int)eventosReader["estado"])) {
                        tiemposEventos.Add((int)eventosReader["estado"], (DateTime)eventosReader["timeevent"]);
                    }
                }
                eventosReader.Close();
                getLastTreeEventsCommand.Dispose();

                var dd0 = tiemposEventos[0];
                var dd1 = tiemposEventos[1];
                var dd2 = tiemposEventos[2];
                //Restamos 1 y 2 y obtenemos tiempo llegada
                var tiempollegada = dd2 - dd1;
                //Restamos 2 y 0 y obtenemos tiempo resolución
                var tiemporesolucion = dd0 - dd2;
                //Sumamos los 2 tiempos y obtenemos el tiempo total
                var tiempototal = tiempollegada + tiemporesolucion;

                var insertHistorialCommand = new SqlCommand("INSERT INTO eventos (linea, opcion, tiempototal, tiempollegada, tiemporeparacion) VALUES (@linea, @opcion, @tt, @tl, @tr)", this.db.getConnection());
                insertHistorialCommand.Parameters.Add("@linea", SqlDbType.VarChar, 50).Value = linea;
                insertHistorialCommand.Parameters.Add("@opcion", SqlDbType.VarChar, 50).Value = opcion;
                insertHistorialCommand.Parameters.Add("@tt", SqlDbType.Int, 20).Value = tiempototal.TotalSeconds;
                insertHistorialCommand.Parameters.Add("@tl", SqlDbType.Int, 10).Value = tiempollegada.TotalSeconds;
                insertHistorialCommand.Parameters.Add("@tr", SqlDbType.Int, 10).Value = tiemporesolucion.TotalSeconds;
                insertHistorialCommand.Prepare();
                insertHistorialCommand.ExecuteNonQuery();
                insertHistorialCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
