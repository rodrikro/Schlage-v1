using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Administrador.Modulos.emails;

namespace Administrador.Modulos.products {
    public partial class crear_producto : Form {

        private Hashtable rectificadoresControls = new Hashtable();

        public crear_producto() {
            InitializeComponent();
        }

        private void crear_producto_Load( object sender, EventArgs e ) {
            this.acabadosTableAdapter.Fill(this.products.acabados);
            this.familiasTableAdapter.Fill(this.products.familias);
            //Para cada elemento de la colección de rectificadores creamos un panel para corrientes
            var rectificadoresTableAdapter = new Datasets.productsTableAdapters.rectificadoresTableAdapter();
            var rectficadoresDataTable = rectificadoresTableAdapter.GetData();
            var counter = 0;
            foreach( Datasets.products.rectificadoresRow rectificador in rectficadoresDataTable.Rows){
                var tmpRectificadorControl = new control_linea_rect_value();

                tmpRectificadorControl.setID(rectificador.id);
                tmpRectificadorControl.setLinea(rectificador.linea.ToString());
                tmpRectificadorControl.setRect(rectificador.rectificador);
                tmpRectificadorControl.setCorriente(0);

                panelcont.Controls.Add(tmpRectificadorControl);
                rectificadoresControls.Add(counter, tmpRectificadorControl);
                counter = counter + 1;
            }
        }

        private void Button1_Click( object sender, EventArgs e ) {
            var tex_oid = txt_oid.Text;
            var tex_fam = (string)cmb_fam.SelectedValue;
            var tex_acc = (int)cmb_acabado.SelectedValue;
            var tex_desc = txt_descr.Text;
            try {
                var productDescriptionTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
                var corrientesTableAdapter = new Datasets.productsTableAdapters.corrientesTableAdapter();
                var corrientesHistorialTableAdapter = new Datasets.productsTableAdapters.corrientes_historialTableAdapter();

                var inicioWindow = (Inicio)this.MdiParent;
                var email = new CambiosCorrientesEmail();
                email.user = inicioWindow.currentUser;
                email.producto = tex_oid;
                var razon = "Crear Producto";
                email.razon = razon;
                var tiempoCambio = DateTime.Now;

                productDescriptionTableAdapter.Insert(tex_oid, tex_desc, tex_fam, "0", tex_acc, "0");
                foreach(DictionaryEntry  rectificador in this.rectificadoresControls){
                    var rectificadorControl = (control_linea_rect_value)rectificador.Value;
                    corrientesTableAdapter.Insert(tex_oid, rectificadorControl.getID(), rectificadorControl.getCorriente());

                    //Almacenar en la tabla de hisitorial
                    corrientesHistorialTableAdapter.Insert(rectificadorControl.getID(), tex_oid, rectificadorControl.getID(), null, rectificadorControl.getCorriente(), inicioWindow.currentUser, tiempoCambio, razon);

                    //Agregar cambio al email
                    var cambio = new CambioCorriente();
                    cambio.id = rectificadorControl.getID().ToString();
                    cambio.rectificador = rectificadorControl.getLinea() + "." + rectificadorControl.getRect();
                    cambio.corrienteAnterior = "N/A";
                    cambio.corrienteNueva = rectificadorControl.getCorriente().ToString();
                    email.cambios.Add(cambio);
                }

                //Notificar email
                inicioWindow.emailServer.SendEmail(email);

                MessageBox.Show("Agregado Correctamente");

                var crearProductoWin = new Modulos.products.crear_producto();
                crearProductoWin.MdiParent = this.MdiParent;
                crearProductoWin.Show();

                this.Close();
            } catch(Exception ex){
                //Checar que no está duplicado
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
