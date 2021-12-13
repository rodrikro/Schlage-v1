using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.reportes {
    public partial class descripcion_producto : Form {
        public descripcion_producto() {
            InitializeComponent();
        }

        private void txt_oid_KeyDown( object sender, KeyEventArgs e ) {
            if (e.KeyCode == Keys.Enter) {
                Button1_Click(sender, e);
            }
        }

        private void Button1_Click( object sender, EventArgs e ) {
            if (txt_oid.Text.Trim() != "") {
                var productDescriptionTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
                var has = (int)productDescriptionTableAdapter.GetByOID(txt_oid.Text);
                if(has == 1){
                    var reporte = new descripcion_producto_reporte();
                    reporte.CargarReporte(txt_oid.Text);
                    reporte.Show();
                    this.Close();
                } else {
                    MessageBox.Show("No existe el producto.");
                }
            } else {
                MessageBox.Show("Escribe un Dato.");
            }
        }
    }
}
