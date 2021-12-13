using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lineas.pantallas {
    public partial class entrada : Form, shared.ILinea {
        public entrada() {
            InitializeComponent();
        }
        private config.configuration configuration;
        private services.DataDB dataDB;
        private services.Opc opcServer;
        public services.plc.PlcLineaEntrada lineaEntrada;

        private shared.lineaEntrada lineaConfig;

        private bool canWrite = false;
        private Dictionary<int, shared.workrack> workracks = new Dictionary<int, shared.workrack>();
        private int currentViewWorkrackID = -1;
        private string currentViewTag = "";

        public void ConfigurarLinea( ref config.configuration configParam, ref services.DataDB dataDBParam, ref services.Opc opcServerParam ) {
            this.configuration = configParam;
            this.dataDB = dataDBParam;
            this.opcServer = opcServerParam;

            this.btn_cycleNext.Visible = !this.configuration.SiguienteCicloAutomatico;

            this.Text = "Linea " + this.configuration.Linea + " Entrada";

            this.lineaConfig = new shared.lineaEntrada();
            this.lineaConfig.configuration = this.configuration;
            this.lineaConfig.dataDB = this.dataDB;
            this.lineaConfig.plcEntrada = this.lineaEntrada;
            
            int workracks = this.configuration.Workbars * this.configuration.Racksworkbars;
            if (workracks == 4 && this.configuration.Linea == "300") {
                var rack1Group = new GroupBox();
                rack1Group.Text = "Rack 1";
                rack1Group.Width = 360;
                rack1Group.Height= 190;
                
                var rack2Group = new GroupBox();
                rack2Group.Text = "Rack 2";
                rack2Group.Width = 360;
                rack2Group.Height = 190;

                flowLayoutPanel1.Controls.Add(rack1Group);
                flowLayoutPanel1.Controls.Add(rack2Group);

                for (int i = 0; i < workracks; i++) {
                    var tmpWorkRack = new shared.workrack();
                    tmpWorkRack.lineaConfig = this.lineaConfig;
                    tmpWorkRack.plcID = i + 1; //En el PLC todo es 1,2,3,4 y no 0 en caso de solo 1 workrack
                    tmpWorkRack.id = i;
                    tmpWorkRack.stationName = this.configuration.Stationnames[i];

                    var tmpE = new shared.rack_entrada_chico();
                    tmpE.workrack = tmpWorkRack;
                    tmpE.ViewIntention += rackControlsViewIntention;

                    if (i % 2 == 1) {
                        rack2Group.Controls.Add(tmpE);
                        int position = i / 2;
                        tmpE.Location = new Point(5, 15 + 80 * position);
                    } else {
                        rack1Group.Controls.Add(tmpE);
                        int position = i / 2;
                        tmpE.Location = new Point(5, 15 + 80 * position);
                    }

                    tmpWorkRack.setVistaRackEntrada(tmpE);

                    this.workracks.Add(i, tmpWorkRack);
                }

            } else if(workracks == 1) {
                var tmpWorkRack = new shared.workrack();
                tmpWorkRack.lineaConfig = this.lineaConfig;
                tmpWorkRack.plcID = 0;
                tmpWorkRack.id = 0;
                tmpWorkRack.stationName = this.configuration.Stationnames[0];
                var tmpE = new shared.rack_entrada_grande();
                tmpE.workrack = tmpWorkRack;
                tmpE.ViewIntention += rackControlsViewIntention;
                flowLayoutPanel1.Controls.Add(tmpE);
                tmpWorkRack.setVistaRackEntrada(tmpE);
                this.workracks.Add(0, tmpWorkRack);
            } else {
                bool interfazGrande = this.configuration.Racksworkbars == 1;
                for (int i = 0; i < workracks; i++) {
                    var tmpWorkRack = new shared.workrack();
                    tmpWorkRack.lineaConfig = this.lineaConfig;
                    tmpWorkRack.plcID = i + 1; //En el PLC todo es 1,2,3,4 y no 0 en caso de solo 1 workrack
                    tmpWorkRack.id = i;
                    tmpWorkRack.stationName = this.configuration.Stationnames[i];
                    if (interfazGrande) {
                        var tmpE = new shared.rack_entrada_grande();
                        tmpE.workrack = tmpWorkRack;
                        tmpE.ViewIntention += rackControlsViewIntention;
                        flowLayoutPanel1.Controls.Add(tmpE);
                        tmpWorkRack.setVistaRackEntrada(tmpE);
                    } else {
                        var tmpE = new shared.rack_entrada_chico();
                        tmpE.workrack = tmpWorkRack;
                        tmpE.ViewIntention += rackControlsViewIntention;
                        flowLayoutPanel1.Controls.Add(tmpE);
                        tmpWorkRack.setVistaRackEntrada(tmpE);
                    }
                    this.workracks.Add(i, tmpWorkRack);
                }
            }
            cleanActionView();
            traerDatosDeLinea();
            plcDataTimer.Enabled = true;
            this.KeyPreview = true;
        }
        public void rackControlsViewIntention( shared.Irack_entrada rackView, EventArgs e ) {
            var workrack = rackView.getWorkrack();
            this.currentViewWorkrackID = workrack.id;
            this.showAction();
        }

        public void showAction() {
            if (this.currentViewWorkrackID != -1 && this.canWrite) {
                var currentWorkRack = this.workracks[this.currentViewWorkrackID];
                var newViewTag = this.currentViewWorkrackID.ToString() + "_" + currentWorkRack.isInUse.ToString();
                if (currentViewTag != newViewTag) {
                    gp_data.Controls.Clear();
                    if (currentWorkRack.isInUse) {
                        //Ver
                        gp_data.Text = "Estación " + currentWorkRack.stationName + " - Ver";
                        currentWorkRack.formularioLectura.Called();
                        gp_data.Controls.Add(currentWorkRack.formularioLectura);
                    } else {
                        //Agregar
                        gp_data.Text = "Estación " + currentWorkRack.stationName + " - Agregar";
                        gp_data.Controls.Add(currentWorkRack.formularioEscritura);
                    }
                    gp_data.Show();
                    this.currentViewTag = newViewTag;
                }
            } else {
                this.gp_data.Hide();
            }
        }

        private void cleanActionView(){
            this.currentViewWorkrackID = -1;
            gp_data.Controls.Clear();
            gp_data.Hide();
        }

        private void Button1_Click( object sender, EventArgs e ) {
            Process.Start("osk.exe");
        }

        private void Button2_Click( object sender, EventArgs e ) {
            Process.Start("calc.exe");
        }

        private void plcDataTimer_Tick( object sender, EventArgs e ) {
            var isInCycle = this.lineaEntrada.isInCycle();
            var canWrite = this.lineaEntrada.canLoadData();
            this.lineaConfig.canWrite = canWrite;
            this.lineaConfig.inCycle = canWrite;
            if (isInCycle) {
                lineStat.Text = "En ciclo";
                lineStat.BackColor = Color.Green;
            } else {
                lineStat.Text = "Detenida";
                lineStat.BackColor = Color.Red;
            }
            this.canWrite = canWrite;
            btn_cycleNext.Enabled = canWrite;
            foreach (shared.workrack workrack in this.workracks.Values) {
                workrack.UpdateWithPLCInfo();
            }
            this.showAction();
        }

        private void lineStat_Click( object sender, EventArgs e ) {
            this.lineaEntrada.setLineAction();
        }

        private void btn_cycleNext_Click( object sender, EventArgs e ) {
            this.lineaEntrada.setNextCycle();
        }

        private void dataLinea_Tick( object sender, EventArgs e ) {
            traerDatosDeLinea();
        }
        private void traerDatosDeLinea() {
            try {
                var familias = this.dataDB.getFamilias();
                Dictionary<string, services.Familia> familiasDict = new Dictionary<string, services.Familia>();
                foreach (services.Familia familia in familias) {
                    familiasDict.Add(familia.familia, familia);
                }
                var lineaInt = Convert.ToInt32(this.configuration.Linea);
                var rectificadores = this.dataDB.getRectificadoresDeLinea(lineaInt);

                var rectificadoresDictionary = new Dictionary<int, services.Rectificadores>();
                foreach (services.Rectificadores rectificador in rectificadores) { 
                    rectificadoresDictionary.Add(rectificador.id, rectificador);
                }

                this.lineaConfig.familias = familiasDict;
                this.lineaConfig.rectificadores = rectificadores;
                this.lineaConfig.rectificadoresDictionary = rectificadoresDictionary;
                this.dataLinea.Interval = 5 * 60 * 1000;
            } catch (Exception ex) {
                MessageBox.Show("Error al traer datos de linea: " + ex.Message);
                this.dataLinea.Interval = 5 * 1000;
            }
        }

        private void entrada_KeyDown( object sender, KeyEventArgs e ) {
            if (e.KeyCode == Keys.F3 && this.currentViewWorkrackID != -1) {
                var currentWorkRack = this.workracks[this.currentViewWorkrackID];
                currentWorkRack.LimpiarPLC();
            }
        }
    }
}
