namespace Lineas.shared {
    partial class entrada_agregar_rack_corriente {
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
            this.lbl_tot = new System.Windows.Forms.Label();
            this.lbl_corr = new System.Windows.Forms.Label();
            this.lbl_rect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_tot
            // 
            this.lbl_tot.BackColor = System.Drawing.Color.Transparent;
            this.lbl_tot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tot.Location = new System.Drawing.Point(62, 4);
            this.lbl_tot.Name = "lbl_tot";
            this.lbl_tot.Size = new System.Drawing.Size(50, 15);
            this.lbl_tot.TabIndex = 9;
            this.lbl_tot.Text = "10";
            this.lbl_tot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_corr
            // 
            this.lbl_corr.BackColor = System.Drawing.Color.Transparent;
            this.lbl_corr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_corr.Location = new System.Drawing.Point(42, 4);
            this.lbl_corr.Name = "lbl_corr";
            this.lbl_corr.Size = new System.Drawing.Size(28, 15);
            this.lbl_corr.TabIndex = 8;
            this.lbl_corr.Text = "5";
            this.lbl_corr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_rect
            // 
            this.lbl_rect.AutoSize = true;
            this.lbl_rect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rect.Location = new System.Drawing.Point(10, 4);
            this.lbl_rect.Name = "lbl_rect";
            this.lbl_rect.Size = new System.Drawing.Size(31, 15);
            this.lbl_rect.TabIndex = 7;
            this.lbl_rect.Text = "RID";
            // 
            // entrada_agregar_rack_corriente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_tot);
            this.Controls.Add(this.lbl_corr);
            this.Controls.Add(this.lbl_rect);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "entrada_agregar_rack_corriente";
            this.Size = new System.Drawing.Size(110, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_rect;
        public System.Windows.Forms.Label lbl_corr;
        public System.Windows.Forms.Label lbl_tot;
    }
}
