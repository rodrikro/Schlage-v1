using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administrador.Modulos.emails;

namespace Administrador.Modulos {
    public partial class administrar_familias : Form {
        public administrar_familias() {
            InitializeComponent();
        }

        private void administrar_familias_Load( object sender, EventArgs e ) {
            this.familiasTableAdapter.Fill(this.products.familias);

        }

        private void ToolStripButton2_Click( object sender, EventArgs e ) {



            //Rellenar tabla
            this.familiasTableAdapter.Fill(this.products.familias);
        }

        private void DataGridView1_DataError( object sender, DataGridViewDataErrorEventArgs e ) {
            MessageBox.Show("Error de definición: Verifique la Familia no se repita y que el área sea un número decimal y que sea el área en pulgadas cuadradas.");
            if(e.Context == DataGridViewDataErrorContexts.Commit){
                MessageBox.Show("Commit error");
            } else if(e.Context == DataGridViewDataErrorContexts.CurrentCellChange){
                MessageBox.Show("Cell change");
            } else if(e.Context == DataGridViewDataErrorContexts.Parsing){
                MessageBox.Show("Error de definición: Verifique la Familia no se repita y que el área sea un número decimal y que sea el área en pulgadas cuadradas.");
            } else if(e.Context == DataGridViewDataErrorContexts.LeaveControl){
                MessageBox.Show("leave control error");
            }
            if ((e.Exception) is ConstraintException) {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";
                e.ThrowException = false;
            }
        }

        private void ToolStripButton1_Click( object sender, EventArgs e ) {
            var razonCambioResponse = razon_cambio.MostrarDialogo();
            if (razonCambioResponse.Accepted == false) {
                return;
            }
            try {
            
                //Deteccion de cambios
                var dataSet = this.products.familias.DataSet;
                var hasChanges = dataSet.HasChanges();
                if (hasChanges) {
                    var email = new CambiosFamiliasEmail();
                    email.razon = razonCambioResponse.Razon;
                    var familiasHistorialTableAdapter = new Datasets.productsTableAdapters.familias_historialTableAdapter();
                    var inicioWindow = (Inicio)this.MdiParent;

                    email.user = inicioWindow.currentUser;
                    var tiempoCambio = DateTime.Now;

                    var changes = dataSet.GetChanges();

                    foreach (DataTable changesTable in changes.Tables) {
                        foreach (DataRow changedRow in changesTable.Rows) {
                            if (!changedRow.HasVersion(DataRowVersion.Original)
                                || !changedRow.HasVersion(DataRowVersion.Current)
                                || changedRow["familia", DataRowVersion.Original] != changedRow["familia", DataRowVersion.Current]
                                || changedRow["areainsq", DataRowVersion.Original] != changedRow["areainsq", DataRowVersion.Current]
                            ) {

                                var familiaAnterior = "";
                                var familiaNueva = "";
                                double areaAnterior = 0;
                                double areaNueva = 0;

                                if (changedRow.HasVersion(DataRowVersion.Original)) {
                                    familiaAnterior = (string)changedRow["familia", DataRowVersion.Original];
                                    areaAnterior = (double)changedRow["areainsq", DataRowVersion.Original];
                                }
                                if (changedRow.HasVersion(DataRowVersion.Current)) {
                                    familiaNueva = (string)changedRow["familia", DataRowVersion.Current];
                                    areaNueva = (double)changedRow["areainsq", DataRowVersion.Current];
                                }

                                //Almacenar en tabla
                                familiasHistorialTableAdapter.Insert(familiaAnterior, familiaNueva, areaAnterior, areaNueva, inicioWindow.currentUser, tiempoCambio, razonCambioResponse.Razon);

                                //Agregar cambio al email
                                var cambio = new CambioFamilia();
                                cambio.familiaAnterior = familiaAnterior;
                                cambio.familiaNueva = familiaNueva;
                                cambio.areaAnterior = areaAnterior.ToString();
                                cambio.areaNueva = areaNueva.ToString();
                                email.cambios.Add(cambio);

                            }
                        }
                    }

                    //Actualizar en DB original
                    this.familiasTableAdapter.Update(this.products.familias);

                    //Enviar Email con los cambios
                    inicioWindow.emailServer.SendEmail(email);
                }
                
                MessageBox.Show("Guardado Satisfactoriamente");
            } catch(Exception ex) {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

    }
}
