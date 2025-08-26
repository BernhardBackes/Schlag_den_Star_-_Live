namespace Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard {

    partial class UserControlGameboardLevel {
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
            this.buttonLeftPlayerWinner = new System.Windows.Forms.Button();
            this.buttonRightPlayerWinner = new System.Windows.Forms.Button();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.labelID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonLeftPlayerWinner
            // 
            this.buttonLeftPlayerWinner.ForeColor = System.Drawing.Color.Black;
            this.buttonLeftPlayerWinner.Location = new System.Drawing.Point(19, 2);
            this.buttonLeftPlayerWinner.Name = "buttonLeftPlayerWinner";
            this.buttonLeftPlayerWinner.Size = new System.Drawing.Size(120, 28);
            this.buttonLeftPlayerWinner.TabIndex = 0;
            this.buttonLeftPlayerWinner.Text = "WINNER";
            this.buttonLeftPlayerWinner.UseVisualStyleBackColor = true;
            this.buttonLeftPlayerWinner.Click += new System.EventHandler(this.buttonLeftPlayerWinner_Click);
            // 
            // buttonRightPlayerWinner
            // 
            this.buttonRightPlayerWinner.ForeColor = System.Drawing.Color.Black;
            this.buttonRightPlayerWinner.Location = new System.Drawing.Point(183, 2);
            this.buttonRightPlayerWinner.Name = "buttonRightPlayerWinner";
            this.buttonRightPlayerWinner.Size = new System.Drawing.Size(120, 28);
            this.buttonRightPlayerWinner.TabIndex = 1;
            this.buttonRightPlayerWinner.Text = "WINNER";
            this.buttonRightPlayerWinner.UseVisualStyleBackColor = true;
            this.buttonRightPlayerWinner.Click += new System.EventHandler(this.buttonRightPlayerWinner_Click);
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(307, 9);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnabled.TabIndex = 2;
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxBlocked_CheckedChanged);
            // 
            // labelID
            // 
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(136, 2);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(50, 28);
            this.labelID.TabIndex = 3;
            this.labelID.Text = "ID";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelID.Click += new System.EventHandler(this.labelID_Click);
            // 
            // UserControlGameboardLevel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBoxEnabled);
            this.Controls.Add(this.buttonRightPlayerWinner);
            this.Controls.Add(this.buttonLeftPlayerWinner);
            this.Controls.Add(this.labelID);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlGameboardLevel";
            this.Size = new System.Drawing.Size(324, 33);
            this.Click += new System.EventHandler(this.UserControlGameboardLevel_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLeftPlayerWinner;
        private System.Windows.Forms.Button buttonRightPlayerWinner;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.Label labelID;
    }
}
