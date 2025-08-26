using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cliparts.Tools.Base;

namespace Cliparts.Messaging {
    public partial class FormError : Form {

        #region Properties

        Business business;

        List<ErrorEventArgs> errorList = new List<ErrorEventArgs>();

        private bool adjustingGUI = false;

        #endregion


        #region Funktionen

        public FormError(
            Business business,
            ErrorEventArgs[] errorList,
            string[] errorTextList,
            ErrorStatus status) {
            
            InitializeComponent();

            BackColor = ClipartsColors.DE_DARKBLUE;
            Text = String.Format("Error @ {0}", Application.ProductName);

            this.business = business;
            this.business.ErrorListChanged += new EventHandler<ErrorListChangedEventArgs>(this.business_ErrorListChanged);
            this.business.StatusChanged += new EventHandler<MessageStatusChangedEventArgs>(this.business_StatusChanged);

            this.business_ErrorListChanged(this, new ErrorListChangedEventArgs(errorList, errorTextList));
            this.business_StatusChanged(this, new MessageStatusChangedEventArgs(status, string.Empty));

            this.panelSelectedError.Visible = false;

        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        private void FormError_Click(object sender, EventArgs e) {
            this.listBoxErrors.SelectedIndex = -1;
        }

        private void FormError_FormClosing(object sender, FormClosingEventArgs e) {
            this.panelSelectedError.Visible = false;
            this.business.ErrorListChanged -= this.business_ErrorListChanged;
            this.business.StatusChanged -= this.business_StatusChanged;
        }

        private void business_ErrorListChanged(object sender, ErrorListChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(()=> this.business_ErrorListChanged(sender, e)));
            else {
                this.adjustingGUI = true;

                if (e == null || e.ErrorList == null) this.errorList = new List<ErrorEventArgs>();
                else this.errorList = new List<ErrorEventArgs>(e.ErrorList);

                this.listBoxErrors.BeginUpdate();
                int topIndex = this.listBoxErrors.TopIndex;
                this.listBoxErrors.Items.Clear();
                if (e != null && e.ErrorTextList != null) this.listBoxErrors.Items.AddRange(e.ErrorTextList);
                if (topIndex > this.listBoxErrors.Items.Count) this.listBoxErrors.TopIndex = this.listBoxErrors.Items.Count - 1;
                else this.listBoxErrors.TopIndex = topIndex;
                this.listBoxErrors.Enabled = this.listBoxErrors.Items.Count > 0;
                this.listBoxErrors.EndUpdate();

                this.buttonClearList.Enabled = this.listBoxErrors.Enabled;

                this.panelSelectedError.Visible = this.listBoxErrors.Enabled;

                this.adjustingGUI = false;
            }
        }
        private void listBoxErrors_SelectedIndexChanged(object sender, EventArgs e) {
            if (!this.adjustingGUI) {
                if (this.listBoxErrors.Enabled) {
                    int index = this.listBoxErrors.SelectedIndex;
                    if (index >= 0 && index < this.errorList.Count) {
                        ErrorEventArgs error = this.errorList[index];
                        if (error == null) this.panelSelectedError.Visible = false;
                        else {
                            this.textBoxSelectedErrorSender.Text = error.Sender.ToString();
                            this.textBoxSelectedErrorSubSender.Text = error.SubSender.ToString();
                            this.textBoxSelectedErrorTime.Text = error.TimeStamp.ToLongTimeString();
                            this.textBoxSelectedErrorMessage.Text = error.Message;
                            this.panelSelectedError.Visible = true;
                        }
                    }
                    else this.panelSelectedError.Visible = false;
                }
                else this.panelSelectedError.Visible = false;
            }
        }

        private void business_StatusChanged(object sender, MessageStatusChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_StatusChanged(sender, e)));
            else {
                this.adjustingGUI = true;

                this.adjustingGUI = false;
            }
        }

        private void buttonClearList_Click(object sender, EventArgs e) {
            this.business.ClearErrorList();
            this.listBoxErrors.SelectedIndex = -1;
            this.Close();
        }

        #endregion

    }
}
