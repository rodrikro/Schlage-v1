using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administrador.Modulos.emails {
    public interface EmailInterface {
        string getSubject();
        string getBody();
    }
}
