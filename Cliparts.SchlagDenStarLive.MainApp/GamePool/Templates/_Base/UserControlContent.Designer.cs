namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base {
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
            this.userControlViewer = new Cliparts.VRemote4.Viewer.UserControlViewer();
            this.labelGameClass = new System.Windows.Forms.Label();
            this.checkBoxShowSafeArea = new System.Windows.Forms.CheckBox();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.groupBoxPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // userControlViewer
            // 
            this.userControlViewer.BackColor = System.Drawing.Color.Black;
            this.userControlViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControlViewer.Location = new System.Drawing.Point(8, 6);
            this.userControlViewer.Margin = new System.Windows.Forms.Padding(172, 40, 172, 40);
            this.userControlViewer.Name = "userControlViewer";
            this.userControlViewer.Size = new System.Drawing.Size(800, 450);
            this.userControlViewer.TabIndex = 7;
            // 
            // labelGameClass
            // 
            this.labelGameClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameClass.Location = new System.Drawing.Point(6, 877);
            this.labelGameClass.Name = "labelGameClass";
            this.labelGameClass.Size = new System.Drawing.Size(1294, 18);
            this.labelGameClass.TabIndex = 6;
            this.labelGameClass.Text = "labelGameClass";
            this.labelGameClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxShowSafeArea
            // 
            this.checkBoxShowSafeArea.AutoSize = true;
            this.checkBoxShowSafeArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowSafeArea.Location = new System.Drawing.Point(11, 14);
            this.checkBoxShowSafeArea.Name = "checkBoxShowSafeArea";
            this.checkBoxShowSafeArea.Size = new System.Drawing.Size(97, 22);
            this.checkBoxShowSafeArea.TabIndex = 8;
            this.checkBoxShowSafeArea.Text = "safe area";
            this.checkBoxShowSafeArea.UseVisualStyleBackColor = true;
            this.checkBoxShowSafeArea.CheckedChanged += new System.EventHandler(this.checkBoxShowSafeArea_CheckedChanged);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.checkBoxShowSafeArea);
            this.groupBoxPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPreview.ForeColor = System.Drawing.Color.White;
            this.groupBoxPreview.Location = new System.Drawing.Point(819, 416);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(536, 40);
            this.groupBoxPreview.TabIndex = 9;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "preview";
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.BackColor = System.Drawing.Color.Black;
            this.pictureBoxLoading.Image = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Loading;
            this.pictureBoxLoading.Location = new System.Drawing.Point(311, 120);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(190, 220);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLoading.TabIndex = 10;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // UserControlContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Indigo;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBoxLoading);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.userControlViewer);
            this.Controls.Add(this.labelGameClass);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlContent";
            this.Size = new System.Drawing.Size(1358, 893);
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VRemote4.Viewer.UserControlViewer userControlViewer;
        protected System.Windows.Forms.Label labelGameClass;
        protected System.Windows.Forms.GroupBox groupBoxPreview;
        protected System.Windows.Forms.CheckBox checkBoxShowSafeArea;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
    }
}
