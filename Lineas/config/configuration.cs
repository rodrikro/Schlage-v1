using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace Lineas.config {
    [Serializable]
    public class AndonElement {
        public string Name;
        public string DatabaseName;
        public string PLCName;
        public AndonElement( string name, string databaseName, string plcname ) {
            this.Name = name;
            this.DatabaseName = databaseName;
            this.PLCName = plcname;
        }
    }
    //Aca estamos, jaj okok esta bien bro, entonces este es el archivo que te había pasado
    [Serializable]
    public class configuration {
        public int isSet = 0; //Prueba para leer configuracion
        public bool simulateOPC = false;

        public string AndonConnectionString;
        public string DataConnectionString;

        public string OpcServerString;
        public string OpcTopic;
        public bool PuedoOxidar = false;
        public bool PuedoEnviarBarras = false;
        
        public bool VerMensajeTareaDB = false;
        public bool VerMensajeCargaPLC = false;

        public bool SiguienteCicloAutomatico = false;

        public string Tipo; //entrada/salida
        public string Linea;
        public string[] Stationnames;
        public int Workbars = 1;
        public int Racksworkbars = 1;
        public int ColaSalida = 9;
        public int PullOutSalida = 9;
        public string[] Acabados;

        public List<AndonElement> AndonElements = new List<AndonElement>();
        //Una pregunta, tu solo ves el archivo al que tengo el foco?
        public static configuration readConfig() {
            try {
                var jsonRead = System.IO.File.ReadAllText(@"configuration.json");
                configuration parsedConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<configuration>(jsonRead);
                return parsedConfig;
            } catch (System.IO.FileNotFoundException ex) {
                System.Console.WriteLine("Error: " + ex.Message);
                return null;
            } catch (Exception ex) {
                System.Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        public static void saveConfig( ref configuration config ) {
            try {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(@"configuration.json", json);
            } catch (Exception ex) {
                System.Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void saveTestConfig() {
            try {
                var config = new configuration();
                config.isSet = 1;
                config.simulateOPC = false;
                config.Acabados = new string[]{ "704" };
                config.AndonConnectionString = "Server=127.0.0.1;Database=nx_andon;User ID=nxu_andon;Password=the_nxu_andon;Trusted_Connection=False; Timeout=10;";
                config.DataConnectionString = "Server=127.0.0.1;Database=nx_data;User ID=nxu_data;Password=the_nxu_data;Trusted_Connection=False; Timeout=10;";
                config.OpcServerString = "RSLinx OPC Server";
                config.OpcTopic = "nxn200_new";
                config.Linea = "200";
                config.Workbars = 1;
                config.ColaSalida = 1;
                config.Racksworkbars = 1;
                config.Stationnames = new string[]{ "201" };
                config.Tipo = "entrada";
                config.PuedoOxidar = true;
                config.PuedoEnviarBarras = true;

                config.PullOutSalida = 9;
                config.ColaSalida = 9;
                
                config.AndonElements.Add(new AndonElement("Mantenimiento", "mantenimiento", "Mannto"));
                config.AndonElements.Add(new AndonElement("Materiales", "materiales", "Mat"));
                config.AndonElements.Add(new AndonElement("Producción", "produccion", "Prod"));
                config.AndonElements.Add(new AndonElement("Calidad", "calidad", "Calidad"));
                
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(@"configuration.json", json);
                var jsonRead = System.IO.File.ReadAllText(@"configuration.json");
                configuration parsedConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<configuration>(jsonRead);
                System.Console.WriteLine("JSON: " + parsedConfig);
            } catch (Exception ex) {
                System.Console.WriteLine("Error: " + ex.Message);
            }
        }
    }


}
