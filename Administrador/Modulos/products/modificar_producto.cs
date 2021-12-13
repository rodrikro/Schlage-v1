using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.products {
    public partial class modificar_producto : Form {
        public modificar_producto() {
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
                    var mpf = new modificar_producto_form();
                    mpf.txt_oid.Text = txt_oid.Text;
                    mpf.MdiParent = this.MdiParent;
                    mpf.Show();
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
