using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.reportes {
    public partial class productos_calidad : Form {
        public productos_calidad() {
            InitializeComponent();
        }

        private void reporte_productos_calidad_Load( object sender, EventArgs e ) {
            var reporte = new Reportes.product_calidad();
            var dataSet = new Datasets.products();

            var dataAdapterRectificadores = dataSet.Tables["rectificadores"];
            var dataAdapterCorrientes = dataSet.Tables["corrientes"];
            var dataAdapterDescripciones = dataSet.Tables["product_description"];

                        
            var rectificadorTableAdapter = new Datasets.productsTableAdapters.rectificadoresTableAdapter();
            var dataRectificadores = rectificadorTableAdapter.GetData();
            foreach(Datasets.products.rectificadoresRow tmpData in dataRectificadores){
                dataAdapterRectificadores.ImportRow(tmpData);
            }

            var corrientesTableAdapter = new Datasets.productsTableAdapters.corrientesTableAdapter();
            var dataCorrientes = corrientesTableAdapter.GetData();
            foreach(Datasets.products.corrientesRow tmpData in dataCorrientes){
                dataAdapterCorrientes.ImportRow(tmpData);
            }

            var productsTableAdapter = new Datasets.productsTableAdapters.product_descriptionTableAdapter();
            var dataProducts = productsTableAdapter.GetData();
            foreach(Datasets.products.product_descriptionRow tmpData in dataProducts){
                dataAdapterDescripciones.ImportRow(tmpData);
            }

            reporte.SetDataSource(dataSet);
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Visible = true;
        }
    }
}
