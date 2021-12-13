using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.reportes {
    public partial class productos_por_familia : Form {
        public productos_por_familia() {
            InitializeComponent();
        }

        private void productos_por_familia_Load( object sender, EventArgs e ) {
            this.familiasTableAdapter.Fill(this.products.familias);
            crystalReportViewer1.Visible = false;
        }

        private void Button1_Click( object sender, EventArgs e ) {
            var familia = (string)cmb_fam.SelectedValue;
            var reporte = new Reportes.products_by_family();
            var dataSet = new Datasets.products();
            var dataAdapter = dataSet.Tables["product_description"];
            var tableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
            var dataByFamilia = tableAdapter.GetDataByFamilia(familia);
            foreach(Datasets.products.product_descriptionRow productDescription in dataByFamilia){
                dataAdapter.ImportRow(productDescription);
            }
            reporte.SetDataSource(dataSet);
            reporte.DataDefinition.FormulaFields["UnboundString1"].Text = "'Familia: " + familia + "'";
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Visible = true;
        }
    }
}
