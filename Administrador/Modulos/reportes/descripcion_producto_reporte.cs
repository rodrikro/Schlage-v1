using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administrador.Datasets.productsTableAdapters;
namespace Administrador.Modulos.reportes {
    public partial class descripcion_producto_reporte : Form {
        public descripcion_producto_reporte() {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load( object sender, EventArgs e ) {

        }

        public void CargarReporte( string oid ) {
            var reporte = new Reportes.product_description();
            var dataSet = new Datasets.products();

            var DA_RECT = dataSet.Tables["rectificadores"];
            var DA_CORR = dataSet.Tables["corrientes"];

            var TA = new product_descriptionTableAdapter();
            var DTS = TA.GetDataByOID(oid);
            var txt_descr = "";
            var txt_fam = "";
            var txt_acc = "";
            var txt_oid = "";

            foreach (Datasets.products.product_descriptionRow productDescription in DTS.Rows) {
                txt_descr = productDescription.descripcion;
                txt_acc = productDescription.acabado.ToString();
                txt_fam = productDescription.familia;
                txt_oid = productDescription.oid;
            }

            var TARECT = new rectificadoresTableAdapter();
            var DTSRECT = TARECT.GetData();
            foreach (Datasets.products.rectificadoresRow rectificador in DTSRECT.Rows) {
                DA_RECT.ImportRow(rectificador);
            }

            var TACORR = new corrientesTableAdapter();
            var DTSCORR = TACORR.GetDataByOID(oid);
            foreach (Datasets.products.corrientesRow corriente in DTSCORR.Rows) {
                DA_CORR.ImportRow(corriente);
            }

            reporte.DataDefinition.FormulaFields["UnboundString1"].Text = "'OID: " + txt_oid + "'";
            reporte.DataDefinition.FormulaFields["UnboundString2"].Text = "'Descripción: " + txt_descr + "'";
            reporte.DataDefinition.FormulaFields["UnboundString3"].Text = "'Acabado: " + txt_acc + "'";
            reporte.DataDefinition.FormulaFields["UnboundString4"].Text = "'Familia: " + txt_fam + "'";

            reporte.SetDataSource(dataSet);
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Visible = true;
        }
    }
}
