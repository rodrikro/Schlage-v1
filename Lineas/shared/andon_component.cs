using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Lineas.shared {
    public partial class andon_component : UserControl {
        private services.AndonDB andonDB;
        private config.configuration configuration;
        private services.plc.PlcAndon plcAndon;
        private config.AndonElement andonElement;

        private services.AndonEstado dbInstance;
        private bool hasDelay = false;

        public andon_component() {
            InitializeComponent();

        }


        public void configureLayout( config.AndonElement andonElementParam, services.plc.PlcAndon plcAndonParam, ref config.configuration configParam, ref services.AndonDB andonDBParam ) {
            this.andonDB = andonDBParam;
            this.configuration = configParam;
            this.plcAndon = plcAndonParam;
            this.andonElement = andonElementParam;

            this.dbInstance = this.andonDB.GetDBInstanceID(Convert.ToInt32(this.configuration.Linea), this.andonElement.DatabaseName);

            System.Console.WriteLine("andonElement: " + andonElementParam.Name + " ID: " + this.dbInstance.id);

            title.Text = andonElement.Name;
            setFormStatus();
            this.andonStatusTimer.Enabled = true;
        }

        private void fetchDBAndonStatusAndUpdate() {
            var newAndonStatus = this.andonDB.getStatus(this.dbInstance.id);
            if(newAndonStatus != this.dbInstance.status){
                this.dbInstance.status = newAndonStatus;
                this.setFormStatus();
                this.setPlcStatus();
            }
        }

        private void setFormStatus() {
            System.Console.WriteLine("andonElement: " + this.andonElement.Name + " ID: " + this.dbInstance.id + " Status: " + this.dbInstance.status);
            switch (this.dbInstance.status) { 
                case 1:
                    this.ButtonLlamar.Enabled = true && !this.hasDelay;
                    this.ButtonLlamar.Text = "Ya llegue";
                    this.ButtonLlamar.BackColor = System.Drawing.Color.Red;
                    this.ButtonLlamar.ForeColor = System.Drawing.Color.White;

                    this.ButtonResuelto.Enabled = false && !this.hasDelay;
                    this.ButtonResuelto.Text = "Resuelto";
                    this.ButtonResuelto.BackColor = System.Drawing.Color.Empty;
                    this.ButtonResuelto.ForeColor = System.Drawing.Color.Black;
                    break;
                case 2:
                    this.ButtonLlamar.Enabled = false && !this.hasDelay;
                    this.ButtonLlamar.Text = "Ya llegue";
                    this.ButtonLlamar.BackColor = System.Drawing.Color.Empty;
                    this.ButtonLlamar.ForeColor = System.Drawing.Color.Black;

                    this.ButtonResuelto.Enabled = true && !this.hasDelay;
                    this.ButtonResuelto.Text = "Resuelto";
                    this.ButtonResuelto.BackColor = System.Drawing.Color.Green;
                    this.ButtonResuelto.ForeColor = System.Drawing.Color.White;
                    break;
                default:
                    this.ButtonLlamar.Enabled = true && !this.hasDelay;
                    this.ButtonLlamar.Text = "Llamar";
                    this.ButtonLlamar.BackColor = System.Drawing.Color.Empty;
                    this.ButtonLlamar.ForeColor = System.Drawing.Color.Black;

                    this.ButtonResuelto.Enabled = false && !this.hasDelay;
                    this.ButtonResuelto.Text = "Resuelto";
                    this.ButtonResuelto.BackColor = System.Drawing.Color.Empty;
                    this.ButtonResuelto.ForeColor = System.Drawing.Color.Black;
                    break;
            }
        }

        private void setFormButtonsEnabled(bool enabled) {
            this.ButtonLlamar.Enabled = enabled;
            this.ButtonResuelto.Enabled = enabled;
        }

        private void setPlcStatus() {
            switch (this.dbInstance.status) {
                case 1:
                    this.plcAndon.setSignal(true);
                    this.plcAndon.setAck(false);
                    break;
                case 2:
                    this.plcAndon.setSignal(false);
                    this.plcAndon.setAck(true);
                    break;
                default:
                    this.plcAndon.setSignal(false);
                    this.plcAndon.setAck(false);
                    break;
            }
        }

        private void setDBStatus() { 
            this.andonDB.setAndonStatus(this.dbInstance.id, Convert.ToInt32(this.dbInstance.linea), this.dbInstance.opcion, this.dbInstance.status);
        }
        private void setDBTotals() {
            this.andonDB.calculateAndSaveTotal(this.dbInstance.linea, this.dbInstance.opcion);
        }

        private void ButtonLlamar_Click( object sender, EventArgs e ) {
            if (this.dbInstance.status == 0) {
                this.dbInstance.status = 1;
            } else if (this.dbInstance.status == 1) {
                this.dbInstance.status = 2;
            }
            this.hasDelay = true;
            this.setFormStatus();
            this.setPlcStatus();
            this.setDBStatus();
            this.delayTimer.Enabled = true;
        }

        private void ButtonResuelto_Click( object sender, EventArgs e ) {
            this.dbInstance.status = 0;
            this.hasDelay = true;
            this.setFormStatus();
            this.setPlcStatus();
            this.setDBStatus();
            setDBTotals();
            this.delayTimer.Enabled = true;
        }

        private void andonStatusTimer_Tick( object sender, EventArgs e ) {
            this.fetchDBAndonStatusAndUpdate();
        }

        private void delayTimer_Tick( object sender, EventArgs e ) {
            this.hasDelay = false;
            this.setFormStatus();
            this.delayTimer.Enabled = false;
        }
    }
}
