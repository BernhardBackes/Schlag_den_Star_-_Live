namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

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
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.labelScorePositionY = new System.Windows.Forms.Label();
            this.labelScorePositionX = new System.Windows.Forms.Label();
            this.numericUpDownGamePositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGamePositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamePositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxGame
            // 
            this.groupBoxGame.Controls.Add(this.labelScorePositionY);
            this.groupBoxGame.Controls.Add(this.labelScorePositionX);
            this.groupBoxGame.Controls.Add(this.numericUpDownGamePositionY);
            this.groupBoxGame.Controls.Add(this.numericUpDownGamePositionX);
            this.groupBoxGame.ForeColor = System.Drawing.Color.White;
            this.groupBoxGame.Location = new System.Drawing.Point(819, 6);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(534, 50);
            this.groupBoxGame.TabIndex = 17;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "game";
            // 
            // labelScorePositionY
            // 
            this.labelScorePositionY.Location = new System.Drawing.Point(197, 18);
            this.labelScorePositionY.Name = "labelScorePositionY";
            this.labelScorePositionY.Size = new System.Drawing.Size(30, 24);
            this.labelScorePositionY.TabIndex = 3;
            this.labelScorePositionY.Text = "y:";
            this.labelScorePositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelScorePositionX
            // 
            this.labelScorePositionX.Location = new System.Drawing.Point(6, 18);
            this.labelScorePositionX.Name = "labelScorePositionX";
            this.labelScorePositionX.Size = new System.Drawing.Size(100, 24);
            this.labelScorePositionX.TabIndex = 2;
            this.labelScorePositionX.Text = "position.x:";
            this.labelScorePositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownGamePositionY
            // 
            this.numericUpDownGamePositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownGamePositionY.Name = "numericUpDownGamePositionY";
            this.numericUpDownGamePositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownGamePositionY.TabIndex = 1;
            this.numericUpDownGamePositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownGamePositionY.ValueChanged += new System.EventHandler(this.numericUpDownGamePositionY_ValueChanged);
            // 
            // numericUpDownGamePositionX
            // 
            this.numericUpDownGamePositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownGamePositionX.Name = "numericUpDownGamePositionX";
            this.numericUpDownGamePositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownGamePositionX.TabIndex = 0;
            this.numericUpDownGamePositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownGamePositionX.ValueChanged += new System.EventHandler(this.numericUpDownGamePositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxGame);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.groupBoxGame, 0);
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamePositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxGame;
        protected System.Windows.Forms.Label labelScorePositionY;
        protected System.Windows.Forms.Label labelScorePositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownGamePositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownGamePositionX;
    }
}
