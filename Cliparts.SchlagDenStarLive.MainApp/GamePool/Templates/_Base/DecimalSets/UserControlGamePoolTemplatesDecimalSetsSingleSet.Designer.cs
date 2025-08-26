namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalSets {
    partial class UserControlGamePoolTemplatesDecimalSetsSingleSet {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.buttonInvalid = new System.Windows.Forms.Button();
            this.buttonValid = new System.Windows.Forms.Button();
            this.buttonIdle = new System.Windows.Forms.Button();
            this.numericUpDownValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInvalid
            // 
            this.buttonInvalid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInvalid.ForeColor = System.Drawing.Color.Black;
            this.buttonInvalid.Location = new System.Drawing.Point(2, 90);
            this.buttonInvalid.Name = "buttonInvalid";
            this.buttonInvalid.Size = new System.Drawing.Size(84, 22);
            this.buttonInvalid.TabIndex = 7;
            this.buttonInvalid.Text = "invalid";
            this.buttonInvalid.UseVisualStyleBackColor = true;
            this.buttonInvalid.Click += new System.EventHandler(this.buttonInvalid_Click);
            // 
            // buttonValid
            // 
            this.buttonValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonValid.ForeColor = System.Drawing.Color.Black;
            this.buttonValid.Location = new System.Drawing.Point(2, 68);
            this.buttonValid.Name = "buttonValid";
            this.buttonValid.Size = new System.Drawing.Size(84, 22);
            this.buttonValid.TabIndex = 6;
            this.buttonValid.Text = "valid";
            this.buttonValid.UseVisualStyleBackColor = true;
            this.buttonValid.Click += new System.EventHandler(this.buttonValid_Click);
            // 
            // buttonIdle
            // 
            this.buttonIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIdle.ForeColor = System.Drawing.Color.Black;
            this.buttonIdle.Location = new System.Drawing.Point(2, 46);
            this.buttonIdle.Name = "buttonIdle";
            this.buttonIdle.Size = new System.Drawing.Size(84, 22);
            this.buttonIdle.TabIndex = 5;
            this.buttonIdle.Text = "idle";
            this.buttonIdle.UseVisualStyleBackColor = true;
            this.buttonIdle.Click += new System.EventHandler(this.buttonIdle_Click);
            // 
            // numericUpDownValue
            // 
            this.numericUpDownValue.DecimalPlaces = 2;
            this.numericUpDownValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownValue.Location = new System.Drawing.Point(2, 4);
            this.numericUpDownValue.Name = "numericUpDownValue";
            this.numericUpDownValue.Size = new System.Drawing.Size(84, 31);
            this.numericUpDownValue.TabIndex = 4;
            this.numericUpDownValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownValue.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownValue.ValueChanged += new System.EventHandler(this.numericUpDownValue_ValueChanged);
            // 
            // UserControlGamePoolTemplatesDecimalSetsSingleSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.buttonInvalid);
            this.Controls.Add(this.buttonValid);
            this.Controls.Add(this.buttonIdle);
            this.Controls.Add(this.numericUpDownValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesDecimalSetsSingleSet";
            this.Size = new System.Drawing.Size(90, 115);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInvalid;
        private System.Windows.Forms.Button buttonValid;
        private System.Windows.Forms.Button buttonIdle;
        private System.Windows.Forms.NumericUpDown numericUpDownValue;
    }
}
