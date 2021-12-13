using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public class paqueteSalidaPlc {
        public bool hasData = false;
        public string modelo = "";
        public string rack = "";
        public int indexdb = 0;
        public bool isBarraText = false;

        public bool inCleanup = false;

        public SlotSalida SlotSalida = null;

        private Dictionary<int, shared.paqueteSalidaPlc> paquetesDeSalidaRef;
        private System.Timers.Timer expirationTimer;

        public paqueteSalidaPlc( string dataFromPLC ) {
            if (dataFromPLC == null) {
                return;
            }
            try {
                var cleanData = dataFromPLC.Trim();
                if (cleanData.ToUpper().Equals("BARRA")) {
                    this.hasData = true;
                    this.isBarraText = true;
                } else if (!cleanData.Equals("") && !cleanData.Equals("0")) {
                    this.hasData = true;
                    var dataParts = cleanData.Split('/');
                    if (dataParts.Length < 5) {
                        throw new Exception("Dato no tiene IndexDB |" + cleanData + "|");
                    }
                    this.modelo = dataParts[0];
                    this.rack = dataParts[1];
                    this.indexdb = Convert.ToInt32(dataParts[4]);
                } else {
                    this.hasData = false;
                }
            } catch (Exception ex) {
                System.Console.WriteLine("Something happened: " + ex.Message);
                throw new Exception("No puedo parsear dato de salida. " + ex.Message);
            }
        }

        public void setParentListReference( ref Dictionary<int, shared.paqueteSalidaPlc> paquetesDeSalidaParam) {
            this.paquetesDeSalidaRef = paquetesDeSalidaParam;
        }

        public void startExpiration() {
            this.expirationTimer = new System.Timers.Timer(5 * 60 * 1000);
            this.expirationTimer.Enabled = true;
            this.expirationTimer.Elapsed += expirationTimerElapsed;
        }

        public void expirationTimerElapsed( Object source, System.Timers.ElapsedEventArgs e ) {
            this.expirationTimer.Enabled = false;
            this.removeFromList();
        }

        public void doCleanup() {
            this.inCleanup = true;
            if (this.SlotSalida.PaqueteSalidaActual == this) {
                this.SlotSalida.LimpiarSlot();
            }
            this.startExpiration();
        }

        public void removeFromList() {
            if (this.SlotSalida.PaqueteSalidaActual == this) {
                this.SlotSalida.LimpiarSlot();
            }
            this.paquetesDeSalidaRef.Remove(this.indexdb);
        }
    }
}
