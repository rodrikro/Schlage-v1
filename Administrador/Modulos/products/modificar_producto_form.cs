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
    public partial class modificar_producto_form : Form {

        private Hashtable rectificadoresControls = new Hashtable();

        public modificar_producto_form() {
            InitializeComponent();
        }

        private void modificar_producto_form_Load( object sender, EventArgs e ) {
            this.acabadosTableAdapter.Fill(this.products.acabados);
            this.familiasTableAdapter.Fill(this.products.familias);

            //Traemos el campo del OID del product_descr y seteamos valores
            var productDescriptionTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
            var productDescriptionDataTable = productDescriptionTableAdapter.GetDataByOID(txt_oid.Text);
            foreach(Datasets.products.product_descriptionRow productDescription in productDescriptionDataTable.Rows){
                cmb_acabado.SelectedValue = productDescription.acabado;
                cmb_fam.SelectedValue = productDescription.familia;
                txt_descr.Text = productDescription.descripcion;
            }

            //Traer corrientes (CorrientesByOID)
            var corrientesRect = new Hashtable();
            var corrientesTableAdapter = new Datasets.productsTableAdapters.corrientesTableAdapter();
            var corrientesDataTable = corrientesTableAdapter.GetDataByOID(txt_oid.Text);
            foreach(Datasets.products.corrientesRow corrienteRow in corrientesDataTable.Rows){
                corrientesRect.Add(corrienteRow.rectificador, corrienteRow.corriente);
            }
            //Para cada elemento de la colección de rectificadores creamos un panel para corrientes
            var rectificadoresTableAdapter = new Datasets.productsTableAdapters.rectificadoresTableAdapter();
            var rectficadoresDataTable = rectificadoresTableAdapter.GetData();
            var counter = 0;
            foreach( Datasets.products.rectificadoresRow rectificador in rectficadoresDataTable.Rows){
                var tmpRectificadorControl = new control_linea_rect_value();

                tmpRectificadorControl.setID(rectificador.id);
                tmpRectificadorControl.setLinea(rectificador.linea.ToString());
                tmpRectificadorControl.setRect(rectificador.rectificador);
                if(corrientesRect.Contains(rectificador.id)){
                    tmpRectificadorControl.setCorriente((double)corrientesRect[rectificador.id]);
                } else {
                    tmpRectificadorControl.setCorriente(0);
                    corrientesTableAdapter.Insert(txt_oid.Text, rectificador.id, 0);
                }
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

            var razonCambioResponse = razon_cambio.MostrarDialogo();
            if (razonCambioResponse.Accepted == false) {
                return;
            }

            try {
                //Actualizar producto
                var productDescriptionTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
                var corrientesTableAdapter = new Datasets.productsTableAdapters.corrientesTableAdapter();
                var corrientesHistorialTableAdapter = new Datasets.productsTableAdapters.corrientes_historialTableAdapter();
                var inicioWindow = (Inicio)this.MdiParent;
                productDescriptionTableAdapter.UpdateQueryByOid(tex_desc, tex_fam, tex_acc, tex_oid);

                bool hayCambios = false;
                var email = new CambiosCorrientesEmail();
                email.user = inicioWindow.currentUser;
                email.producto = tex_oid;
                email.razon = razonCambioResponse.Razon;
                var tiempoCambio = DateTime.Now;

                foreach(DictionaryEntry  rectificador in this.rectificadoresControls){
                    var rectificadorControl = (control_linea_rect_value)rectificador.Value;
                    if (rectificadorControl.hasChanged()) {

                        //Actualizar tabla de corrientes
                        corrientesTableAdapter.UpdateCorrienteByOIDANDRECT(rectificadorControl.getCorriente(), tex_oid, rectificadorControl.getID());

                        //Almacenar en la tabla de hisitorial
                        corrientesHistorialTableAdapter.Insert(rectificadorControl.getID(), tex_oid, rectificadorControl.getID(), rectificadorControl.getValorAnterior(), rectificadorControl.getCorriente(), inicioWindow.currentUser, tiempoCambio, razonCambioResponse.Razon);

                        //Agregar cambio al email
                        var cambio = new CambioCorriente();
                        cambio.id = rectificadorControl.getID().ToString();
                        cambio.rectificador = rectificadorControl.getLinea() + "." + rectificadorControl.getRect();
                        cambio.corrienteAnterior = rectificadorControl.getValorAnterior().ToString();
                        cambio.corrienteNueva = rectificadorControl.getCorriente().ToString();
                        email.cambios.Add(cambio);

                        hayCambios = true;
                        
                        //Actualizamos referencias por si se actualiza nuevamente
                        rectificadorControl.setCorriente(rectificadorControl.getCorriente());
                    }
                }

                //Notificar email si hay cambios
                if (hayCambios) {
                    inicioWindow.emailServer.SendEmail(email);
                }

                MessageBox.Show("Modificado correctamente");
            } catch(Exception ex){
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button2_Click( object sender, EventArgs e ) {
            var button = MessageBox.Show("¿Desea eliminar el producto?", "Confirmación", MessageBoxButtons.YesNo);
            if (button == System.Windows.Forms.DialogResult.No) {
                return;
            }
            var razonCambioResponse = razon_cambio.MostrarDialogo();
            if (razonCambioResponse.Accepted == false) {
                return;
            }
            try {
                
                var productDescriptionTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
                var corrientesTableAdapter = new Datasets.productsTableAdapters.corrientesTableAdapter();
                var corrientesHistorialTableAdapter = new Datasets.productsTableAdapters.corrientes_historialTableAdapter();
                var inicioWindow = (Inicio)this.MdiParent;
                var tex_oid = txt_oid.Text;

                //Agregar a historial
                bool hayCambios = false;
                var email = new CambiosCorrientesEmail();
                email.user = inicioWindow.currentUser;
                email.producto = tex_oid;
                email.razon = razonCambioResponse.Razon;
                var tiempoCambio = DateTime.Now;

                foreach (DictionaryEntry rectificador in this.rectificadoresControls) {
                    var rectificadorControl = (control_linea_rect_value)rectificador.Value;

                    //Almacenar en la tabla de hisitorial
                    corrientesHistorialTableAdapter.Insert(rectificadorControl.getID(), tex_oid, rectificadorControl.getID(), rectificadorControl.getValorAnterior(), null, inicioWindow.currentUser, tiempoCambio, razonCambioResponse.Razon);

                    //Agregar cambio al email
                    var cambio = new CambioCorriente();
                    cambio.id = rectificadorControl.getID().ToString();
                    cambio.rectificador = rectificadorControl.getLinea() + "." + rectificadorControl.getRect();
                    cambio.corrienteAnterior = rectificadorControl.getValorAnterior().ToString();
                    cambio.corrienteNueva = "N/A";
                    email.cambios.Add(cambio);
                }

                //Notificar email
                inicioWindow.emailServer.SendEmail(email);

                //Borrar producto
                corrientesTableAdapter.DeleteQueryByOid(txt_oid.Text);
                productDescriptionTableAdapter.DeleteQueryByOID(txt_oid.Text);
                MessageBox.Show("Eliminado correctamente");
                this.Close();
            } catch(Exception ex){
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
