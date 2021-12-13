using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos {
    public partial class razon_cambio : Form {
        public static RazonCambioResponse MostrarDialogo() {
            var respuesta = new RazonCambioResponse();
            var currentDialog = new razon_cambio();
            currentDialog.btnGuardar.Click += ( sender, e ) => { 
                respuesta.Accepted = true;
                respuesta.Razon = currentDialog.txtRazon.Text;
                currentDialog.Close();
            };
            currentDialog.btnCancelar.Click += ( sender, e ) => {
                respuesta.Accepted = false;
                respuesta.Razon = "";
                currentDialog.Close();
            };
            currentDialog.ShowDialog();
            return respuesta;
        }
        public razon_cambio() {
            InitializeComponent();
        }
    }
    public class RazonCambioResponse {
        public bool Accepted;
        public string Razon;
    }
}
