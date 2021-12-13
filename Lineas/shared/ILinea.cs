using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    interface ILinea {
        void ConfigurarLinea( ref config.configuration config, ref services.DataDB dataDB, ref services.Opc opcServer );
    }
}
