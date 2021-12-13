using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class entrada_lectura_rack : UserControl {
        public shared.workrack workrack;
        public entrada_lectura_rack() {
            InitializeComponent();
        }
        public void Called() {
            if (this.workrack.plcInfo != null) {
                lbl_job.Text = this.workrack.plcInfo.job;
                lbl_rack.Text = this.workrack.plcInfo.rack;
                lbl_np.Text = this.workrack.plcInfo.numeroDePiezas.ToString();
                lbl_oid.Text = this.workrack.plcInfo.oid;
                lbl_acabado.Text = this.workrack.plcInfo.acabado.ToString();
            }
        }

        private void Button1_Click( object sender, EventArgs e ) {
            DialogResult dialogResult = MessageBox.Show("¿Eliminar datos del PLC?", "Eliminar datos", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                //do something
                if (this.workrack.EliminarTareaDePLC()) {

                }
            } else if (dialogResult == DialogResult.No) {
                //do something else
            }

        }
    }
}
