using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public class lineaEntrada {
        public Dictionary<string, services.Familia> familias;
        public List<services.Rectificadores> rectificadores;
        public Dictionary<int, services.Rectificadores> rectificadoresDictionary;
        public config.configuration configuration;
        public services.DataDB dataDB;
        public services.plc.PlcLineaEntrada plcEntrada;
        public bool canWrite;
        public bool inCycle;
    }
}
