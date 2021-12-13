using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class salida_barraplc_component : UserControl {

        private VerticalFlowPanel parentReference;
        public paqueteSalidaPlc PaqueteSalidaActual = null;

        public salida_barraplc_component() {
            InitializeComponent();
        }

        private void salida_element_component_Load( object sender, EventArgs e ) {

        }

        public void SetBarraType( string type ) {
            if (type == "db") {
                this.lbl_job.Text = "BARRA APP DE ENTRADA";
            } else if (type == "plc") {
                this.lbl_job.Text = "BARRA DE PLC";
            } else {
                this.lbl_job.Text = "BARRA DE PLC";
            }
        }
        public void SetReferences( ref VerticalFlowPanel parent, ref paqueteSalidaPlc paqueteSalida) {
            this.parentReference = parent;
            this.PaqueteSalidaActual = paqueteSalida;
        }

        private void Button1_Click( object sender, EventArgs e ) {
            try {
                //Quitamos la forma de la lista
                this.parentReference.Controls.Remove(this);

                //Iniciamos la expiracion para evitar duplicados ya que el plc copia y mueve informacion
                this.PaqueteSalidaActual.doCleanup();

                //Borramos la forma
                this.Dispose();
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
