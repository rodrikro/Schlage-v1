using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Administrador.Modulos.products {
    public partial class control_linea_rect_value : UserControl {
        public control_linea_rect_value() {
            InitializeComponent();
        }
        private int id = 0;
        private double valorAnterior = 0;
        private bool valorAnteriorSet = false;

        private void txt_corr_Leave(object sender, EventArgs e) {
            try {
                 double corriente = Double.Parse(txt_corr.Text);
            }   catch (Exception ex){
                MessageBox.Show("Verifique que sea un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_corr.Focus();
            }
        }

        public void setLinea(string texto){
            lbl_linea.Text = texto;
        }
        public void setRect(string texto){
            lbl_rect.Text = texto;
        }
        public string getRect() {
            return lbl_rect.Text;
        }
        public string getLinea() {
            return lbl_linea.Text;
        }
        public void setID(int valor){
            this.id = valor;
        }
        public void setCorriente(double valor){
            this.valorAnterior = valor;
            this.valorAnteriorSet = true;
            txt_corr.Text = valor.ToString();
        }
        public int getID(){
            return this.id;
        }
        public double getCorriente(){
            return Double.Parse(txt_corr.Text);
        }
        public double getValorAnterior() {
            return this.valorAnterior;
        }
        public bool hasChanged() {
            if (!this.valorAnteriorSet && this.getCorriente() != 0) {
                return true;
            }
            if(this.valorAnteriorSet && this.getCorriente() != this.valorAnterior){
                return true;
            }
            return false;
        }
    }
}
