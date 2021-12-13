namespace Administrador.Modulos.reportes {
    partial class descripcion_producto {
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
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.txt_oid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(109, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Escribe el OID";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(84, 53);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(124, 23);
            this.Button1.TabIndex = 10;
            this.Button1.Text = "Siguiente Paso";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txt_oid
            // 
            this.txt_oid.Location = new System.Drawing.Point(59, 26);
            this.txt_oid.Name = "txt_oid";
            this.txt_oid.Size = new System.Drawing.Size(174, 20);
            this.txt_oid.TabIndex = 9;
            this.txt_oid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_oid_KeyDown);
            // 
            // descripcion_producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 82);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.txt_oid);
            this.Name = "descripcion_producto";
            this.Text = "Descripción de Producto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox txt_oid;
    }
}