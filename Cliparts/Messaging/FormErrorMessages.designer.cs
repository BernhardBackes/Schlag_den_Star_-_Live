namespace Cliparts.Messaging {
    partial class FormError {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
            this.listBoxErrors = new System.Windows.Forms.ListBox();
            this.buttonClearList = new System.Windows.Forms.Button();
            this.panelSelectedError = new System.Windows.Forms.Panel();
            this.labelSelectedErrorSender = new System.Windows.Forms.Label();
            this.textBoxSelectedErrorSender = new System.Windows.Forms.TextBox();
            this.textBoxSelectedErrorSubSender = new System.Windows.Forms.TextBox();
            this.labelSelectedErrorSubSender = new System.Windows.Forms.Label();
            this.textBoxSelectedErrorTime = new System.Windows.Forms.TextBox();
            this.labelSelectedErrorTime = new System.Windows.Forms.Label();
            this.textBoxSelectedErrorMessage = new System.Windows.Forms.TextBox();
            this.labelSelectedErrorMessage = new System.Windows.Forms.Label();
            this.panelSelectedError.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.FormattingEnabled = true;
            this.listBoxErrors.ItemHeight = 18;
            this.listBoxErrors.Location = new System.Drawing.Point(12, 12);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.Size = new System.Drawing.Size(994, 490);
            this.listBoxErrors.TabIndex = 0;
            this.listBoxErrors.SelectedIndexChanged += new System.EventHandler(this.listBoxErrors_SelectedIndexChanged);
            // 
            // buttonClearList
            // 
            this.buttonClearList.ForeColor = System.Drawing.Color.Black;
            this.buttonClearList.Location = new System.Drawing.Point(164, 508);
            this.buttonClearList.Name = "buttonClearList";
            this.buttonClearList.Size = new System.Drawing.Size(690, 29);
            this.buttonClearList.TabIndex = 1;
            this.buttonClearList.Text = "clear list";
            this.buttonClearList.UseVisualStyleBackColor = true;
            this.buttonClearList.Click += new System.EventHandler(this.buttonClearList_Click);
            // 
            // panelSelectedError
            // 
            this.panelSelectedError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSelectedError.Controls.Add(this.textBoxSelectedErrorMessage);
            this.panelSelectedError.Controls.Add(this.labelSelectedErrorMessage);
            this.panelSelectedError.Controls.Add(this.textBoxSelectedErrorTime);
            this.panelSelectedError.Controls.Add(this.labelSelectedErrorTime);
            this.panelSelectedError.Controls.Add(this.textBoxSelectedErrorSubSender);
            this.panelSelectedError.Controls.Add(this.labelSelectedErrorSubSender);
            this.panelSelectedError.Controls.Add(this.textBoxSelectedErrorSender);
            this.panelSelectedError.Controls.Add(this.labelSelectedErrorSender);
            this.panelSelectedError.Location = new System.Drawing.Point(164, 110);
            this.panelSelectedError.Name = "panelSelectedError";
            this.panelSelectedError.Size = new System.Drawing.Size(690, 264);
            this.panelSelectedError.TabIndex = 2;
            this.panelSelectedError.Visible = false;
            // 
            // labelSelectedErrorSender
            // 
            this.labelSelectedErrorSender.Location = new System.Drawing.Point(3, 10);
            this.labelSelectedErrorSender.Name = "labelSelectedErrorSender";
            this.labelSelectedErrorSender.Size = new System.Drawing.Size(110, 24);
            this.labelSelectedErrorSender.TabIndex = 0;
            this.labelSelectedErrorSender.Text = "sender:";
            this.labelSelectedErrorSender.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSelectedErrorSender
            // 
            this.textBoxSelectedErrorSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedErrorSender.Location = new System.Drawing.Point(119, 12);
            this.textBoxSelectedErrorSender.Name = "textBoxSelectedErrorSender";
            this.textBoxSelectedErrorSender.ReadOnly = true;
            this.textBoxSelectedErrorSender.Size = new System.Drawing.Size(549, 22);
            this.textBoxSelectedErrorSender.TabIndex = 1;
            this.textBoxSelectedErrorSender.Text = "textBoxSelectedErrorSender";
            // 
            // textBoxSelectedErrorSubSender
            // 
            this.textBoxSelectedErrorSubSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedErrorSubSender.Location = new System.Drawing.Point(119, 40);
            this.textBoxSelectedErrorSubSender.Name = "textBoxSelectedErrorSubSender";
            this.textBoxSelectedErrorSubSender.ReadOnly = true;
            this.textBoxSelectedErrorSubSender.Size = new System.Drawing.Size(549, 22);
            this.textBoxSelectedErrorSubSender.TabIndex = 3;
            this.textBoxSelectedErrorSubSender.Text = "textBoxSelectedErrorSubSender";
            // 
            // labelSelectedErrorSubSender
            // 
            this.labelSelectedErrorSubSender.Location = new System.Drawing.Point(3, 38);
            this.labelSelectedErrorSubSender.Name = "labelSelectedErrorSubSender";
            this.labelSelectedErrorSubSender.Size = new System.Drawing.Size(110, 24);
            this.labelSelectedErrorSubSender.TabIndex = 2;
            this.labelSelectedErrorSubSender.Text = "sub sender:";
            this.labelSelectedErrorSubSender.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSelectedErrorTime
            // 
            this.textBoxSelectedErrorTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedErrorTime.Location = new System.Drawing.Point(119, 68);
            this.textBoxSelectedErrorTime.Name = "textBoxSelectedErrorTime";
            this.textBoxSelectedErrorTime.ReadOnly = true;
            this.textBoxSelectedErrorTime.Size = new System.Drawing.Size(549, 22);
            this.textBoxSelectedErrorTime.TabIndex = 5;
            this.textBoxSelectedErrorTime.Text = "textBoxSelectedErrorTime";
            // 
            // labelSelectedErrorTime
            // 
            this.labelSelectedErrorTime.Location = new System.Drawing.Point(3, 66);
            this.labelSelectedErrorTime.Name = "labelSelectedErrorTime";
            this.labelSelectedErrorTime.Size = new System.Drawing.Size(110, 24);
            this.labelSelectedErrorTime.TabIndex = 4;
            this.labelSelectedErrorTime.Text = "time stamp:";
            this.labelSelectedErrorTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSelectedErrorMessage
            // 
            this.textBoxSelectedErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSelectedErrorMessage.Location = new System.Drawing.Point(119, 96);
            this.textBoxSelectedErrorMessage.Multiline = true;
            this.textBoxSelectedErrorMessage.Name = "textBoxSelectedErrorMessage";
            this.textBoxSelectedErrorMessage.ReadOnly = true;
            this.textBoxSelectedErrorMessage.Size = new System.Drawing.Size(549, 147);
            this.textBoxSelectedErrorMessage.TabIndex = 7;
            this.textBoxSelectedErrorMessage.Text = "textBoxSelectedErrorMessage";
            // 
            // labelSelectedErrorMessage
            // 
            this.labelSelectedErrorMessage.Location = new System.Drawing.Point(3, 93);
            this.labelSelectedErrorMessage.Name = "labelSelectedErrorMessage";
            this.labelSelectedErrorMessage.Size = new System.Drawing.Size(110, 24);
            this.labelSelectedErrorMessage.TabIndex = 6;
            this.labelSelectedErrorMessage.Text = "message:";
            this.labelSelectedErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormError
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.panelSelectedError);
            this.Controls.Add(this.buttonClearList);
            this.Controls.Add(this.listBoxErrors);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormError";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormError_FormClosing);
            this.Click += new System.EventHandler(this.FormError_Click);
            this.panelSelectedError.ResumeLayout(false);
            this.panelSelectedError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxErrors;
        private System.Windows.Forms.Button buttonClearList;
        private System.Windows.Forms.Panel panelSelectedError;
        private System.Windows.Forms.TextBox textBoxSelectedErrorTime;
        private System.Windows.Forms.Label labelSelectedErrorTime;
        private System.Windows.Forms.TextBox textBoxSelectedErrorSubSender;
        private System.Windows.Forms.Label labelSelectedErrorSubSender;
        private System.Windows.Forms.TextBox textBoxSelectedErrorSender;
        private System.Windows.Forms.Label labelSelectedErrorSender;
        private System.Windows.Forms.TextBox textBoxSelectedErrorMessage;
        private System.Windows.Forms.Label labelSelectedErrorMessage;
    }
}