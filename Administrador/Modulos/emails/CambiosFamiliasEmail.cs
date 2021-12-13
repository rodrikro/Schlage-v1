using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administrador.Modulos.emails {
    class CambioFamilia {
        public string familiaAnterior = "";
        public string familiaNueva = "";
        public string areaAnterior = "";
        public string areaNueva = "";
    }

    class CambiosFamiliasEmail : EmailInterface {
        public string user = "";
        public string razon = "";
        public List<CambioFamilia> cambios = new List<CambioFamilia>();
        public string getSubject() {
            return "[ Sistema Schlage ] Cambios en familias detectado";
        }

        public string getBody() {
            var body = "Se detectaron cambios en el sistema de familias por el usuario " + this.user + "<br>\n";
            body += "Razón del cambio: " + this.razon + "<br>\n<br>\n";
            body += "Los cambios son:<br>\n<br>\n";
            body += "Familia Anterior - Familia nueva - Area Anterior - Area Nueva<br>\n";
            foreach( CambioFamilia cambio in cambios){
                body += cambio.familiaAnterior.PadRight(10, ' ') + " - " + cambio.familiaNueva.PadRight(10, ' ') + " - " + cambio.areaAnterior.PadRight(10, ' ') + " - " + cambio.areaNueva.PadRight(10, ' ') + "<br>\n";
            }
            return body;
        }
    }
}

/**
 * Ejemplo de correo:
 * 
 * Asunto: [ Sistema Schlage ] Cambios en familias detectado
 * 
 * Se detectaron cambios en el sistema de familias por el usuario {user}.
 * Los cambios son:
 * 
 * Familia Anterior - Familia nueva - Area Anterior - Area Nueva
 * XXXXXXX          - XXXXXXX       - XXXXXXXX      - XXXXXXXX
 * 
**/
