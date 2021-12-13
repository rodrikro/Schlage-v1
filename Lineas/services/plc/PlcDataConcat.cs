using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc {
    public class PlcDataConcat {
        private Opc opc;
        private config.configuration configuration;

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        private int id;
        private string plcTag;

        public PlcDataConcat( ref config.configuration config, ref services.Opc opcServer, int id ) {
            this.opc = opcServer;
            this.configuration = config;
            this.id = id;
        }

        public void configureTags() {
            tagnames.Add("DataConcat[" + this.id.ToString() + "]", "PV" + configuration.Linea + "_Out.DataConcat[" + this.id.ToString() + "],L1,C1");
        }

        public void connectTags() {
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                this.opc.AddItem(entry.Key, entry.Value);
            }
        }
        public string getData() {
            var data = this.opc.GetItemValue("DataConcat[" + this.id.ToString() + "]");
            return (string)data;
        }
        public void clear() {
            this.opc.SetItemValue("DataConcat[" + this.id.ToString() + "]", "");
        }
    }
}
