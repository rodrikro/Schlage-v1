using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public class SlotSalida {
        public services.plc.PlcLineaSalida lineaSalida;
        public int Position = -1;
        public int Type = 0; //0 is dataconcat, 1 is pullout
        public paqueteSalidaPlc PaqueteSalidaActual = null;

        public paqueteSalidaPlc ParseData() {
            try {
                if (this.Position > -1 && this.Type == 0) {
                    var data = this.lineaSalida.dataconcats[this.Position].getData();
                    var paqueteSalida = new shared.paqueteSalidaPlc(data);
                    if (paqueteSalida.hasData && paqueteSalida.indexdb == 0 && !paqueteSalida.isBarraText) {
                        //No tenemos un dato valido por lo tanto lo borramos
                        this.LimpiarSlot();
                        return null;
                    }
                    return paqueteSalida;
                } else if (this.Position > -1 && this.Type == 1) {
                    var data = this.lineaSalida.pullouts[this.Position].getData();
                    var paqueteSalida = new shared.paqueteSalidaPlc(data);
                    if (paqueteSalida.hasData && paqueteSalida.indexdb == 0) {
                        //No tenemos un dato valido por lo tanto lo borramos
                        this.LimpiarSlot();
                        return null;
                    }
                    return paqueteSalida;
                }
            } catch (Exception ex) {
                this.LimpiarSlot();
                throw ex;
            }
            this.PaqueteSalidaActual = null;
            return null;
        }

        public void LimpiarSlot(){
            this.PaqueteSalidaActual = null;
            if (this.Position > -1 && this.Type == 0) {
                this.lineaSalida.dataconcats[this.Position].clear();
            } else if (this.Position > -1 && this.Type == 1) {
                this.lineaSalida.pullouts[this.Position].clear();
            }
        }
    }
}
