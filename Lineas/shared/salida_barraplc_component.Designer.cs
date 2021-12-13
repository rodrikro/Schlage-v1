namespace Lineas.shared {
    partial class salida_barraplc_component {
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
            this.group = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.lbl_job = new System.Windows.Forms.Label();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // group
            // 
            this.group.Controls.Add(this.Label2);
            this.group.Controls.Add(this.Button1);
            this.group.Controls.Add(this.lbl_job);
            this.group.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group.Location = new System.Drawing.Point(3, 0);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(713, 100);
            this.group.TabIndex = 1;
            this.group.TabStop = false;
            this.group.Text = "Barra";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(78, 45);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(41, 18);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Job:";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(563, 33);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(144, 42);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Descargar";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // lbl_job
            // 
            this.lbl_job.AutoSize = true;
            this.lbl_job.Location = new System.Drawing.Point(125, 45);
            this.lbl_job.Name = "lbl_job";
            this.lbl_job.Size = new System.Drawing.Size(123, 18);
            this.lbl_job.TabIndex = 0;
            this.lbl_job.Text = "BARRA de PLC";
            // 
            // salida_barraplc_component
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.group);
            this.Name = "salida_barraplc_component";
            this.Size = new System.Drawing.Size(724, 107);
            this.Load += new System.EventHandler(this.salida_element_component_Load);
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox group;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label lbl_job;
    }
}
