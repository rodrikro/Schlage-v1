namespace Administrador.Modulos.products {
    partial class crear_producto {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.Label5 = new System.Windows.Forms.Label();
            this.lbl_rect = new System.Windows.Forms.Label();
            this.lbl_linea = new System.Windows.Forms.Label();
            this.panelcont = new System.Windows.Forms.FlowLayoutPanel();
            this.cmb_fam = new System.Windows.Forms.ComboBox();
            this.familiasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.products = new Administrador.Datasets.products();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.cmb_acabado = new System.Windows.Forms.ComboBox();
            this.acabadosBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txt_descr = new System.Windows.Forms.TextBox();
            this.txt_oid = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.familiasTableAdapter = new Administrador.Datasets.productsTableAdapters.familiasTableAdapter();
            this.acabadosTableAdapter = new Administrador.Datasets.productsTableAdapters.acabadosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.familiasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.products)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acabadosBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(142, 65);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(112, 13);
            this.Label5.TabIndex = 28;
            this.Label5.Text = "Densidad de Corriente";
            // 
            // lbl_rect
            // 
            this.lbl_rect.AutoSize = true;
            this.lbl_rect.Location = new System.Drawing.Point(67, 65);
            this.lbl_rect.Name = "lbl_rect";
            this.lbl_rect.Size = new System.Drawing.Size(64, 13);
            this.lbl_rect.TabIndex = 27;
            this.lbl_rect.Text = "Rectificador";
            // 
            // lbl_linea
            // 
            this.lbl_linea.AutoSize = true;
            this.lbl_linea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_linea.Location = new System.Drawing.Point(24, 65);
            this.lbl_linea.Name = "lbl_linea";
            this.lbl_linea.Size = new System.Drawing.Size(38, 13);
            this.lbl_linea.TabIndex = 26;
            this.lbl_linea.Text = "Linea";
            // 
            // panelcont
            // 
            this.panelcont.AutoScroll = true;
            this.panelcont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelcont.Location = new System.Drawing.Point(16, 85);
            this.panelcont.Name = "panelcont";
            this.panelcont.Size = new System.Drawing.Size(260, 363);
            this.panelcont.TabIndex = 25;
            // 
            // cmb_fam
            // 
            this.cmb_fam.DataSource = this.familiasBindingSource1;
            this.cmb_fam.DisplayMember = "familia";
            this.cmb_fam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_fam.DropDownWidth = 200;
            this.cmb_fam.FormattingEnabled = true;
            this.cmb_fam.Location = new System.Drawing.Point(153, 35);
            this.cmb_fam.Name = "cmb_fam";
            this.cmb_fam.Size = new System.Drawing.Size(80, 21);
            this.cmb_fam.TabIndex = 24;
            this.cmb_fam.ValueMember = "familia";
            // 
            // familiasBindingSource1
            // 
            this.familiasBindingSource1.DataMember = "familias";
            this.familiasBindingSource1.DataSource = this.products;
            // 
            // products
            // 
            this.products.DataSetName = "products";
            this.products.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(26, 13);
            this.Label1.TabIndex = 17;
            this.Label1.Text = "OID";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(298, 210);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(170, 35);
            this.Button1.TabIndex = 29;
            this.Button1.Text = "Agregar";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cmb_acabado
            // 
            this.cmb_acabado.DataSource = this.acabadosBindingSource1;
            this.cmb_acabado.DisplayMember = "id";
            this.cmb_acabado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_acabado.FormattingEnabled = true;
            this.cmb_acabado.Location = new System.Drawing.Point(237, 35);
            this.cmb_acabado.Name = "cmb_acabado";
            this.cmb_acabado.Size = new System.Drawing.Size(77, 21);
            this.cmb_acabado.TabIndex = 23;
            this.cmb_acabado.ValueMember = "id";
            // 
            // acabadosBindingSource1
            // 
            this.acabadosBindingSource1.DataMember = "acabados";
            this.acabadosBindingSource1.DataSource = this.products;
            // 
            // txt_descr
            // 
            this.txt_descr.Location = new System.Drawing.Point(322, 36);
            this.txt_descr.MaxLength = 200;
            this.txt_descr.Name = "txt_descr";
            this.txt_descr.Size = new System.Drawing.Size(158, 20);
            this.txt_descr.TabIndex = 22;
            // 
            // txt_oid
            // 
            this.txt_oid.Location = new System.Drawing.Point(13, 36);
            this.txt_oid.Name = "txt_oid";
            this.txt_oid.Size = new System.Drawing.Size(136, 20);
            this.txt_oid.TabIndex = 21;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(322, 19);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(63, 13);
            this.Label4.TabIndex = 20;
            this.Label4.Text = "Descripción";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(237, 19);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(50, 13);
            this.Label3.TabIndex = 19;
            this.Label3.Text = "Acabado";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(159, 19);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(39, 13);
            this.Label2.TabIndex = 18;
            this.Label2.Text = "Familia";
            // 
            // familiasTableAdapter
            // 
            this.familiasTableAdapter.ClearBeforeFill = true;
            // 
            // acabadosTableAdapter
            // 
            this.acabadosTableAdapter.ClearBeforeFill = true;
            // 
            // crear_producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 466);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.lbl_rect);
            this.Controls.Add(this.lbl_linea);
            this.Controls.Add(this.panelcont);
            this.Controls.Add(this.cmb_fam);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.cmb_acabado);
            this.Controls.Add(this.txt_descr);
            this.Controls.Add(this.txt_oid);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Name = "crear_producto";
            this.Text = "Crear Producto";
            this.Load += new System.EventHandler(this.crear_producto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.familiasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.products)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acabadosBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label lbl_rect;
        internal System.Windows.Forms.Label lbl_linea;
        internal System.Windows.Forms.FlowLayoutPanel panelcont;
        internal System.Windows.Forms.ComboBox cmb_fam;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.ComboBox cmb_acabado;
        internal System.Windows.Forms.TextBox txt_descr;
        internal System.Windows.Forms.TextBox txt_oid;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        private Datasets.products products;
        private System.Windows.Forms.BindingSource familiasBindingSource1;
        private Datasets.productsTableAdapters.familiasTableAdapter familiasTableAdapter;
        private System.Windows.Forms.BindingSource acabadosBindingSource1;
        private Datasets.productsTableAdapters.acabadosTableAdapter acabadosTableAdapter;
    }
}