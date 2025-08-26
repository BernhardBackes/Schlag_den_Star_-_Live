
namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    partial class UserControlChannel {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.labelVoltageValue = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelVoltagePeakValue = new System.Windows.Forms.Label();
            this.labelVoltagePeak = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelVoltageValue
            // 
            this.labelVoltageValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelVoltageValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoltageValue.Location = new System.Drawing.Point(3, 45);
            this.labelVoltageValue.Name = "labelVoltageValue";
            this.labelVoltageValue.Size = new System.Drawing.Size(244, 77);
            this.labelVoltageValue.TabIndex = 0;
            this.labelVoltageValue.Text = "labelVoltageValue";
            this.labelVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelIndex
            // 
            this.labelIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIndex.Location = new System.Drawing.Point(3, 3);
            this.labelIndex.Name = "labelIndex";
            this.labelIndex.Size = new System.Drawing.Size(244, 36);
            this.labelIndex.TabIndex = 1;
            this.labelIndex.Text = "labelIndex";
            this.labelIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOpen
            // 
            this.buttonOpen.ForeColor = System.Drawing.Color.Black;
            this.buttonOpen.Location = new System.Drawing.Point(3, 165);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(120, 30);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.ForeColor = System.Drawing.Color.Black;
            this.buttonClose.Location = new System.Drawing.Point(127, 165);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(120, 30);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelVoltagePeakValue
            // 
            this.labelVoltagePeakValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelVoltagePeakValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoltagePeakValue.Location = new System.Drawing.Point(127, 126);
            this.labelVoltagePeakValue.Name = "labelVoltagePeakValue";
            this.labelVoltagePeakValue.Size = new System.Drawing.Size(120, 36);
            this.labelVoltagePeakValue.TabIndex = 4;
            this.labelVoltagePeakValue.Text = "labelVoltagePeakValue";
            this.labelVoltagePeakValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVoltagePeak
            // 
            this.labelVoltagePeak.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoltagePeak.Location = new System.Drawing.Point(3, 126);
            this.labelVoltagePeak.Name = "labelVoltagePeak";
            this.labelVoltagePeak.Size = new System.Drawing.Size(120, 36);
            this.labelVoltagePeak.TabIndex = 5;
            this.labelVoltagePeak.Text = "peak:";
            this.labelVoltagePeak.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserControlChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Sienna;
            this.Controls.Add(this.labelVoltagePeak);
            this.Controls.Add(this.labelVoltagePeakValue);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.labelIndex);
            this.Controls.Add(this.labelVoltageValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "UserControlChannel";
            this.Size = new System.Drawing.Size(250, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelVoltageValue;
        private System.Windows.Forms.Label labelIndex;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelVoltagePeakValue;
        private System.Windows.Forms.Label labelVoltagePeak;
    }
}
