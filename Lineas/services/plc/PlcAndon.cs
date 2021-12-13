using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc {
    public class PlcAndon {
        private Opc opc;
        private config.configuration configuration;

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        private string id;
        private string plcTag;

        public PlcAndon( ref config.configuration config, ref services.Opc opcServer, string id, string plcTag ) {
            this.opc = opcServer;
            this.configuration = config;
            this.id = id;
            this.plcTag = plcTag;
        }

        public void configureTags() {
            tagnames.Add("ANDON_" + this.id + "_IN", "PV" + configuration.Linea + "_In." + this.plcTag + ",L1,C1");
            tagnames.Add("ANDON_" + this.id + "_IN_ACK", "PV" + configuration.Linea + "_In." + this.plcTag + "_Ack,L1,C1");
        }

        public void setSignal(bool status){
            this.opc.SetItemValue("ANDON_" + this.id + "_IN", status ? "1" : "0");
        }
        public void setAck( bool status ) {
            this.opc.SetItemValue("ANDON_" + this.id + "_IN_ACK", status ? "1" : "0");
        }
        public void connectTags() {
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                System.Console.WriteLine("Connecting " + entry.Key + " - " + entry.Value);  
                this.opc.AddItem(entry.Key, entry.Value);
            }
        }
    }
}
