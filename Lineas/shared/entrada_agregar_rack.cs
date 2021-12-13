using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class entrada_agregar_rack : UserControl {
        
        public shared.workrack workrack;

        private services.Job jobReference = null;
        
        public entrada_agregar_rack() {
            InitializeComponent();
        }
        private void entrada_agregar_rack_Load( object sender, EventArgs e ) {
            this.gpb_conf.Hide();
            Oxidado.Checked = false;
            Oxidado.Hide();
            btn_barra.Hide();
            if(this.workrack.lineaConfig.configuration.PuedoOxidar) 
                Oxidado.Show();
            if (this.workrack.lineaConfig.configuration.PuedoEnviarBarras)
                btn_barra.Show();
        }

        private void btn_enviardatos_Click( object sender, EventArgs e ) {
            var job = txt_o_job.Text.Trim();
            var rack = txt_o_rack.Text.Trim();
            var numPiezas = txt_o_numpiez.Text.Trim();
            if (job == "" || rack == "" || numPiezas == "") {
                MessageBox.Show("Los campos no pueden estar vacios");
                return;
            }
            try {
                var rackNumber = Convert.ToInt32(rack);
                var numPiezasNumber = Convert.ToInt32(numPiezas);
                var linea = Convert.ToInt32(this.workrack.lineaConfig.configuration.Linea);

                this.workrack.NumeroDePiezas = numPiezasNumber;
                this.workrack.Job = this.workrack.lineaConfig.dataDB.getJob(job);

                if (numPiezasNumber > this.workrack.Job.numPiezasT) {
                    MessageBox.Show("El Numero de Piezas no puede ser mayor a " + this.workrack.Job.numPiezasT.ToString() + ".");
                    return;
                }

                this.workrack.Producto = this.workrack.lineaConfig.dataDB.getProduct(this.workrack.Job.oid);
                if(!this.workrack.lineaConfig.familias.ContainsKey(this.workrack.Producto.familia)){
                    throw new Exception("Familia " + this.workrack.Producto.familia + " no encontrada");
                }
                this.workrack.Familia = this.workrack.lineaConfig.familias[this.workrack.Producto.familia];
                var esPermitido = false;
                foreach (string acabadoPermitido in this.workrack.lineaConfig.configuration.Acabados) {
                    var acabadoPermitidoNumber = Convert.ToInt32(acabadoPermitido);
                    if (acabadoPermitidoNumber == this.workrack.Producto.acabado) {
                        esPermitido = true;
                    }
                }
                if (!esPermitido) {
                    MessageBox.Show("El acabado no es permitido en esta linea.");
                    return;
                }
                this.workrack.Rack = this.workrack.lineaConfig.dataDB.getRack(rackNumber);
                if (this.workrack.Rack == null) {
                    var creado = this.workrack.lineaConfig.dataDB.createRack(rackNumber);
                    if (creado) {
                        this.workrack.Rack = new services.Rack();
                        this.workrack.Rack.id = rackNumber;
                        this.workrack.Rack.enUso = false;
                    }
                }
                if (this.workrack.Rack == null || this.workrack.Rack.enUso) {
                    MessageBox.Show("El rack esta en uso.");
                    return;
                }
                var rectificadores = this.workrack.lineaConfig.rectificadores;
                this.workrack.Corrientes = this.workrack.lineaConfig.dataDB.getCorrientesDeProducto(this.workrack.Job.oid, rectificadores);

                var multiplicador = services.Corrientes.calculateMultiplier(numPiezasNumber, this.workrack.Familia.area);
                this.workrack.CorrientesString = services.Corrientes.getCorrientesString(this.workrack.Corrientes, ",", multiplicador);
                

                btn_enviardatos.Enabled = false;
                btn_barra.Enabled = false;
                
                lbl_oid.Text = this.workrack.Job.oid;
                lbl_job.Text = job;
                lbl_nump.Text = numPiezasNumber.ToString();
                txt_description.Text = this.workrack.Producto.description;

                FlowLayoutPanel1.Controls.Clear();

                foreach (services.Corriente corriente in this.workrack.Corrientes) {
                    var tmpObj = new shared.entrada_agregar_rack_corriente();
                    string corrienteString = corriente.corriente.ToString();
                    string corrienteTotal = (corriente.corriente * multiplicador).ToString();
                    tmpObj.FillData(this.workrack.lineaConfig.rectificadoresDictionary[corriente.rectificadorId].rectificador, corrienteString, corrienteTotal);
                    FlowLayoutPanel1.Controls.Add(tmpObj);
                }
                gpb_conf.Show();
            } catch(Exception ex){
                MessageBox.Show("Error al enviar datos: " + ex.Message);
            }
        }

        private void btn_barra_Click( object sender, EventArgs e ) {
            try {
                var rack = "0";
                var numPiezas = "0";
                var job = "BARRA";

                var rackNumber = Convert.ToInt32(rack);
                var numPiezasNumber = Convert.ToInt32(numPiezas);
                var linea = Convert.ToInt32(this.workrack.lineaConfig.configuration.Linea);

                this.workrack.NumeroDePiezas = numPiezasNumber;
                this.workrack.Barra = true;
                this.workrack.Job = null;
                this.workrack.Producto = null;
                this.workrack.Familia = null;
                this.workrack.Rack = null;
                var rectificadores = this.workrack.lineaConfig.rectificadores;
                this.workrack.Corrientes = services.Corrientes.getCorrientesListInZero(rectificadores);

                var multiplicador = services.Corrientes.calculateMultiplier(numPiezasNumber, 0);
                this.workrack.CorrientesString = services.Corrientes.getCorrientesString(this.workrack.Corrientes, ",", multiplicador);


                btn_enviardatos.Enabled = false;
                Oxidado.Hide();
                btn_barra.Enabled = false;

                lbl_oid.Text = "VACIO";
                lbl_job.Text = job;
                lbl_nump.Text = numPiezasNumber.ToString();
                txt_description.Text = "Barra";

                FlowLayoutPanel1.Controls.Clear();

                foreach (services.Corriente corriente in this.workrack.Corrientes) {
                    var tmpObj = new shared.entrada_agregar_rack_corriente();
                    string corrienteString = corriente.corriente.ToString();
                    string corrienteTotal = (corriente.corriente * multiplicador).ToString();
                    tmpObj.FillData(this.workrack.lineaConfig.rectificadoresDictionary[corriente.rectificadorId].rectificador, corrienteString, corrienteTotal);
                    FlowLayoutPanel1.Controls.Add(tmpObj);
                }
                gpb_conf.Show();
            } catch (Exception ex) {
                MessageBox.Show("Error al enviar datos: " + ex.Message);
            }
        }

        private void btn_cargar_Click( object sender, EventArgs e ) {
            if (this.workrack.Barra) {
                if (this.workrack.CargarBarraAPLC()) {
                    this.LimpiarForma();
                }
            } else {
                this.workrack.Oxidado = Oxidado.Checked;
                if (this.workrack.CargarAPLC()) {
                    this.LimpiarForma();
                }
            }

        }

        private void btn_limpiar_Click( object sender, EventArgs e ) {
            this.LimpiarForma();
        }
        public void LimpiarForma() {
            txt_o_job.Text = "";
            txt_o_rack.Text = "";
            txt_o_numpiez.Text = "";
            Oxidado.Checked = false;
            btn_enviardatos.Enabled = true;
            btn_barra.Enabled = true;
            txt_o_job.Focus();
            this.gpb_conf.Hide();
            if (this.workrack.lineaConfig.configuration.PuedoOxidar)
                Oxidado.Show();
            if (this.workrack.lineaConfig.configuration.PuedoEnviarBarras)
                btn_barra.Show();
        }

        private void btn_cancelar_Click( object sender, EventArgs e ) {
            txt_o_job.Focus();
            this.gpb_conf.Hide();
            Oxidado.Checked = false;
            btn_enviardatos.Enabled = true;
            btn_barra.Enabled = true;
            if (this.workrack.lineaConfig.configuration.PuedoOxidar)
                Oxidado.Show();
            if (this.workrack.lineaConfig.configuration.PuedoEnviarBarras)
                btn_barra.Show();
        }

        private void txt_o_numpiez_KeyPress( object sender, KeyPressEventArgs e ) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void updateLblCant() {
            if (this.jobReference == null || txt_o_job.Text.Trim() != this.jobReference.job) {
                try {

                    this.jobReference = this.workrack.lineaConfig.dataDB.getJob(txt_o_job.Text.Trim());
                    lbl_cant.Text = "Cant: " + this.jobReference.numPiezasT.ToString();

                } catch (Exception ex) {
                    lbl_cant.Text = "No Existe";
                    this.jobReference = null;
                }

            }
        }

        private void txt_o_job_Leave( object sender, EventArgs e ) {
            this.updateLblCant();
        }

        private void txt_o_job_KeyPress( object sender, KeyPressEventArgs e ) {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) {
                this.updateLblCant();
            }
        }
    }
}
