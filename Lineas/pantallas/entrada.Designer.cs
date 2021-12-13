namespace Lineas.pantallas {
    partial class entrada {
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
            this.Button1 = new System.Windows.Forms.Button();
            this.btn_cycleNext = new System.Windows.Forms.Button();
            this.lineStat = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gp_data = new System.Windows.Forms.GroupBox();
            this.plcDataTimer = new System.Windows.Forms.Timer(this.components);
            this.dataLinea = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(595, 23);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(158, 34);
            this.Button1.TabIndex = 24;
            this.Button1.Text = "Iniciar Teclado";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btn_cycleNext
            // 
            this.btn_cycleNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cycleNext.Location = new System.Drawing.Point(164, 23);
            this.btn_cycleNext.Name = "btn_cycleNext";
            this.btn_cycleNext.Size = new System.Drawing.Size(143, 34);
            this.btn_cycleNext.TabIndex = 26;
            this.btn_cycleNext.Text = "Siguiente Ciclo";
            this.btn_cycleNext.UseVisualStyleBackColor = true;
            this.btn_cycleNext.Click += new System.EventHandler(this.btn_cycleNext_Click);
            // 
            // lineStat
            // 
            this.lineStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineStat.Location = new System.Drawing.Point(21, 23);
            this.lineStat.Name = "lineStat";
            this.lineStat.Size = new System.Drawing.Size(136, 34);
            this.lineStat.TabIndex = 25;
            this.lineStat.Text = " ";
            this.lineStat.UseVisualStyleBackColor = true;
            this.lineStat.Click += new System.EventHandler(this.lineStat_Click);
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(431, 23);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(158, 34);
            this.Button2.TabIndex = 27;
            this.Button2.Text = "Calculadora";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(17, 84);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(740, 200);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // gp_data
            // 
            this.gp_data.Location = new System.Drawing.Point(12, 288);
            this.gp_data.Name = "gp_data";
            this.gp_data.Padding = new System.Windows.Forms.Padding(5);
            this.gp_data.Size = new System.Drawing.Size(749, 406);
            this.gp_data.TabIndex = 29;
            this.gp_data.TabStop = false;
            this.gp_data.Text = "groupBox1";
            // 
            // plcDataTimer
            // 
            this.plcDataTimer.Interval = 500;
            this.plcDataTimer.Tick += new System.EventHandler(this.plcDataTimer_Tick);
            // 
            // dataLinea
            // 
            this.dataLinea.Enabled = true;
            this.dataLinea.Interval = 300000;
            this.dataLinea.Tick += new System.EventHandler(this.dataLinea_Tick);
            // 
            // entrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 706);
            this.ControlBox = false;
            this.Controls.Add(this.gp_data);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.btn_cycleNext);
            this.Controls.Add(this.lineStat);
            this.Controls.Add(this.Button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(780, 740);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(780, 740);
            this.Name = "entrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "$";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entrada_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button btn_cycleNext;
        internal System.Windows.Forms.Button lineStat;
        internal System.Windows.Forms.Button Button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gp_data;
        private System.Windows.Forms.Timer plcDataTimer;
        private System.Windows.Forms.Timer dataLinea;
    }
}