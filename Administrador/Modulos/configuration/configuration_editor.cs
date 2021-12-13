using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.configuration {
    public partial class configuration_editor : Form {
        public configuration_editor() {
            InitializeComponent();
        }

        private void Button1_Click( object sender, EventArgs e ) {
            var newConnectionString = "Data Source=" + txt_server.Text + ";Initial Catalog=" + txt_bd.Text + ";Persist Security Info=True;User ID=" + txt_user.Text + ";Password=" + txt_psw.Text;
            var result = dbTester.tryConnectionString(newConnectionString);
            if (result) {
                configFileSettings.UpdateConnectionString("Administrador.Properties.Settings.productsConnectionString", newConnectionString);
                MessageBox.Show("Actualizado satisfactoriamente, se reiniciará el software.");
                Application.Restart();
            }
        }
    }
}
