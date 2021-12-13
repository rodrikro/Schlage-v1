using System;
using System.Collections.Generic;
using System.Text;

namespace Lineas.shared {
    public class VerticalFlowPanel : System.Windows.Forms.FlowLayoutPanel {
        protected override System.Windows.Forms.CreateParams CreateParams {
            get {
                var cp = base.CreateParams;
                cp.Style |= 0x00200000; // WS_VSCROLL
                return cp;
            }
        }
    }
}
