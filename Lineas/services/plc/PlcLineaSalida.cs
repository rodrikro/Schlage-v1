using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.services.plc
{
    public class PlcLineaSalida
    {
        private Opc opc;
        private config.configuration configuration;

        public const string ID_IMONLINE = "imonline"; //Read 1 for is online

        private Dictionary<string, string> tagnames = new Dictionary<string, string>();
        public Dictionary<int, PlcPullOut> pullouts = new Dictionary<int, PlcPullOut>();
        public Dictionary<int, PlcDataConcat> dataconcats = new Dictionary<int, PlcDataConcat>();

        public PlcLineaSalida( ref config.configuration configParam, ref services.Opc opcServerParam ) {
            this.opc = opcServerParam;
            this.configuration = configParam;
            for (int i = 0; i < configuration.ColaSalida; i++) {
                var tmpPLCDC = new PlcDataConcat(ref this.configuration, ref this.opc, i);
                dataconcats.Add(i, tmpPLCDC);
            }
            for (int i = 0; i < configuration.PullOutSalida; i++) {
                var tmpPLCDC = new PlcPullOut(ref this.configuration, ref this.opc, i);
                pullouts.Add(i, tmpPLCDC);
            }
        }

        public void configureTags(){
            tagnames.Add(ID_IMONLINE, "HBL" + configuration.Linea + ",L1,C1");
            foreach (KeyValuePair<int, PlcDataConcat> element in this.dataconcats) {
                element.Value.configureTags();
            }
            foreach (KeyValuePair<int, PlcPullOut> element in this.pullouts) {
                element.Value.configureTags();
            }
        }

        public void connectTags(){
            foreach (KeyValuePair<string, string> entry in this.tagnames) {
                this.opc.AddItem(entry.Key, entry.Value);
            }
            foreach (KeyValuePair<int, PlcDataConcat> element in this.dataconcats) {
                element.Value.connectTags();
            }
            foreach (KeyValuePair<int, PlcPullOut> element in this.pullouts) {
                element.Value.connectTags();
            }
        }

        public bool isOnline() {
            return 1 == (Int16)this.opc.GetItemValue(PlcLineaSalida.ID_IMONLINE);
        }
    }
}
