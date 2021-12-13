using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class rack_entrada_grande : UserControl, shared.Irack_entrada {
        public shared.workrack workrack;

        public rack_entrada_grande() {
            InitializeComponent();
        }
        private void rack_entrada_grande_Load( object sender, EventArgs e ) {
            this.lblNombreEstacion.Text = this.workrack.stationName;
            this.clearInUse();
            this.canWriteTimer.Enabled = true;
        }
        public shared.workrack getWorkrack() {
            return this.workrack;
        }
        public void setInUseBarra() {
            this.lblEstado.Text = "En Uso";
            this.lblContenido.Text = "[BARRA]";
            this.label4.Visible = true;
            this.lblContenido.Visible = true;
            this.btnAction.Text = "Ver";
        }
        public void setInUse(string rack, string piezas, string job ) {
            this.lblEstado.Text = "En Uso";
            this.lblContenido.Text = "[" + rack + " - " + piezas + "] " + job;
            this.label4.Visible = true;
            this.lblContenido.Visible = true;
            this.btnAction.Visible = true;
            this.btnAction.Text = "Ver";
        }
        public void clearInUse() {
            this.lblContenido.Visible = false;
            this.label4.Visible = false;
            this.lblEstado.Text = "Disponible";
            this.btnAction.Text = "Agregar Contenido";
        }
        public event RackEntradaViewContentEventHandler ViewIntention;

        private void button1_Click( object sender, EventArgs e ) {
            if (this.ViewIntention != null) {
                ViewIntention(this, new RackEntradaViewContentEventArgs("Clicked"));
            }
        }

        private void canWriteTimer_Tick( object sender, EventArgs e ) {
            this.btnAction.Enabled = this.workrack.lineaConfig.canWrite || ( this.workrack.plcInfo != null && this.workrack.plcInfo.ctr == 1);
        }
    }
}
