namespace Administrador.Modulos.products {
    partial class control_linea_rect_value {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txt_corr = new System.Windows.Forms.TextBox();
            this.lbl_rect = new System.Windows.Forms.Label();
            this.lbl_linea = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_corr
            // 
            this.txt_corr.Location = new System.Drawing.Point(116, 4);
            this.txt_corr.Name = "txt_corr";
            this.txt_corr.Size = new System.Drawing.Size(101, 20);
            this.txt_corr.TabIndex = 6;
            this.txt_corr.Leave += new System.EventHandler(this.txt_corr_Leave);
            // 
            // lbl_rect
            // 
            this.lbl_rect.AutoSize = true;
            this.lbl_rect.Location = new System.Drawing.Point(59, 7);
            this.lbl_rect.Name = "lbl_rect";
            this.lbl_rect.Size = new System.Drawing.Size(39, 13);
            this.lbl_rect.TabIndex = 5;
            this.lbl_rect.Text = "Label2";
            // 
            // lbl_linea
            // 
            this.lbl_linea.AutoSize = true;
            this.lbl_linea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_linea.Location = new System.Drawing.Point(5, 7);
            this.lbl_linea.Name = "lbl_linea";
            this.lbl_linea.Size = new System.Drawing.Size(38, 13);
            this.lbl_linea.TabIndex = 4;
            this.lbl_linea.Text = "Linea";
            // 
            // control_linea_rect_value
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_corr);
            this.Controls.Add(this.lbl_rect);
            this.Controls.Add(this.lbl_linea);
            this.Name = "control_linea_rect_value";
            this.Size = new System.Drawing.Size(223, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txt_corr;
        internal System.Windows.Forms.Label lbl_rect;
        internal System.Windows.Forms.Label lbl_linea;

    }
}
