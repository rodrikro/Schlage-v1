namespace Lineas.shared {
    partial class andon_component {
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
            this.components = new System.ComponentModel.Container();
            this.ButtonResuelto = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.ButtonLlamar = new System.Windows.Forms.Button();
            this.andonStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.delayTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ButtonResuelto
            // 
            this.ButtonResuelto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResuelto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonResuelto.Location = new System.Drawing.Point(9, 85);
            this.ButtonResuelto.Name = "ButtonResuelto";
            this.ButtonResuelto.Size = new System.Drawing.Size(264, 39);
            this.ButtonResuelto.TabIndex = 17;
            this.ButtonResuelto.Text = "Resuelto";
            this.ButtonResuelto.UseVisualStyleBackColor = true;
            this.ButtonResuelto.Click += new System.EventHandler(this.ButtonResuelto_Click);
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, -1);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(283, 31);
            this.title.TabIndex = 16;
            this.title.Text = "Title";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonLlamar
            // 
            this.ButtonLlamar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLlamar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLlamar.Location = new System.Drawing.Point(9, 34);
            this.ButtonLlamar.Name = "ButtonLlamar";
            this.ButtonLlamar.Size = new System.Drawing.Size(264, 39);
            this.ButtonLlamar.TabIndex = 15;
            this.ButtonLlamar.Text = "Llamar";
            this.ButtonLlamar.UseVisualStyleBackColor = true;
            this.ButtonLlamar.Click += new System.EventHandler(this.ButtonLlamar_Click);
            // 
            // andonStatusTimer
            // 
            this.andonStatusTimer.Interval = 2000;
            this.andonStatusTimer.Tick += new System.EventHandler(this.andonStatusTimer_Tick);
            // 
            // delayTimer
            // 
            this.delayTimer.Interval = 3000;
            this.delayTimer.Tick += new System.EventHandler(this.delayTimer_Tick);
            // 
            // andon_component
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ButtonResuelto);
            this.Controls.Add(this.title);
            this.Controls.Add(this.ButtonLlamar);
            this.Name = "andon_component";
            this.Size = new System.Drawing.Size(283, 136);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button ButtonResuelto;
        internal System.Windows.Forms.Label title;
        internal System.Windows.Forms.Button ButtonLlamar;
        private System.Windows.Forms.Timer andonStatusTimer;
        private System.Windows.Forms.Timer delayTimer;
    }
}
