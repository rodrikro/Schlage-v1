namespace Lineas.pantallas {
    partial class salida {
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
            this.timerCheckPLC = new System.Windows.Forms.Timer(this.components);
            this.salidaBox = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.FlowLayoutPanel1 = new Lineas.shared.VerticalFlowPanel();
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.salidaBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(576, 24);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(194, 34);
            this.Button1.TabIndex = 22;
            this.Button1.Text = "Iniciar Teclado";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // timerCheckPLC
            // 
            this.timerCheckPLC.Enabled = true;
            this.timerCheckPLC.Interval = 1000;
            this.timerCheckPLC.Tick += new System.EventHandler(this.timerCheckPLC_Tick);
            // 
            // salidaBox
            // 
            this.salidaBox.Controls.Add(this.textBoxLog);
            this.salidaBox.Controls.Add(this.FlowLayoutPanel1);
            this.salidaBox.Location = new System.Drawing.Point(10, 74);
            this.salidaBox.Name = "salidaBox";
            this.salidaBox.Size = new System.Drawing.Size(760, 620);
            this.salidaBox.TabIndex = 24;
            this.salidaBox.TabStop = false;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(6, 538);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(748, 76);
            this.textBoxLog.TabIndex = 24;
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.AutoScroll = true;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(748, 512);
            this.FlowLayoutPanel1.TabIndex = 23;
            // 
            // timerLog
            // 
            this.timerLog.Enabled = true;
            this.timerLog.Tick += new System.EventHandler(this.timerLog_Tick);
            // 
            // salida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 706);
            this.ControlBox = false;
            this.Controls.Add(this.salidaBox);
            this.Controls.Add(this.Button1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(790, 740);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(790, 740);
            this.Name = "salida";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linea Salida";
            this.salidaBox.ResumeLayout(false);
            this.salidaBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Timer timerCheckPLC;
        private System.Windows.Forms.GroupBox salidaBox;
        private System.Windows.Forms.TextBox textBoxLog;
        internal shared.VerticalFlowPanel FlowLayoutPanel1;
        private System.Windows.Forms.Timer timerLog;
    }
}