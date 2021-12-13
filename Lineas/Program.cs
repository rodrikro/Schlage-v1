using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lineas.config;
using Plossum.CommandLine;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Lineas
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args){
            var moduleName = Process.GetCurrentProcess().MainModule.ModuleName;
            var processName = System.IO.Path.GetFileNameWithoutExtension(moduleName);
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 1) {
                MessageBox.Show("Hay otra instancia abierta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {

                var arguments = new CommandLineArguments();
                CommandLineParser parser = new CommandLineParser(arguments);
                parser.Parse();

                configuration config = configuration.readConfig();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (config != null) {
                    try {
                        var loaderForm = new BootScreen();
                        //loaderForm.Show();
                        loaderForm.Show();
                        setProgress(ref loaderForm, 0, "");
                        
                        //Conectar a data
                        setProgress(ref loaderForm, 40, "Conectando Base de Datos");
                        var dataDB = new services.Database(config.DataConnectionString);
                        dataDB.connect();
                        
                        //Conectar a PLC
                        setProgress(ref loaderForm, 50, "Conectando a PLC");
                        var opcServer = new services.Opc(config.OpcServerString, config.OpcTopic);
                        if (config.simulateOPC) {
                            opcServer.Simulate();
                        } else {
                            opcServer.Connect();
                        }
                        var DataDBObject = new services.DataDB(ref dataDB);

                        //Setup Linea
                        setProgress(ref loaderForm, 60, "Configurando Linea");
                        shared.ILinea pantalla;
                        if (config.Tipo.ToLowerInvariant() == "salida") 
                        {
                            var lineaSalidaPLC = new services.plc.PlcLineaSalida(ref config, ref opcServer);
                            lineaSalidaPLC.configureTags();
                            lineaSalidaPLC.connectTags();

                            //Checar en linea
                            if (!opcServer.isSimulation()) {
                                var checkLine = true;
                                var successPLCCheck = false;
                                var checkLineTries = 0;
                                while (checkLine) {
                                    var tmpValue = opcServer.GetItemValue(services.plc.PlcLineaSalida.ID_IMONLINE);
                                    if (tmpValue == null) {
                                        checkLineTries = checkLineTries + 1;
                                        successPLCCheck = false;
                                        if (checkLineTries == 10) {
                                            checkLine = false;
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    } else {
                                        checkLine = false;
                                        successPLCCheck = true;
                                    }
                                }
                                if (!successPLCCheck) {
                                    throw new Exception("Cannot connect to PLC");
                                }
                            }
                            ///Checar en Linea end

                            var pantallaSalida = new pantallas.salida();
                            pantallaSalida.lineaSalida = lineaSalidaPLC;
                            pantallaSalida.ConfigurarLinea(ref config, ref DataDBObject, ref opcServer);
                            pantalla = pantallaSalida;

                        } 
                        else if (config.Tipo.ToLowerInvariant() == "entrada") {
                            var lineaEntradaPLC = new services.plc.PlcLineaEntrada(ref config, ref opcServer);
                            lineaEntradaPLC.configureTags();
                            lineaEntradaPLC.connectTags();

                            //Checar en linea
                            if (!opcServer.isSimulation()){
                                var checkLine = true;
                                var successPLCCheck = false;
                                var checkLineTries = 0;
                                while (checkLine) {
                                    var tmpValue = opcServer.GetItemValue(services.plc.PlcLineaEntrada.ID_IMONLINE);
                                    if (tmpValue == null) {
                                        checkLineTries = checkLineTries + 1;
                                        successPLCCheck = false;
                                        if (checkLineTries == 10) {
                                            checkLine = false;
                                        }
                                        System.Threading.Thread.Sleep(1000);
                                    } else {
                                        checkLine = false;
                                        successPLCCheck = true;
                                    }
                                }
                                if (!successPLCCheck) {
                                    throw new Exception("Cannot connect to PLC");
                                }
                            }
                            ///Checar en Linea end

                            var pantallaEntrada = new pantallas.entrada();
                            pantallaEntrada.lineaEntrada = lineaEntradaPLC;
                            pantallaEntrada.ConfigurarLinea(ref config, ref DataDBObject, ref opcServer);
                            pantalla = pantallaEntrada;

                        } else {
                            pantalla = null;
                            throw new Exception("Bad Configuration");
                        }
                        setProgress(ref loaderForm, 100, "Inicializando...");
                        System.Threading.Thread.Sleep(1000);
                        loaderForm.Close();
                        Application.Run((Form)pantalla);
                    } catch (Exception ex) {
                        MessageBox.Show("Error Initializing APP: " + ex.Message);
                    }
                } 
                else {
                    MessageBox.Show("Aplicacion no configurada");
                    //var configurationForm = new configuration_form();
                    //Application.Run(configurationForm);
                }
            }
        }
        static void setProgress( ref BootScreen loaderForm, int progress, string text ) {
            loaderForm.progressBar1.Value = progress;
            loaderForm.label_status.Text = text;
        } 
    }


}
