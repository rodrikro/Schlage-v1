using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public class workrack {
        public shared.lineaEntrada lineaConfig;

        public bool isInUse = false;
        public int id;
        public int plcID;
        public string stationName;

        public bool Barra = false;
        public services.Tarea Tarea;
        public services.Rack Rack;
        public services.Job Job;
        public services.Product Producto;
        public services.Familia Familia;
        public List<services.Corriente> Corrientes;
        public string CorrientesString;
        public bool Oxidado = false;
        public int NumeroDePiezas;

        public services.plc.PlcWorkrackInfo plcInfo = null;

        public shared.entrada_agregar_rack formularioEscritura = new shared.entrada_agregar_rack();
        public shared.entrada_lectura_rack formularioLectura = new shared.entrada_lectura_rack();
        public shared.Irack_entrada vistaRackEntrada;

        public workrack() {
            this.formularioEscritura.workrack = this;
            this.formularioLectura.workrack = this;
            this.formularioEscritura.Location = new System.Drawing.Point(50, 20);
            this.formularioLectura.Location = new System.Drawing.Point(50, 20);
        }

        public void setVistaRackEntrada( shared.Irack_entrada rackEntradaParam ) {
            this.vistaRackEntrada = rackEntradaParam;
        }

        public void LimpiarForma() {
            this.Barra = false;
            this.Tarea = null;
            this.Rack = null;
            this.Job = null;
            this.Producto = null;
            this.Familia = null;
            this.Corrientes = null;
            this.CorrientesString = "";
            this.NumeroDePiezas = 0;
            this.formularioEscritura.LimpiarForma();
        }
        public void LimpiarPLC() {
            this.lineaConfig.plcEntrada.workracks[this.plcID].Limpiar();
            this.vistaRackEntrada.clearInUse();
        }
        public void UpdateWithPLCInfo() {
            var plcInfo = this.lineaConfig.plcEntrada.workracks[this.plcID].getInfo();
            if (plcInfo.ctr == 1 && plcInfo.task == 0) {
                this.isInUse = false;
                this.plcInfo = null;
                this.vistaRackEntrada.clearInUse();
                this.lineaConfig.plcEntrada.workracks[this.plcID].Limpiar();
            } else if (plcInfo.ctr == 1) {
                this.plcInfo = plcInfo;
                this.isInUse = true;
                if (plcInfo.job.Equals("BARRA")) {
                    this.vistaRackEntrada.setInUseBarra();
                } else {
                    this.vistaRackEntrada.setInUse(plcInfo.rack, plcInfo.numeroDePiezas.ToString(), plcInfo.job);
                }
            } else {
                this.isInUse = false;
                this.plcInfo = null;
                this.vistaRackEntrada.clearInUse();
            }
        }
        public bool EliminarTareaDePLC() {
            try {
                if (this.plcInfo.task != 0 || this.plcInfo.rack != "") {
                    var task = Convert.ToInt32(this.plcInfo.task);
                    var rack = Convert.ToInt32(this.plcInfo.rack);
                    var job = this.plcInfo.job;
                    this.lineaConfig.dataDB.eliminarTarea(this.plcInfo.job, task, rack);
                    this.lineaConfig.dataDB.setRackStatus(rack, false);
                }
                this.LimpiarPLC();
                return true;
            } catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("Error al eliminar de PLC: " + ex.Message);
                return false;
            }
        }
        public bool CargarAPLC() {
            try {
                var siguienteIndice = this.lineaConfig.dataDB.getSiguienteIndiceDeTareaDelDia();
                int linea = Convert.ToInt32(this.lineaConfig.configuration.Linea);
                var crearTarea = this.lineaConfig.dataDB.crearTarea(linea, this.Job.job, siguienteIndice, this.Producto.oid, this.Rack.id, this.NumeroDePiezas, this.CorrientesString);
                if (!crearTarea) {
                    throw new Exception("No fue posible crear la tarea.");
                }
                if (this.lineaConfig.configuration.VerMensajeTareaDB) {
                    System.Windows.Forms.MessageBox.Show("Tarea creada satisfactoriamente en la Base de Datos: " + siguienteIndice);
                }
                
                var acabado = this.Producto.acabado;
                if(this.Oxidado){
                    acabado = acabado + 1000;
                }
                this.lineaConfig.dataDB.setRackStatus(this.Rack.id, true);
                this.lineaConfig.plcEntrada.workracks[this.plcID].CargarInfo(this.Job.job, this.Rack.id.ToString(), this.Producto.oid, this.NumeroDePiezas, this.CorrientesString, siguienteIndice, acabado);
                if (this.lineaConfig.configuration.VerMensajeCargaPLC) {
                    System.Windows.Forms.MessageBox.Show("Tarea " + siguienteIndice + " enviada al PLC.");
                }
                this.LimpiarForma();
                return true;
            } catch(Exception ex){
                System.Windows.Forms.MessageBox.Show("Error al cargar al PLC: " + ex.Message);
                return false;
            }
        }
        public bool CargarBarraAPLC() {
            try {
                var siguienteIndice = this.lineaConfig.dataDB.getSiguienteIndiceDeTareaDelDia();
                int linea = Convert.ToInt32(this.lineaConfig.configuration.Linea);
                var crearTarea = this.lineaConfig.dataDB.crearTarea(linea, "BARRA", siguienteIndice, "", 0, 0, this.CorrientesString);
                if (!crearTarea) {
                    throw new Exception("No fue posible crear la tarea.");
                }
                if (this.lineaConfig.configuration.VerMensajeTareaDB) {
                    System.Windows.Forms.MessageBox.Show("Barra creada satisfactoriamente en la Base de Datos: " + siguienteIndice);
                }
                this.lineaConfig.plcEntrada.workracks[this.plcID].CargarInfo("BARRA", "0", "", 0, this.CorrientesString, siguienteIndice, 0);
                if (this.lineaConfig.configuration.VerMensajeCargaPLC) {
                    System.Windows.Forms.MessageBox.Show("Tarea " + siguienteIndice + " enviada al PLC.");
                }
                this.LimpiarForma();
                return true;
            } catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show("Error al cargar al PLC: " + ex.Message);
                return false;
            }
        }
    }
}
