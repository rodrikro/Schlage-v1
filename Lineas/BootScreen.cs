using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lineas {
    public partial class BootScreen : Form {
        public BootScreen() {
            InitializeComponent();
        }

        private void BootScreen_Load( object sender, EventArgs e ) {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
    }
}
