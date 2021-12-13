using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Timers;

namespace Administrador {
    public partial class Inicio : Form {

        private DateTime LastActionTime = new DateTime();
        private static System.Timers.Timer shutdownappTimer;
        public string currentUser = "";
        public Modulos.emails.EmailServer emailServer = new Modulos.emails.EmailServer();

        public Inicio() {
            InitializeComponent();
        }

        private void updateLastActionTime() {
            this.LastActionTime = DateTime.Now;
        }

        private void checkTimer( Object source, ElapsedEventArgs e ) { 
            var dateDifferenceMillis = DateTime.Now - this.LastActionTime;
            var dateTimeDifferenceMinutes = dateDifferenceMillis.TotalMinutes;
            if(dateTimeDifferenceMinutes > 30){
                Application.Exit();
            }
        }

        private void setShutdownTimer() {
            this.updateLastActionTime();
            shutdownappTimer = new System.Timers.Timer(1000 * 5);
            shutdownappTimer.Elapsed += checkTimer;
            shutdownappTimer.AutoReset = true;
            shutdownappTimer.Enabled = true;
        }

        private void SalirToolStripMenuItem_Click( object sender, EventArgs e ) {
            Application.Exit();
        }

        private void sobreElProgramaToolStripMenuItem1_Click( object sender, EventArgs e ) {
            MessageBox.Show("Developed by Miguel Gonzalez <miguel.gonzalez@intzen.com>");
            this.updateLastActionTime();
        }

        private void ProductosPorFamiliasToolStripMenuItem_Click( object sender, EventArgs e ) {
            var familiaWin = new Modulos.reportes.productos_por_familia();
            familiaWin.MdiParent = this;
            familiaWin.Show();
            this.updateLastActionTime();
        }

        private void ProductosPorAcabadoToolStripMenuItem_Click( object sender, EventArgs e ) {
            var acabadoWin = new Modulos.reportes.productos_por_acabado();
            acabadoWin.MdiParent = this;
            acabadoWin.Show();
            this.updateLastActionTime();
        }

        private void ReporteDeProductosCalidadToolStripMenuItem_Click( object sender, EventArgs e ) {
            var productosCalidadWin = new Modulos.reportes.productos_calidad();
            productosCalidadWin.MdiParent = this;
            productosCalidadWin.Show();
            this.updateLastActionTime();
        }

        private void ParámetrosDeProductoToolStripMenuItem_Click( object sender, EventArgs e ) {
            var descripcionProductoWin = new Modulos.reportes.descripcion_producto();
            descripcionProductoWin.MdiParent = this;
            descripcionProductoWin.Show();
            this.updateLastActionTime();
        }

        private void AdministrarFamiliasToolStripMenuItem_Click( object sender, EventArgs e ) {
            var administrarFamiliasWin = new Modulos.administrar_familias();
            administrarFamiliasWin.MdiParent = this;
            administrarFamiliasWin.Show();
            this.updateLastActionTime();
        }

        private void AltaToolStripMenuItem_Click( object sender, EventArgs e ) {
            var crearProductoWin = new Modulos.products.crear_producto();
            crearProductoWin.MdiParent = this;
            crearProductoWin.Show();
            this.updateLastActionTime();
        }

        private void ModificarToolStripMenuItem_Click( object sender, EventArgs e ) {
            var modificarProductoWin = new Modulos.products.modificar_producto();
            modificarProductoWin.MdiParent = this;
            modificarProductoWin.Show();
            this.updateLastActionTime();
        }

        private void Inicio_Load( object sender, EventArgs e ) {
            // Read a particular key from the config file
            MenuStrip1.Enabled = false;
            try {
                var connectionStrings = ConfigurationManager.ConnectionStrings;
                var connectionString = connectionStrings["Administrador.Properties.Settings.productsConnectionString"];
                if (connectionString == null) {
                    MenuStrip1.Enabled = false;
                } else {
                    var result = Modulos.configuration.dbTester.tryConnectionString(connectionString.ToString());
                    if (result) {
                        MenuStrip1.Enabled = true;
                        setShutdownTimer();
                    } else {
                        MenuStrip1.Enabled = false;
                    }
                }
            } catch (Exception ex) {
                MenuStrip1.Enabled = false;
                MessageBox.Show("Error: " + ex.Message);
            }

            if (!MenuStrip1.Enabled) {
                var button = MessageBox.Show("No es posible conectarse a la base de datos, ¿Desea actualizar la configuración?", "Error", MessageBoxButtons.YesNo);
                if (button == System.Windows.Forms.DialogResult.Yes) {
                    var configurationManagerWin = new Modulos.configuration.configuration_editor();
                    configurationManagerWin.MdiParent = this;
                    configurationManagerWin.Show();
                } else {
                    Application.Exit();
                }
            }

            //Configure EmailServer
            var configuration = Administrador.Properties.Settings.Default;
            var fromEmail = (string)configuration.emailFrom;
            var toEmail = (string)configuration.emailTo;
            emailServer.configure(fromEmail, toEmail);

        }
        public void setCurrentUser( string user ) {
            this.currentUser = user;
            this.Text = this.Text;
        }
    }
}
