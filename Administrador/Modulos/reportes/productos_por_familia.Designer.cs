namespace Administrador.Modulos.reportes {
    partial class productos_por_familia {
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
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Button1 = new System.Windows.Forms.Button();
            this.cmb_fam = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.products = new Administrador.Datasets.products();
            this.familiasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.familiasTableAdapter = new Administrador.Datasets.productsTableAdapters.familiasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.products)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.familiasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.Button1);
            this.SplitContainer1.Panel1.Controls.Add(this.cmb_fam);
            this.SplitContainer1.Panel1.Controls.Add(this.Label1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.crystalReportViewer1);
            this.SplitContainer1.Size = new System.Drawing.Size(692, 366);
            this.SplitContainer1.SplitterDistance = 35;
            this.SplitContainer1.TabIndex = 2;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(387, 9);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(106, 23);
            this.Button1.TabIndex = 2;
            this.Button1.Text = "Ver Productos";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cmb_fam
            // 
            this.cmb_fam.DataSource = this.familiasBindingSource;
            this.cmb_fam.DisplayMember = "familia";
            this.cmb_fam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_fam.FormattingEnabled = true;
            this.cmb_fam.Location = new System.Drawing.Point(251, 11);
            this.cmb_fam.Name = "cmb_fam";
            this.cmb_fam.Size = new System.Drawing.Size(121, 21);
            this.cmb_fam.TabIndex = 1;
            this.cmb_fam.ValueMember = "familia";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(206, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(39, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Familia";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(692, 327);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // products
            // 
            this.products.DataSetName = "products";
            this.products.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // familiasBindingSource
            // 
            this.familiasBindingSource.DataMember = "familias";
            this.familiasBindingSource.DataSource = this.products;
            // 
            // familiasTableAdapter
            // 
            this.familiasTableAdapter.ClearBeforeFill = true;
            // 
            // productos_por_familia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 366);
            this.Controls.Add(this.SplitContainer1);
            this.Name = "productos_por_familia";
            this.Text = "Productos por Familia";
            this.Load += new System.EventHandler(this.productos_por_familia_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel1.PerformLayout();
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.products)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.familiasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.ComboBox cmb_fam;
        internal System.Windows.Forms.Label Label1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private Datasets.products products;
        private System.Windows.Forms.BindingSource familiasBindingSource;
        private Datasets.productsTableAdapters.familiasTableAdapter familiasTableAdapter;

    }
}