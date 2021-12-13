using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Lineas.config {
    public partial class configuration_form : Form {
        private string tipo = "";
        private bool andonConnectionValid = false;
        private bool dataConnectionValid = false;
        private bool opcConnectionValid = false;
        public configuration_form() {
            InitializeComponent();
        }

        private void comboBox1_Selection( object sender, EventArgs e ) {
            ComboBox combo = (ComboBox)sender;
            tipo = combo.SelectedItem.ToString().ToLower();
            switch (tipo) {
                case "entrada":
                    label17.Visible = false;
                    txt_colas.Visible = false;
                    break;
                case "salida":
                    label17.Visible = true;
                    txt_colas.Visible = true;
                    break;
            }
        }

        private void btn_t1_Click( object sender, EventArgs e ) {
            var DBConn = new SqlConnection();
            DBConn.ConnectionString = "Server=" + txt_and_server.Text + ";Database=" + txt_and_db.Text + ";User ID=" + txt_and_user.Text + ";Password=" + txt_and_password.Text + ";Trusted_Connection=False; Timeout=5;";
            try {
                DBConn.Open();
                andonConnectionValid = true;
                MessageBox.Show("Conexión exitosa");
            } catch(Exception ex){
                andonConnectionValid = false;
                MessageBox.Show("No pude conectarme a la base de datos: " + ex.Message);
            } finally {
                DBConn.Close();
            }
        }

        private void btn_t3_Click( object sender, EventArgs e ) {
            var DBConn = new SqlConnection();
            DBConn.ConnectionString = "Server=" + txt_app_server.Text + ";Database=" + txt_app_db.Text + ";User ID=" + txt_app_user.Text + ";Password=" + txt_app_pass.Text + ";Trusted_Connection=False; Timeout=5;";
            try {
                DBConn.Open();
                dataConnectionValid = true;
                MessageBox.Show("Conexión exitosa");
            } catch (Exception ex) {
                dataConnectionValid = false;
                MessageBox.Show("No pude conectarme a la base de datos: " + ex.Message);
            } finally {
                DBConn.Close();
            }
        }

        private void btn_t2_Click( object sender, EventArgs e ) {
            services.Opc opcserver = new services.Opc(txt_opc_server.Text, txt_opc_topic.Text);
            try {
                opcserver.Connect();
                if(opcserver.IsConnected()){
                    opcConnectionValid = true;
                    MessageBox.Show("Conexión exitosa");
                } else {
                    opcConnectionValid = false;
                    MessageBox.Show("No pude conectarme al OPC");
                }
            } catch(Exception ex){
                opcConnectionValid = false;
                MessageBox.Show("No pude conectarme al OPC: " + ex.Message);
            }
        }

        private void btn_save_Click( object sender, EventArgs e ) {
            var config = new configuration();
            config.isSet = 1;
            config.AndonConnectionString = "Server=" + txt_and_server.Text + ";Database=" + txt_and_db.Text + ";User ID=" + txt_and_user.Text + ";Password=" + txt_and_password.Text + ";Trusted_Connection=False; Timeout=10;";
            config.DataConnectionString = "Server=" + txt_app_server.Text + ";Database=" + txt_app_db.Text + ";User ID=" + txt_app_user.Text + ";Password=" + txt_app_pass.Text + ";Trusted_Connection=False; Timeout=10;";
            config.OpcServerString = txt_opc_server.Text;
            config.OpcTopic = txt_opc_topic.Text;
            config.Linea = txt_linea.Text;
            
            config.AndonElements.Add(new AndonElement("Mantenimiento", "mantenimiento", "Mannto"));
            config.AndonElements.Add(new AndonElement("Materiales", "materiales", "Mat"));
            config.AndonElements.Add(new AndonElement("Producción", "produccion", "Prod"));
            config.AndonElements.Add(new AndonElement("Calidad", "calidad", "Calidad"));
            
            if (config.Linea.Trim() == "") {
                MessageBox.Show("Especifica una linea.");
                return;
            }

            config.Workbars = Convert.ToInt32(txt_wb.Text);
            if (config.Workbars < 1) {
                MessageBox.Show("Checar el Workbar Mayor a 0.");
                return;
            }
            config.Racksworkbars = Convert.ToInt32(txt_rw.Text);
            if (config.Racksworkbars < 1) {
                MessageBox.Show("Checar los Racks por Workbar Mayor a 0.");
                return;
            }
            
            config.Stationnames = txt_sn.Text.Split(',');
            if (config.Stationnames.Length != config.Workbars) {
                MessageBox.Show("Los nombres deben conicidir con la cantidad de racks y workbars.");
                return;
            }

            config.Acabados = txt_acabados.Text.Split(',');
            if (config.Acabados.Length == 0) {
                MessageBox.Show("Debe haber al menos un acabado.");
                return;
            }

            if (tipo.Trim() == "") {
                MessageBox.Show("Especifica el tipo.");
                return;
            }
            config.Tipo = tipo;

            if (tipo == "salida" && Convert.ToInt32(txt_colas.Text) <= 0) {
                MessageBox.Show("Colas debe ser mayor a 0;");
                return;
            }
            if (tipo == "salida") {
                config.ColaSalida = Convert.ToInt32(txt_colas.Text);
            } else {
                config.ColaSalida = 1;
            }
            configuration.saveConfig(ref config);
        }

        private void Timer1_Tick( object sender, EventArgs e ) {
            if (andonConnectionValid && dataConnectionValid && opcConnectionValid) {
                btn_save.Enabled = true;
            } else {
                btn_save.Enabled = false;
            }
        }
    }
}
