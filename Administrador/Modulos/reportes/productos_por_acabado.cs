using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.reportes {
    public partial class productos_por_acabado : Form {
        public productos_por_acabado() {
            InitializeComponent();
        }

        private void productos_por_acabado_Load( object sender, EventArgs e ) {
            this.acabadosTableAdapter.Fill(this.products.acabados);
            crystalReportViewer1.Visible = false;

        }

        private void Button1_Click( object sender, EventArgs e ) {
            var acabado = (int)cmb_acc.SelectedValue;
            var reporte = new Reportes.products_by_acc();
            var dataSet = new Datasets.products();
            var dataAdapter = dataSet.Tables["product_description"];
            var tableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
            var dataByFamilia = tableAdapter.GetDataByAcabado(acabado);
            foreach (Datasets.products.product_descriptionRow productDescription in dataByFamilia) {
                dataAdapter.ImportRow(productDescription);
            }
            reporte.SetDataSource(dataSet);
            reporte.DataDefinition.FormulaFields["UnboundString1"].Text = "'Acabado: " + acabado + "'";
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Visible = true;
        }
    }
}
