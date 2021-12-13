using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administrador.Modulos.emails {
    class CambioCorriente {
        public string id = "";
        public string rectificador = "";
        public string corrienteAnterior = "";
        public string corrienteNueva = "";
    }
    class CambiosCorrientesEmail : EmailInterface {

        public string user = "";
        public string producto = "";
        public string razon = "";
        public List<CambioCorriente> cambios = new List<CambioCorriente>();

        public string getSubject() {
            return "[ Sistema Schlage ] Cambios en corriente detectado";
        }

        public string getBody() {
            var body = "Se detectaron cambios en las corrientes del producto OID " + this.producto + " por el usuario " + this.user + "<br>\n";
            body += "Razón del cambio: " + this.razon + "<br>\n<br>\n";
            body += "Los cambios son:<br>\n<br>\n";
            body += "ID - Rectificador - Valor Anterior - Valor Nuevo<br>\n";
            foreach (CambioCorriente cambio in cambios) {
                body += cambio.id.PadRight(6, ' ') + " - " + cambio.rectificador.PadRight(6, ' ') + " - " + cambio.corrienteAnterior.PadRight(12, ' ') + " - " + cambio.corrienteNueva.PadRight(12, ' ') + "<br>\n";
            }
            return body;
        }
    }
}

/**
 * Ejemplo de correo:
 * 
 * Asunto: [ Sistema Schlage ] Cambios en corriente detectado
 * 
 * Se detectaron cambios en las corrientes del producto OID { producto } por el usuario { user }
 * Los cambios son:
 * 
 * ID - Rectificador - Valor Anterior - Valor Nuevo
 * XXXXX - XXXXX     - XXXXXXXX       - XXXXXXXX
 * 
**/