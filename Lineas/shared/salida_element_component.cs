using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class salida_element_component : UserControl {

        private string linea;
        private int indexDB;
        private int rack;
        private string job;
        private VerticalFlowPanel parentReference;
        private services.DataDB dataDB;
        public paqueteSalidaPlc PaqueteSalidaActual = null;

        public salida_element_component() {
            InitializeComponent();
        }

        private void salida_element_component_Load( object sender, EventArgs e ) {

        }
        public void SetReferences( ref VerticalFlowPanel parent, ref services.DataDB dataDBParam, ref paqueteSalidaPlc paqueteSalida) {
            this.dataDB = dataDBParam;
            this.parentReference = parent;
            this.PaqueteSalidaActual = paqueteSalida;
        }
        public void SetData(string lineaParam, int indexDBParam, string jobParam, string descriptionParam, string oidParam, int numeroPiezasParam, int rackParam) {
            this.linea = lineaParam;
            this.indexDB = indexDBParam;
            this.job = jobParam;
            lbl_job.Text = jobParam;
            lbl_descripcion.Text = descriptionParam;
            lbl_oid.Text = oidParam;
            txt_nps.Text = numeroPiezasParam.ToString();
            this.rack = rackParam;
            group.Text = "Rack :" + rackParam.ToString();
        }

        private void Button1_Click( object sender, EventArgs e ) {
            try {
                var cantidadDePiezas = Convert.ToInt32(txt_nps.Text);
                var descargado = this.dataDB.DescargarTarea(this.linea, this.indexDB, cantidadDePiezas, lbl_job.Text, this.rack);
                var rackDisponible = this.dataDB.setRackStatus(this.rack, false);
                if (descargado) {
                    //Quitamos la forma de la lista
                    this.parentReference.Controls.Remove(this);

                    //Iniciamos la expiracion para evitar duplicados ya que el plc copia y mueve informacion
                    this.PaqueteSalidaActual.doCleanup();

                    //Borramos la forma
                    this.Dispose();
                }
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
