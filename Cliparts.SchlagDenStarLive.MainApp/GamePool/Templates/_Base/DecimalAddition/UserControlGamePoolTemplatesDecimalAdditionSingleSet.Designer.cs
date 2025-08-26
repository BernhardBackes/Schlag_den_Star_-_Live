namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalAddition {
    partial class UserControlGamePoolTemplatesDecimalAdditionSingleSet {
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
            this.numericUpDownValue = new System.Windows.Forms.NumericUpDown();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
            this.SuspendLayout();
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
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxActive.Location = new System.Drawing.Point(9, 41);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(71, 22);
            this.checkBoxActive.TabIndex = 5;
            this.checkBoxActive.Text = "active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            this.checkBoxActive.CheckedChanged += new System.EventHandler(this.checkBoxActive_CheckedChanged);
            // 
            // UserControlGamePoolTemplatesDecimalAdditionSingleSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.numericUpDownValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesDecimalAdditionSingleSet";
            this.Size = new System.Drawing.Size(90, 70);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDownValue;
        private System.Windows.Forms.CheckBox checkBoxActive;
    }
}
