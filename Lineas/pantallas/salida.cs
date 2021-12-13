using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lineas.pantallas {
    public partial class salida : Form, shared.ILinea {
        public salida() {
            InitializeComponent();
        }
        private config.configuration configuration;
        private services.DataDB dataDB;
        private services.Opc opcServer;
        private Queue<string> errorLog = new Queue<string>();
        public services.plc.PlcLineaSalida lineaSalida;

        private Dictionary<int, shared.SlotSalida> SalidaDataConcat = new Dictionary<int, shared.SlotSalida>();
        private Dictionary<int, shared.SlotSalida> SalidaPullOut = new Dictionary<int, shared.SlotSalida>();

        private Dictionary<int, shared.paqueteSalidaPlc> paquetesDeSalida = new Dictionary<int, shared.paqueteSalidaPlc>();

        public void ConfigurarLinea( ref config.configuration configParam, ref services.DataDB dataDBParam, ref services.Opc opcServerParam ) {
            this.configuration = configParam;
            this.dataDB = dataDBParam;
            this.opcServer = opcServerParam;

            this.Text = "Linea " + this.configuration.Linea + " Salida";
            for (var i = 0; i < this.configuration.ColaSalida; i++) {
                var slotSalida = new shared.SlotSalida();
                slotSalida.Type = 0;
                slotSalida.Position = i;
                slotSalida.lineaSalida = this.lineaSalida;
                this.SalidaDataConcat.Add(i, slotSalida);
            }
            for (var i = 0; i < this.configuration.PullOutSalida; i++) {
                var slotSalida = new shared.SlotSalida();
                slotSalida.Type = 1;
                slotSalida.Position = i;
                slotSalida.lineaSalida = this.lineaSalida;
                this.SalidaPullOut.Add(i, slotSalida);
            }
        }

        private void Button1_Click( object sender, EventArgs e ) {
            Process.Start("osk.exe");
        }

        private void agregarPaqueteAFlujo(shared.paqueteSalidaPlc paqueteDeSalidaPLC) {
            if (paqueteDeSalidaPLC.isBarraText) {
                var salidaElement = new shared.salida_barraplc_component();
                salidaElement.SetBarraType("plc");
                salidaElement.SetReferences(ref this.FlowLayoutPanel1, ref paqueteDeSalidaPLC);
                FlowLayoutPanel1.Controls.Add(salidaElement);
            } else if (paqueteDeSalidaPLC.indexdb > 0) {
                try {
                    var tarea = this.dataDB.getTarea(Convert.ToInt32(this.configuration.Linea), paqueteDeSalidaPLC.indexdb);
                    if (tarea != null && tarea.terminado == 1) { 
                        //Si la tarea fue finalizada, limpiar el slot
                        paqueteDeSalidaPLC.doCleanup();
                    } else if (tarea != null && tarea.job.Equals("BARRA")) {
                        //Si no fue finalizada y es barra, terminarla directamente
                        this.dataDB.TerminarTarea(this.configuration.Linea, paqueteDeSalidaPLC.indexdb);

                        //Mostramos UI de Barra
                        var salidaElement = new shared.salida_barraplc_component();
                        salidaElement.SetBarraType("db");
                        salidaElement.SetReferences(ref this.FlowLayoutPanel1, ref paqueteDeSalidaPLC);
                        FlowLayoutPanel1.Controls.Add(salidaElement);
                    } else if (tarea != null) {
                        //Si no fue finalizada y no es barra, agregarla al pool de descargas
                        var productData = this.dataDB.getProduct(tarea.oid);
                        var description = "";
                        if (productData != null) {
                            description = productData.description;
                        }
                        var salidaElement = new shared.salida_element_component();
                        salidaElement.SetData(this.configuration.Linea, paqueteDeSalidaPLC.indexdb, tarea.job, description, tarea.oid, tarea.piezas, tarea.rack);
                        salidaElement.SetReferences(ref this.FlowLayoutPanel1, ref this.dataDB, ref paqueteDeSalidaPLC);
                        FlowLayoutPanel1.Controls.Add(salidaElement);
                    } else {
                        this.AddErrorMessage("Hubo un error con el indice: " + paqueteDeSalidaPLC.indexdb);
                        paqueteDeSalidaPLC.doCleanup();
                    }
                } catch (Exception ex) {
                    this.AddErrorMessage("Hubo un error al procesar el paquete de salida: " + paqueteDeSalidaPLC.indexdb + ". " + ex.Message);
                    paqueteDeSalidaPLC.doCleanup();
                }
            }
        }

        private void timerCheckPLC_Tick( object sender, EventArgs e ) {
            //Checamos Cola de Salida
            for (var i = 0; i < this.configuration.ColaSalida; i++) {
                var slotSalida = this.SalidaDataConcat[i];
                try {
                    var paqueteSalida = slotSalida.ParseData();
                    if (paqueteSalida.hasData && paqueteSalida.indexdb > 0 && this.paquetesDeSalida.ContainsKey(paqueteSalida.indexdb)) {
                        //Si tenemos el paquete, lo traemos y actualizamos el slot de salida de la referencia
                        var paqueteSalidaReferencia = this.paquetesDeSalida[paqueteSalida.indexdb];
                        paqueteSalidaReferencia.SlotSalida = slotSalida;
                        slotSalida.PaqueteSalidaActual = paqueteSalidaReferencia;

                        //Solo por precaucion eliminamos el actual.
                        paqueteSalida = null;
                    } else if (paqueteSalida.hasData && paqueteSalida.indexdb > 0 && !this.paquetesDeSalida.ContainsKey(paqueteSalida.indexdb)) {
                        paqueteSalida.SlotSalida = slotSalida;
                        slotSalida.PaqueteSalidaActual = paqueteSalida;

                        //Ponemos el paquete en la lista de que ya tenemos el paquete e iniciamos la expiracion
                        paqueteSalida.setParentListReference(ref this.paquetesDeSalida);
                        this.paquetesDeSalida.Add(paqueteSalida.indexdb, paqueteSalida);
                        //Agregamos el Control
                        agregarPaqueteAFlujo(paqueteSalida);
                    } else if (paqueteSalida.hasData && paqueteSalida.isBarraText && (slotSalida.PaqueteSalidaActual == null || (slotSalida.PaqueteSalidaActual != null && !slotSalida.PaqueteSalidaActual.isBarraText))) { 
                        //Es una barra
                        paqueteSalida.SlotSalida = slotSalida;
                        slotSalida.PaqueteSalidaActual = paqueteSalida;
                        //Agregamos el Control
                        agregarPaqueteAFlujo(paqueteSalida);
                    }
                } catch (Exception ex) {
                    this.AddErrorMessage("Error en DataConcat Indice " + i + ". Error: " + ex.Message);
                    slotSalida.LimpiarSlot();
                }
            }
            //Checamos PullOuts
            for (var i = 0; i < this.configuration.PullOutSalida; i++) {
                var slotSalida = this.SalidaPullOut[i];
                try {
                    var paqueteSalida = slotSalida.ParseData();
                    if (paqueteSalida.hasData && paqueteSalida.indexdb > 0 && !this.paquetesDeSalida.ContainsKey(paqueteSalida.indexdb)) {
                        paqueteSalida.SlotSalida = slotSalida;
                        slotSalida.PaqueteSalidaActual = paqueteSalida;

                        //Ponemos el paquete en la lista ya que hay la posibilidad de duplicacion de datos por el PLC
                        paqueteSalida.setParentListReference(ref this.paquetesDeSalida);
                        this.paquetesDeSalida.Add(paqueteSalida.indexdb, paqueteSalida);
                        paqueteSalida.startExpiration();

                        //Si el paquete de salida tiene un dato, terminamos la tarea
                        if (paqueteSalida.indexdb > 0) {
                            this.dataDB.TerminarTarea(this.configuration.Linea, paqueteSalida.indexdb);
                        }
                        paqueteSalida.SlotSalida.LimpiarSlot();
                    } else if (paqueteSalida.hasData && paqueteSalida.indexdb > 0) {
                        //Ya tenemos el paquete por lo tanto lo borramos
                        slotSalida.LimpiarSlot();
                    }
                } catch (Exception ex) {
                    this.AddErrorMessage("Error en PullOut Indice " + i + ". Error: " + ex.Message);
                    slotSalida.LimpiarSlot();
                }
            }
        }

        public void AddErrorMessage( string errorMessage ) {
            this.errorLog.Enqueue(errorMessage + Environment.NewLine);
        }

        private void timerLog_Tick( object sender, EventArgs e ) {
            if (this.errorLog.Count > 0) {
                var errorMessage = this.errorLog.Dequeue();
                this.textBoxLog.AppendText(errorMessage);
            }
        }
    }
}
