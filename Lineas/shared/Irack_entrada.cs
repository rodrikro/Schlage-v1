using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public delegate void RackEntradaViewContentEventHandler( Irack_entrada rackView, RackEntradaViewContentEventArgs e );
    public class RackEntradaViewContentEventArgs : EventArgs { 
        private string EventInfo;
        public RackEntradaViewContentEventArgs( string Text )
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }
    public interface Irack_entrada {
        void setInUse( string rack, string piezas, string job );
        void clearInUse();
        void setInUseBarra();
        shared.workrack getWorkrack();
        event RackEntradaViewContentEventHandler ViewIntention;
    }
}
