using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc {
    public class PlcPullOut {
        private Opc opc;
        private config.configuration configuration;

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        private int id;
        private string plcTag;

        public PlcPullOut( ref config.configuration config, ref services.Opc opcServer, int id ) {
            this.opc = opcServer;
            this.configuration = config;
            this.id = id;
        }

        public void configureTags() {
            tagnames.Add("PullOut[" + this.id.ToString() + "]", "PV" + configuration.Linea + "_Out.PullOut[" + this.id.ToString() + "],L1,C1");
        }

        public void connectTags() {
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                this.opc.AddItem(entry.Key, entry.Value);
            }
        }

        public string getData() {
            var data = this.opc.GetItemValue("PullOut[" + this.id.ToString() + "]");
            return (string)data;
        }
        public void clear() {
            this.opc.SetItemValue("PullOut[" + this.id.ToString() + "]", "");
        }
    }
}
