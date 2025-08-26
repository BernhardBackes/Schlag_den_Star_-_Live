namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Timer {
    partial class UserControlContent {
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
            this.userControlGamePoolTemplates_ModulesTimerContent = new Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Modules.Timer.UserControlGamePoolTemplates_ModulesTimerContent();
            this.checkBoxShowFullscreenTimer = new System.Windows.Forms.CheckBox();
            this.groupBoxPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // userControlGamePoolTemplates_ModulesTimerContent
            // 
            this.userControlGamePoolTemplates_ModulesTimerContent.BackColor = System.Drawing.Color.Black;
            this.userControlGamePoolTemplates_ModulesTimerContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlGamePoolTemplates_ModulesTimerContent.ForeColor = System.Drawing.Color.White;
            this.userControlGamePoolTemplates_ModulesTimerContent.Location = new System.Drawing.Point(819, 6);
            this.userControlGamePoolTemplates_ModulesTimerContent.Name = "userControlGamePoolTemplates_ModulesTimerContent";
            this.userControlGamePoolTemplates_ModulesTimerContent.Size = new System.Drawing.Size(534, 142);
            this.userControlGamePoolTemplates_ModulesTimerContent.TabIndex = 11;
            // 
            // checkBoxShowFullscreenTimer
            // 
            this.checkBoxShowFullscreenTimer.AutoSize = true;
            this.checkBoxShowFullscreenTimer.Location = new System.Drawing.Point(819, 154);
            this.checkBoxShowFullscreenTimer.Name = "checkBoxShowFullscreenTimer";
            this.checkBoxShowFullscreenTimer.Size = new System.Drawing.Size(188, 22);
            this.checkBoxShowFullscreenTimer.TabIndex = 13;
            this.checkBoxShowFullscreenTimer.Text = "show fullscreen timer";
            this.checkBoxShowFullscreenTimer.UseVisualStyleBackColor = true;
            this.checkBoxShowFullscreenTimer.CheckedChanged += new System.EventHandler(this.checkBoxShowFullscreenTimer_CheckedChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxShowFullscreenTimer);
            this.Controls.Add(this.userControlGamePoolTemplates_ModulesTimerContent);
            this.Name = "UserControlContent";
            this.BackColorChanged += new System.EventHandler(this.UserControlContent_BackColorChanged);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.userControlGamePoolTemplates_ModulesTimerContent, 0);
            this.Controls.SetChildIndex(this.checkBoxShowFullscreenTimer, 0);
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _Modules.Timer.UserControlGamePoolTemplates_ModulesTimerContent userControlGamePoolTemplates_ModulesTimerContent;
        private System.Windows.Forms.CheckBox checkBoxShowFullscreenTimer;
    }
}
