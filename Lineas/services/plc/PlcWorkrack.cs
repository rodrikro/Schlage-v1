using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc {
    public class PlcWorkrackInfo {
        public string job = "";
        public string rack = "";
        public string oid = "";
        public string numeroDePiezas = "0";
        public int ctr = 0;
        public string corrientes = "";
        public int task = 0;
        public int acabado = 0;
    }
    public class PlcWorkrack {
        private Opc opc;
        private config.configuration configuration;

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        private int id;
        private string tagReference = "";
        private string _tagJob = "InJob";
		private string _tagRack = "InRack";
		private string _tagOid = "InOid";
		private string _tagPiezas = "InPiezas";
		private string _tagCTR = "InCTR";
		private string _tagCorrientes = "InCorrientes";
		private string _tagTask = "InTask";
		private string _tagAcabado = "InAcabado";

        private PlcWorkrackInfo _simulationData = new PlcWorkrackInfo();

        public PlcWorkrack( ref config.configuration config, ref services.Opc opcServer, int id ) {
            this.opc = opcServer;
            this.configuration = config;
            this.id = id;
            this.tagReference = config.Linea;
            if (id > 0) {
                this.tagReference = this.tagReference + id;
            }
            this._tagJob = this._tagJob + "_" + id.ToString();
            this._tagRack = this._tagRack + "_" + id.ToString();
            this._tagOid = this._tagOid + "_" + id.ToString();
            this._tagPiezas = this._tagPiezas + "_" + id.ToString();
            this._tagCTR = this._tagCTR + "_" + id.ToString();
            this._tagCorrientes = this._tagCorrientes + "_" + id.ToString();
            this._tagTask = this._tagTask + "_" + id.ToString();
            this._tagAcabado = this._tagAcabado + "_" + id.ToString();
        }

        public void configureTags() {
            tagnames.Add(this._tagJob, "PV" + this.tagReference + "_In.DataInJob,L1,C1");
            tagnames.Add(this._tagRack, "PV" + this.tagReference + "_In.DataInRack,L1,C1");
            tagnames.Add(this._tagOid, "PV" + this.tagReference + "_In.DataInModel,L1,C1");
            tagnames.Add(this._tagPiezas, "PV" + this.tagReference + "_In.DataInPiezas,L1,C1");
            tagnames.Add(this._tagCTR, "PV" + this.tagReference + "_In.CTR,L1,C1");
            tagnames.Add(this._tagCorrientes, "PV" + this.tagReference + "_In.SQLCorriente,L1,C1");
            tagnames.Add(this._tagTask, "PV" + this.tagReference + "_In.Task_ID,L1,C1");
            tagnames.Add(this._tagAcabado, "PV" + this.tagReference + "_In.DataInNum,L1,C1");
        }

        public void Limpiar() {
            if (this.opc.isSimulation()) {
                this._simulationData = new PlcWorkrackInfo();
            } else {
                this.opc.SetItemValue(this._tagJob, "");
                this.opc.SetItemValue(this._tagRack, "");
                this.opc.SetItemValue(this._tagOid, "");
                this.opc.SetItemValue(this._tagPiezas, "");
                this.opc.SetItemValue(this._tagCTR, Convert.ToInt16(0));
                this.opc.SetItemValue(this._tagCorrientes, "");
                this.opc.SetItemValue(this._tagTask, 0);
                this.opc.SetItemValue(this._tagAcabado, 0);
            }
        }

        public void CargarInfo( string job, string rack, string oid, int NumeroPiezas, string corrientes, int task, int acabado ) {
            if (this.opc.isSimulation()) {
                var newSimulationData = new PlcWorkrackInfo();
                newSimulationData.job = job;
                newSimulationData.rack = rack;
                newSimulationData.oid = oid;
                newSimulationData.numeroDePiezas = NumeroPiezas.ToString();
                newSimulationData.ctr = 1;
                newSimulationData.corrientes = corrientes;
                newSimulationData.task = task;
                newSimulationData.acabado = acabado;
                this._simulationData = newSimulationData;
            } else {
                this.opc.SetItemValue(this._tagJob, job);
                this.opc.SetItemValue(this._tagRack, rack);
                this.opc.SetItemValue(this._tagOid, oid);
                this.opc.SetItemValue(this._tagPiezas, NumeroPiezas.ToString());
                this.opc.SetItemValue(this._tagCTR, Convert.ToInt16(1));
                this.opc.SetItemValue(this._tagCorrientes, corrientes);
                this.opc.SetItemValue(this._tagTask, task);
                this.opc.SetItemValue(this._tagAcabado, acabado);
            }
        }

        public PlcWorkrackInfo getInfo() {
            if (this.opc.isSimulation()) {
                return this._simulationData;
            } else {
                var info = new PlcWorkrackInfo();
                info.job = (string)this.opc.GetItemValue(this._tagJob);
                info.rack = (string)this.opc.GetItemValue(this._tagRack);
                info.oid = (string)this.opc.GetItemValue(this._tagOid);
                //info.numeroDePiezas = ((Int32)this.opc.GetItemValue(this._tagPiezas)).ToString();
                info.numeroDePiezas = (string)this.opc.GetItemValue(this._tagPiezas);
                info.ctr = (Int16)this.opc.GetItemValue(this._tagCTR);
                info.corrientes = (string)this.opc.GetItemValue(this._tagCorrientes);
                info.task = (int)this.opc.GetItemValue(this._tagTask);
                info.acabado = (int)this.opc.GetItemValue(this._tagAcabado);
                return info;
            }
        }

        public void connectTags() {
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                this.opc.AddItem(entry.Key, entry.Value);
            }
        }
    }
}
