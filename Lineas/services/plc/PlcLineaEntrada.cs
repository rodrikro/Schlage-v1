using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc
{
    public class PlcLineaEntrada
    {
        private Opc opc;
        private config.configuration configuration;

        public const string ID_IMONLINE = "imonline"; //Read 1 for is online
        const string ID_LINEAACTION = "lineaaction"; //Set 1 and return 0 for line action
        const string ID_LINEASTATUS = "lineastatus"; //Get if line status is in cycle or stopped
        const string ID_PUEDOESCRIBIR = "puedoescribir"; //If I can send the items to PLC
        const string ID_SIGCICLO = "siguienteciclo"; //If we send data to next cycle

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        public Dictionary<int, PlcWorkrack> workracks = new Dictionary<int, PlcWorkrack>();

        private System.Timers.Timer lineactionTimer = new System.Timers.Timer(500);

        public PlcLineaEntrada( ref config.configuration configParam, ref services.Opc opcServerParam ) {
            this.opc = opcServerParam;
            this.configuration = configParam;
            lineactionTimer.Elapsed += lineactionElapsed;
            var workracksNumber = configuration.Workbars * configuration.Racksworkbars;
            if (workracksNumber == 1) {
                workracks.Add(0, new PlcWorkrack(ref this.configuration, ref this.opc, 0));
            } else {
                for (int i = 1; i <= workracksNumber; i++) {
                    workracks.Add(i, new PlcWorkrack(ref this.configuration, ref this.opc, i));
                }
            }
        }

        public void configureTags(){
            tagnames.Add(ID_IMONLINE, "HBL" + configuration.Linea + ",L1,C1");
            
            tagnames.Add(ID_LINEAACTION, "Cycle" + configuration.Linea + ".FT_StartCycle,L1,C1");
            tagnames.Add(ID_LINEASTATUS, "Cycle" + configuration.Linea + ".InCycle,L1,C1");
            if (!this.configuration.SiguienteCicloAutomatico) {
                tagnames.Add(ID_PUEDOESCRIBIR, "Cycle" + configuration.Linea + ".VB_Load,L1,C1");
                tagnames.Add(ID_SIGCICLO, "Cycle" + configuration.Linea + ".VB_Next,L1,C1");
            }
            foreach (KeyValuePair<int, PlcWorkrack> element in this.workracks) {
                element.Value.configureTags();
            }
        }

        public void connectTags(){
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                this.opc.AddItem(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<int, PlcWorkrack> element in this.workracks) {
                element.Value.connectTags();
            }
        }

        public bool isOnline() {
            if (this.opc.isSimulation())
                return true;
            return (Int16)this.opc.GetItemValue(PlcLineaEntrada.ID_IMONLINE) == 1;
        }

        public bool isInCycle() {
            if (this.opc.isSimulation())
                return false;
            return (Int16)this.opc.GetItemValue(PlcLineaEntrada.ID_LINEASTATUS) == 1;
        }

        public bool canLoadData() {
            if (this.opc.isSimulation()) {
                return true;
            }
            if (this.configuration.SiguienteCicloAutomatico) {
                return true;
            }
            return (Int16)this.opc.GetItemValue(PlcLineaEntrada.ID_PUEDOESCRIBIR) == 1;
        }

        public void setNextCycle() {
            if (!this.configuration.SiguienteCicloAutomatico) {
                this.opc.SetItemValue(ID_SIGCICLO, 1);
            }
        }

        public void setLineAction() {
            this.opc.SetItemValue(ID_LINEAACTION, 1);
            this.lineactionTimer.Enabled = true;
        }

        private void lineactionElapsed( Object source, System.Timers.ElapsedEventArgs e ) {
            this.opc.SetItemValue(ID_LINEAACTION, 0);
            this.lineactionTimer.Enabled = false;
        }
    }
}
