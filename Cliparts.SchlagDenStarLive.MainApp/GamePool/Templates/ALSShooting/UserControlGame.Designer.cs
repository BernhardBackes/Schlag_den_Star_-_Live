namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ALSShooting {
    partial class UserControlGame {
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
            this.groupBoxALS = new System.Windows.Forms.GroupBox();
            this.textBoxALSHost = new System.Windows.Forms.TextBox();
            this.buttonALSLockTargets = new System.Windows.Forms.Button();
            this.buttonALSDisconnect = new System.Windows.Forms.Button();
            this.buttonALSConnect = new System.Windows.Forms.Button();
            this.buttonSwapTracks = new System.Windows.Forms.Button();
            this.buttonALSShowForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerScore)).BeginInit();
            this.groupBoxLeftPlayerHeat.SuspendLayout();
            this.groupBoxRightPlayerHeat.SuspendLayout();
            this.groupBoxALS.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStep_03
            // 
            this.buttonStep_03.Location = new System.Drawing.Point(644, 292);
            // 
            // buttonVfullscreen_ShowTimer
            // 
            this.buttonVfullscreen_ShowTimer.Visible = false;
            // 
            // groupBoxALS
            // 
            this.groupBoxALS.Controls.Add(this.buttonALSShowForm);
            this.groupBoxALS.Controls.Add(this.textBoxALSHost);
            this.groupBoxALS.Controls.Add(this.buttonALSLockTargets);
            this.groupBoxALS.Controls.Add(this.buttonALSDisconnect);
            this.groupBoxALS.Controls.Add(this.buttonALSConnect);
            this.groupBoxALS.ForeColor = System.Drawing.Color.White;
            this.groupBoxALS.Location = new System.Drawing.Point(748, 215);
            this.groupBoxALS.Name = "groupBoxALS";
            this.groupBoxALS.Size = new System.Drawing.Size(220, 178);
            this.groupBoxALS.TabIndex = 327;
            this.groupBoxALS.TabStop = false;
            this.groupBoxALS.Text = "shooting";
            // 
            // textBoxALSHost
            // 
            this.textBoxALSHost.Location = new System.Drawing.Point(6, 23);
            this.textBoxALSHost.Name = "textBoxALSHost";
            this.textBoxALSHost.ReadOnly = true;
            this.textBoxALSHost.Size = new System.Drawing.Size(208, 24);
            this.textBoxALSHost.TabIndex = 15;
            this.textBoxALSHost.Text = "textBoxALSHost";
            this.textBoxALSHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonALSLockTargets
            // 
            this.buttonALSLockTargets.ForeColor = System.Drawing.Color.Black;
            this.buttonALSLockTargets.Location = new System.Drawing.Point(6, 85);
            this.buttonALSLockTargets.Name = "buttonALSLockTargets";
            this.buttonALSLockTargets.Size = new System.Drawing.Size(208, 40);
            this.buttonALSLockTargets.TabIndex = 6;
            this.buttonALSLockTargets.Text = "lock targets";
            this.buttonALSLockTargets.UseVisualStyleBackColor = true;
            this.buttonALSLockTargets.Click += new System.EventHandler(this.buttonALSLockTargets_Click);
            // 
            // buttonALSDisconnect
            // 
            this.buttonALSDisconnect.ForeColor = System.Drawing.Color.Black;
            this.buttonALSDisconnect.Location = new System.Drawing.Point(111, 53);
            this.buttonALSDisconnect.Name = "buttonALSDisconnect";
            this.buttonALSDisconnect.Size = new System.Drawing.Size(103, 26);
            this.buttonALSDisconnect.TabIndex = 5;
            this.buttonALSDisconnect.Text = "disconnect";
            this.buttonALSDisconnect.UseVisualStyleBackColor = true;
            this.buttonALSDisconnect.Click += new System.EventHandler(this.buttonALSDisconnect_Click);
            // 
            // buttonALSConnect
            // 
            this.buttonALSConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonALSConnect.Location = new System.Drawing.Point(6, 53);
            this.buttonALSConnect.Name = "buttonALSConnect";
            this.buttonALSConnect.Size = new System.Drawing.Size(103, 26);
            this.buttonALSConnect.TabIndex = 4;
            this.buttonALSConnect.Text = "connect";
            this.buttonALSConnect.UseVisualStyleBackColor = true;
            this.buttonALSConnect.Click += new System.EventHandler(this.buttonALSConnect_Click);
            // 
            // buttonSwapTracks
            // 
            this.buttonSwapTracks.ForeColor = System.Drawing.Color.Black;
            this.buttonSwapTracks.Location = new System.Drawing.Point(748, 54);
            this.buttonSwapTracks.Name = "buttonSwapTracks";
            this.buttonSwapTracks.Size = new System.Drawing.Size(220, 62);
            this.buttonSwapTracks.TabIndex = 16;
            this.buttonSwapTracks.Text = "swap tracks";
            this.buttonSwapTracks.UseVisualStyleBackColor = true;
            this.buttonSwapTracks.Click += new System.EventHandler(this.buttonSwapTracks_Click);
            // 
            // buttonALSShowForm
            // 
            this.buttonALSShowForm.ForeColor = System.Drawing.Color.Black;
            this.buttonALSShowForm.Location = new System.Drawing.Point(6, 145);
            this.buttonALSShowForm.Name = "buttonALSShowForm";
            this.buttonALSShowForm.Size = new System.Drawing.Size(208, 26);
            this.buttonALSShowForm.TabIndex = 16;
            this.buttonALSShowForm.Text = "show form";
            this.buttonALSShowForm.UseVisualStyleBackColor = true;
            this.buttonALSShowForm.Click += new System.EventHandler(this.buttonALSShowForm_Click);
            // 
            // UserControlGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSwapTracks);
            this.Controls.Add(this.groupBoxALS);
            this.Name = "UserControlGame";
            this.Controls.SetChildIndex(this.buttonVfullscreen_ShowScore, 0);
            this.Controls.SetChildIndex(this.numericUpDownLeftPlayerScore, 0);
            this.Controls.SetChildIndex(this.buttonLeftPlayerAddScoreHot_01, 0);
            this.Controls.SetChildIndex(this.numericUpDownRightPlayerScore, 0);
            this.Controls.SetChildIndex(this.buttonRightPlayerAddScoreHot_01, 0);
            this.Controls.SetChildIndex(this.buttonStep_03, 0);
            this.Controls.SetChildIndex(this.buttonStep_04, 0);
            this.Controls.SetChildIndex(this.buttonStep_05, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_ScoreIn, 0);
            this.Controls.SetChildIndex(this.buttonVstage_Init, 0);
            this.Controls.SetChildIndex(this.buttonVstage_SetScore, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_SetScore, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_ScoreOut, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_ShowTimer, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_ShowGame, 0);
            this.Controls.SetChildIndex(this.panelStepMarker, 0);
            this.Controls.SetChildIndex(this.buttonLeftPlayerWinner, 0);
            this.Controls.SetChildIndex(this.labelVersus, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.labelGame, 0);
            this.Controls.SetChildIndex(this.buttonGame_Init, 0);
            this.Controls.SetChildIndex(this.labelVfullscreen, 0);
            this.Controls.SetChildIndex(this.buttonStep_00, 0);
            this.Controls.SetChildIndex(this.labelVinsert, 0);
            this.Controls.SetChildIndex(this.buttonVgraphic_Clear, 0);
            this.Controls.SetChildIndex(this.buttonRightPlayerWinner, 0);
            this.Controls.SetChildIndex(this.buttonGame_SetWinner, 0);
            this.Controls.SetChildIndex(this.buttonStep_01, 0);
            this.Controls.SetChildIndex(this.buttonVgraphic_Clear_1, 0);
            this.Controls.SetChildIndex(this.textBoxLeftPlayerName, 0);
            this.Controls.SetChildIndex(this.textBoxRightPlayerName, 0);
            this.Controls.SetChildIndex(this.labelVplayers, 0);
            this.Controls.SetChildIndex(this.labelVhost, 0);
            this.Controls.SetChildIndex(this.labelVleftplayer, 0);
            this.Controls.SetChildIndex(this.labelVrightplayer, 0);
            this.Controls.SetChildIndex(this.buttonVstage_Clear, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_ShowGameboard, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_UpdateGameboard, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_LoadScene, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_LoadScene, 0);
            this.Controls.SetChildIndex(this.buttonVhost_LoadScene, 0);
            this.Controls.SetChildIndex(this.buttonVleftplayer_LoadScene, 0);
            this.Controls.SetChildIndex(this.buttonVrightplayer_LoadScene, 0);
            this.Controls.SetChildIndex(this.buttonVstage_Clear_1, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_UnloadScene, 0);
            this.Controls.SetChildIndex(this.buttonVfullscreen_UnloadScene, 0);
            this.Controls.SetChildIndex(this.buttonVhost_UnloadScene, 0);
            this.Controls.SetChildIndex(this.buttonVleftplayer_UnloadScene, 0);
            this.Controls.SetChildIndex(this.buttonVrightplayer_UnloadScene, 0);
            this.Controls.SetChildIndex(this.buttonStep_02, 0);
            this.Controls.SetChildIndex(this.checkBoxFlipPlayers, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_ShootingIn, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_SetShooting, 0);
            this.Controls.SetChildIndex(this.buttonVinsert_ShootingOut, 0);
            this.Controls.SetChildIndex(this.groupBoxLeftPlayerHeat, 0);
            this.Controls.SetChildIndex(this.groupBoxRightPlayerHeat, 0);
            this.Controls.SetChildIndex(this.buttonStep_10, 0);
            this.Controls.SetChildIndex(this.buttonStep_09, 0);
            this.Controls.SetChildIndex(this.buttonStep_08, 0);
            this.Controls.SetChildIndex(this.buttonStep_07, 0);
            this.Controls.SetChildIndex(this.buttonStep_06, 0);
            this.Controls.SetChildIndex(this.buttonStep_11, 0);
            this.Controls.SetChildIndex(this.buttonGame_Resolve, 0);
            this.Controls.SetChildIndex(this.buttonGame_Next, 0);
            this.Controls.SetChildIndex(this.groupBoxALS, 0);
            this.Controls.SetChildIndex(this.buttonSwapTracks, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerScore)).EndInit();
            this.groupBoxLeftPlayerHeat.ResumeLayout(false);
            this.groupBoxRightPlayerHeat.ResumeLayout(false);
            this.groupBoxALS.ResumeLayout(false);
            this.groupBoxALS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxALS;
        private System.Windows.Forms.TextBox textBoxALSHost;
        private System.Windows.Forms.Button buttonALSLockTargets;
        private System.Windows.Forms.Button buttonALSDisconnect;
        private System.Windows.Forms.Button buttonALSConnect;
        private System.Windows.Forms.Button buttonSwapTracks;
        private System.Windows.Forms.Button buttonALSShowForm;
    }
}
