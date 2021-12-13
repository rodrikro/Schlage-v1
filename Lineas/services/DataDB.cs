using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Lineas.services
{   
    public struct Rectificadores {
        public int id;
        public int linea;
        public string rectificador;
        public Rectificadores( int idParam, int lineaParam, string rectificadorParam ) {
            this.id = idParam;
            this.linea = lineaParam;
            this.rectificador = rectificadorParam;
        }
    }
    public class Corriente {
        public int rectificadorId;
        public double corriente = 0;
        public Corriente(int rectIDParam, double corrienteParam) {
            this.rectificadorId = rectIDParam;
            this.corriente = corrienteParam;
        }
    }
    public class Corrientes {
        public static List<Corriente> getCorrientesListInZero( List<Rectificadores> rectificadores ) {
            List<Corriente> corrientes = new List<Corriente>();
            foreach (Rectificadores rectificador in rectificadores) {
                corrientes.Add(new Corriente(rectificador.id, 0));
            }
            return corrientes;
        }
        public static double calculateMultiplier( int numeroDePiezas, double area ) {
            return numeroDePiezas * area / 144;
        }
        public static string getCorrientesString( List<Corriente> corrientes, string separador, double multiplicador ) {
            List<string> corrientesList = new List<string>();
            foreach (Corriente corriente in corrientes) {
                var result = (corriente.corriente * multiplicador);
                var corrienteRedondeada = Convert.ToInt32(Math.Round(result));
                corrientesList.Add(corrienteRedondeada.ToString());
            }
            return String.Join(",", corrientesList.ToArray());
        }
    }
    public class Familia {
        public string familia;
        public double area;
        public Familia( string familiaParam, double areaParam ) {
            this.familia = familiaParam;
            this.area = areaParam;
        }
    }
    public class Job { 
        public string job;
        public string oid;
        public int numPiezasT;
        public DateTime fecha;
    }
    public class Product {
        public string oid;
        public string description;
        public string familia;
        public int acabado;
    }
    public class Rack {
        public int id;
        public bool enUso = false;
    }
    public class Tarea {
        public DateTime fechaInicio;
        public DateTime dia;
        public int linea;
        public string job;
        public int indice;
        public string oid;
        public int rack;
        public int piezas;
        public DateTime fechaTerminado;
        public int terminado;
        public int piezassalida;
        public string corrientes;
    }
    public class DataDB
    {
        private Database db;
        public DataDB( ref Database db ) {
            this.db = db;
        }

        public List<Rectificadores> getRectificadoresDeLinea(int linea) {
            var getRectCommand = new SqlCommand("SELECT [id] ,[rectificador] FROM [rectificadores] Where linea = @linea ORDER BY id", this.db.getConnection());
            getRectCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
            getRectCommand.Prepare();
            var rectificadoresReader = getRectCommand.ExecuteReader();
            List<Rectificadores> rectificadores = new List<Rectificadores>();
            while (rectificadoresReader.Read()) {
                rectificadores.Add(new Rectificadores((int)rectificadoresReader["id"], linea, (string)rectificadoresReader["rectificador"]));
            }
            rectificadoresReader.Close();
            getRectCommand.Dispose();
            return rectificadores;
        }

        public List<Corriente> getCorrientesDeProducto( string oid, List<Rectificadores> rectificadores ) {
            Dictionary<int, Corriente> corrientesDictionary = new Dictionary<int, Corriente>();
            List<string> rectificadoresList = new List<string>();
            foreach(Rectificadores rectificador in rectificadores){
                rectificadoresList.Add(rectificador.id.ToString());
                corrientesDictionary.Add(rectificador.id, new Corriente(rectificador.id, 0));
            }
            var rectificadoresText = String.Join(",", rectificadoresList.ToArray());

            var corrientesCommand = new SqlCommand("SELECT [id],[oid],[rectificador],[corriente] FROM [corrientes] WHERE oid = @oid AND rectificador IN (" + rectificadoresText + ") ORDER BY id", this.db.getConnection());
            corrientesCommand.Parameters.Add("@oid", SqlDbType.VarChar, 50).Value = oid;
            corrientesCommand.Prepare();
            var corrientesReader = corrientesCommand.ExecuteReader();

            while (corrientesReader.Read()) {
                if(corrientesDictionary.ContainsKey((int)corrientesReader["rectificador"])){
                    var tmpCorriente = corrientesDictionary[(int)corrientesReader["rectificador"]];
                    tmpCorriente.corriente = (double)corrientesReader["corriente"];
                }
            }
            corrientesReader.Close();
            corrientesCommand.Dispose();

            List<Corriente> corrientes = new List<Corriente>();

            foreach(Rectificadores rectificador in rectificadores){
                var tmpCorriente = corrientesDictionary[rectificador.id];
                corrientes.Add(tmpCorriente);
            }

            return corrientes;
        }

        public Familia getFamilia( string familia ) {
            var getFamiliaCommand = new SqlCommand("SELECT TOP 1 [familia],[areainsq] FROM [familias] WHERE familia = @familia", this.db.getConnection());
            getFamiliaCommand.Parameters.Add("@familia", SqlDbType.VarChar, 20).Value = familia;
            getFamiliaCommand.Prepare();
            var familiaReader = getFamiliaCommand.ExecuteReader();
            familiaReader.Read();
            if (familiaReader.HasRows) {
                var familiaObject = new Familia((string)familiaReader["familia"], (double)familiaReader["areainsq"]);
                familiaReader.Close();
                getFamiliaCommand.Dispose();
                return familiaObject;
            }
            familiaReader.Close();
            getFamiliaCommand.Dispose();
            throw new Exception("Familia '" + familia + "' not found");
        }
        public List<Familia> getFamilias() {
            var getFamiliaCommand = new SqlCommand("SELECT [familia],[areainsq] FROM [familias]", this.db.getConnection());
            getFamiliaCommand.Prepare();
            var familiaReader = getFamiliaCommand.ExecuteReader();
            List<Familia> familias = new List<Familia>();
            while (familiaReader.Read()) {
                var familiaObject = new Familia((string)familiaReader["familia"], (double)familiaReader["areainsq"]);
                familias.Add(familiaObject);
            }
            familiaReader.Close();
            getFamiliaCommand.Dispose();
            return familias;
        }
        public Job getJob( string jobID ) {
            var getJobCommand = new SqlCommand("SELECT TOP 1 [job],[oid],[num_piezas],[fechainicio] FROM [jobs] WHERE job = @job", this.db.getConnection());
            getJobCommand.Parameters.Add("@job", SqlDbType.VarChar, 50).Value = jobID;
            getJobCommand.Prepare();
            var jobReader = getJobCommand.ExecuteReader();
            jobReader.Read();
            if (jobReader.HasRows) {
                var jobObject = new Job();
                jobObject.job = (string)jobReader["job"];
                jobObject.oid = (string)jobReader["oid"];
                jobObject.numPiezasT = (int)jobReader["num_piezas"];
                jobObject.fecha = (DateTime)jobReader["fechainicio"];
                jobReader.Close();
                getJobCommand.Dispose();
                return jobObject;
            }
            jobReader.Close();
            getJobCommand.Dispose();
            throw new Exception("Job '" + jobID + "' no existe.");
        }
        public Product getProduct( string oid ) {
            var getProductCommand = new SqlCommand("SELECT TOP 1 [oid],[descripcion],[acabado],[familia] FROM [product_description] WHERE oid = @oid", this.db.getConnection());
            getProductCommand.Parameters.Add("@oid", SqlDbType.VarChar, 50).Value = oid;
            getProductCommand.Prepare();
            var productReader = getProductCommand.ExecuteReader();
            productReader.Read();
            if (productReader.HasRows) {
                var productObject = new Product();
                productObject.oid = (string)productReader["oid"];
                productObject.description = (string)productReader["descripcion"];
                productObject.acabado = (int)productReader["acabado"];
                productObject.familia = (string)productReader["familia"];
                productReader.Close();
                getProductCommand.Dispose();
                return productObject;
            }
            productReader.Close();
            getProductCommand.Dispose();
            throw new Exception("Producto '" + oid + "' no encontrado");
        }

        public Rack getRack( int rackID ) {
            var getRackCommand = new SqlCommand("SELECT TOP 1 [rack],[enUso] FROM [racks] WHERE rack = @rack", this.db.getConnection());
            getRackCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rackID;
            getRackCommand.Prepare();
            var rackReader = getRackCommand.ExecuteReader();
            rackReader.Read();
            if (rackReader.HasRows) {
                var RackObject = new Rack();
                RackObject.id = (int)rackReader["rack"];
                RackObject.enUso = ((int)rackReader["enUso"] == 1);
                rackReader.Close();
                getRackCommand.Dispose();
                return RackObject;
            }
            rackReader.Close();
            getRackCommand.Dispose();
            return null;
        }

        public bool createRack( int rackID ) {
            try {
                var createRackCommand = new SqlCommand("INSERT INTO racks (rack, enUso) VALUES (@rack, 0);", this.db.getConnection());
                createRackCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rackID;
                createRackCommand.Prepare();
                createRackCommand.ExecuteNonQuery();
                createRackCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error creating rack: " + ex.Message);
                return false;
            }
        }

        public bool setRackStatus(int rackID, bool enUso) {
            try {
                var updateRackCommand = new SqlCommand("UPDATE racks SET enUso = @enuso WHERE rack = @rack", this.db.getConnection());
                updateRackCommand.Parameters.Add("@enuso", SqlDbType.Int, 1).Value = enUso?1:0;
                updateRackCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rackID;
                updateRackCommand.Prepare();
                updateRackCommand.ExecuteNonQuery();
                updateRackCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error updating rack: " + ex.Message);
                return false;
            }
        }
        public Tarea getTarea( int linea, int indice ) {
            try {
                var tareaCommand = new SqlCommand("SELECT TOP 1 [dia],[linea],[job],[indice],[oid],[rack],[piezas],[terminado],[piezassalida],[corrientes] FROM [tareas] WHERE indice = @indice AND linea = @linea", this.db.getConnection());
                tareaCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
                tareaCommand.Parameters.Add("@indice", SqlDbType.Int, 9).Value = indice;
                tareaCommand.Prepare();
                var tareaReader = tareaCommand.ExecuteReader();
                while (tareaReader.Read()) {
                    var tarea = new Tarea();
                    tarea.dia = (DateTime)tareaReader["dia"];
                    tarea.linea = (int)tareaReader["linea"];
                    tarea.job = (string)tareaReader["job"];
                    tarea.indice = (int)tareaReader["indice"];
                    tarea.oid = (string)tareaReader["oid"];
                    tarea.rack = (int)tareaReader["rack"];
                    tarea.piezas = (int)tareaReader["piezas"];
                    tarea.piezassalida = (int)tareaReader["piezassalida"];
                    tarea.terminado = (int)tareaReader["terminado"];
                    tarea.corrientes = (string)tareaReader["corrientes"];
                    tareaReader.Close();
                    tareaCommand.Dispose();
                    return tarea;
                }
                tareaReader.Close();
                tareaCommand.Dispose();
                return null;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al traer tarea:" + ex.Message);
                return null;
            }
        }
        public Tarea getTareaSinFinalizar( int linea, int indice ) {
            try {
                var tareaCommand = new SqlCommand("SELECT TOP 1 [dia],[linea],[job],[indice],[oid],[rack],[piezas],[terminado],[piezassalida],[corrientes] FROM [tareas] WHERE indice = @indice AND linea = @linea AND terminado = 0", this.db.getConnection());
                tareaCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
                tareaCommand.Parameters.Add("@indice", SqlDbType.Int, 9).Value = indice;
                tareaCommand.Prepare();
                var tareaReader = tareaCommand.ExecuteReader();
                while (tareaReader.Read()) {
                    var tarea = new Tarea();
                    tarea.dia = (DateTime)tareaReader["dia"];
                    tarea.linea = (int)tareaReader["linea"];
                    tarea.job = (string)tareaReader["job"];
                    tarea.indice = (int)tareaReader["indice"];
                    tarea.oid = (string)tareaReader["oid"];
                    tarea.rack = (int)tareaReader["rack"];
                    tarea.piezas = (int)tareaReader["piezas"];
                    tarea.piezassalida = (int)tareaReader["piezassalida"];
                    tarea.terminado = (int)tareaReader["terminado"];
                    tarea.corrientes = (string)tareaReader["corrientes"];
                    tareaReader.Close();
                    tareaCommand.Dispose();
                    return tarea;
                }
                tareaReader.Close();
                tareaCommand.Dispose();
                return null;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al traer tarea:" + ex.Message);
                return null;
            }
        }

        public int getSiguienteIndiceDeTareaDelDia() {
            try {
                var hoy = DateTime.Now;
                var countCommand = new SqlCommand("SELECT COUNT(linea) FROM [tareas] WHERE dia = '" + hoy.Year.ToString() + "/" + hoy.Month.ToString() + "/" + hoy.Day.ToString() + "'", this.db.getConnection());
                countCommand.Prepare();
                var count = countCommand.ExecuteScalar();
                countCommand.Dispose();
                var yearText = hoy.Year.ToString().Substring(2, 2);
                var monthText = (hoy.Month * 31 + hoy.Day).ToString().PadLeft(3, '0');
                var countText = count.ToString().PadLeft(4, '0');
                var indiceNuevo = Convert.ToInt32(yearText + monthText + countText) + 1;
                return indiceNuevo;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al crear Indice:" + ex.Message);
                return 0;
            }
        }

        public bool crearTarea(int linea, string job, int indice, string oid, int rack, int piezas, string corrientes) {
            try {
                var crearTareaCommand = new SqlCommand(@"INSERT INTO [tareas] (dt_inicio, dia, linea, job, indice, oid, rack, piezas, terminado, piezassalida, corrientes) VALUES 
                                                        (GETDATE(), @dia, @linea, @job, @indice, @oid, @rack, @piezas, @terminado, @piezassalida, @corrientes)", this.db.getConnection());
                crearTareaCommand.Parameters.Add("@dia", SqlDbType.DateTime, 8).Value = DateTime.Now;
                crearTareaCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
                crearTareaCommand.Parameters.Add("@job", SqlDbType.VarChar, 50).Value = job;
                crearTareaCommand.Parameters.Add("@indice", SqlDbType.Int, 9).Value = indice;
                crearTareaCommand.Parameters.Add("@oid", SqlDbType.VarChar, 50).Value = oid;
                crearTareaCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rack;
                crearTareaCommand.Parameters.Add("@piezas", SqlDbType.Int, 3).Value = piezas;
                crearTareaCommand.Parameters.Add("@terminado", SqlDbType.Int, 1).Value = 0;
                crearTareaCommand.Parameters.Add("@piezassalida", SqlDbType.Int, 3).Value = 0;
                crearTareaCommand.Parameters.Add("@corrientes", SqlDbType.VarChar, 200).Value = corrientes;
                crearTareaCommand.Prepare();
                crearTareaCommand.ExecuteNonQuery();
                crearTareaCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al crear Tarea:" + ex.Message);
                return false;
            }
        }
        public bool eliminarTarea( string job, int indice, int rack ) {
            try {
                var eliminarTareaCommand = new SqlCommand("DELETE FROM [tareas] WHERE job = @job AND indice = @indice AND rack = @rack", this.db.getConnection());
                eliminarTareaCommand.Parameters.Add("@job", SqlDbType.VarChar, 50).Value = job;
                eliminarTareaCommand.Parameters.Add("@indice", SqlDbType.Int, 9).Value = indice;
                eliminarTareaCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rack;
                eliminarTareaCommand.Prepare();
                eliminarTareaCommand.ExecuteNonQuery();
                eliminarTareaCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al eliminar Tarea:" + ex.Message);
                return false;
            }
        }
        public bool DescargarTarea( string linea, int indice, int numeroDePiezas, string job, int rack ) {
            try {
                var sqlCommand = new SqlCommand("UPDATE [tareas] SET [terminado] = 1,[piezassalida] = @psal, dt_terminado = GETDATE() WHERE indice = @indice AND linea = @linea AND job = @job AND terminado = 0 AND rack = @rack", this.db.getConnection());
                sqlCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
                sqlCommand.Parameters.Add("@indice", SqlDbType.Int, 5).Value = indice;
                sqlCommand.Parameters.Add("@psal", SqlDbType.Int, 3).Value = numeroDePiezas;
                sqlCommand.Parameters.Add("@job", SqlDbType.VarChar, 50).Value = job;
                sqlCommand.Parameters.Add("@rack", SqlDbType.Int, 4).Value = rack;
                sqlCommand.Prepare();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al descargar Tarea:" + ex.Message);
                return false;
            }
        }
        public bool TerminarTarea( string linea, int indice ) {
            try {
                var sqlCommand = new SqlCommand("UPDATE [tareas] SET [terminado] = 1,[piezassalida] = @psal, dt_terminado = GETDATE() WHERE indice = @indice AND linea = @linea AND terminado = 0", this.db.getConnection());
                sqlCommand.Parameters.Add("@linea", SqlDbType.Int, 4).Value = linea;
                sqlCommand.Parameters.Add("@indice", SqlDbType.Int, 5).Value = indice;
                sqlCommand.Parameters.Add("@psal", SqlDbType.Int, 3).Value = 0;
                sqlCommand.Prepare();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                return true;
            } catch (Exception ex) {
                System.Console.WriteLine("Error al descargar Tarea:" + ex.Message);
                return false;
            }
        }
    }
}
