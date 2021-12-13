namespace Lineas.shared {
    partial class salida_element_component {
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
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lbl_descripcion = new System.Windows.Forms.Label();
            this.lbl_oid = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txt_nps = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.lbl_job = new System.Windows.Forms.Label();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // group
            // 
            this.group.Controls.Add(this.Label4);
            this.group.Controls.Add(this.Label3);
            this.group.Controls.Add(this.Label2);
            this.group.Controls.Add(this.lbl_descripcion);
            this.group.Controls.Add(this.lbl_oid);
            this.group.Controls.Add(this.Label1);
            this.group.Controls.Add(this.txt_nps);
            this.group.Controls.Add(this.Button1);
            this.group.Controls.Add(this.lbl_job);
            this.group.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group.Location = new System.Drawing.Point(3, 0);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(713, 100);
            this.group.TabIndex = 1;
            this.group.TabStop = false;
            this.group.Text = "GroupBox1";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(20, 75);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(103, 18);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "Descripción:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(81, 49);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(42, 18);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "OID:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(82, 23);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(41, 18);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "Job:";
            // 
            // lbl_descripcion
            // 
            this.lbl_descripcion.AutoSize = true;
            this.lbl_descripcion.Location = new System.Drawing.Point(129, 75);
            this.lbl_descripcion.Name = "lbl_descripcion";
            this.lbl_descripcion.Size = new System.Drawing.Size(57, 18);
            this.lbl_descripcion.TabIndex = 5;
            this.lbl_descripcion.Text = "Label1";
            // 
            // lbl_oid
            // 
            this.lbl_oid.AutoSize = true;
            this.lbl_oid.Location = new System.Drawing.Point(129, 49);
            this.lbl_oid.Name = "lbl_oid";
            this.lbl_oid.Size = new System.Drawing.Size(57, 18);
            this.lbl_oid.TabIndex = 4;
            this.lbl_oid.Text = "Label1";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(432, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 18);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Piezas Salida";
            // 
            // txt_nps
            // 
            this.txt_nps.Location = new System.Drawing.Point(462, 51);
            this.txt_nps.MaxLength = 3;
            this.txt_nps.Name = "txt_nps";
            this.txt_nps.Size = new System.Drawing.Size(50, 24);
            this.txt_nps.TabIndex = 2;
            this.txt_nps.Text = "MMM";
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
            this.lbl_job.Location = new System.Drawing.Point(129, 23);
            this.lbl_job.Name = "lbl_job";
            this.lbl_job.Size = new System.Drawing.Size(57, 18);
            this.lbl_job.TabIndex = 0;
            this.lbl_job.Text = "Label1";
            // 
            // salida_element_component
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.group);
            this.Name = "salida_element_component";
            this.Size = new System.Drawing.Size(724, 107);
            this.Load += new System.EventHandler(this.salida_element_component_Load);
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox group;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lbl_descripcion;
        internal System.Windows.Forms.Label lbl_oid;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txt_nps;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label lbl_job;
    }
}
