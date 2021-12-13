using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class entrada_agregar_rack_corriente : UserControl {
        public entrada_agregar_rack_corriente() {
            InitializeComponent();
        }
        public void FillData(string rid, string corrienteIndividual, string corrienteTotal) {
            this.lbl_rect.Text = rid;
            this.lbl_corr.Text = corrienteIndividual;
            this.lbl_tot.Text = corrienteTotal;
        }
    }
}
