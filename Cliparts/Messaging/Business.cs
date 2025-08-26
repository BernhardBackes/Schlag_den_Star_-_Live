using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cliparts.Messaging {

    public enum ErrorStatus {
        NoMessage,
        NewMessage,
        MessageAvailable
    }

    public class ErrorEventArgs : EventArgs {
        public object Sender { get; private set; }
        public object SubSender { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string Message { get; private set; }
        public string Text { get; private set; }
        public string ConsoleText { get; private set; }
        public ErrorEventArgs(
            object sender,
            object subSender,
            DateTime timeStamp,
            string message) {
            if (sender == null) this.Sender = "<sender not available>";
            else this.Sender = sender;
            if (subSender == null) this.SubSender = string.Empty;
            else this.SubSender = subSender;
            if (timeStamp == null) this.TimeStamp = DateTime.Now;
            else this.TimeStamp = timeStamp;
            if (string.IsNullOrEmpty(message)) this.Message = "<no message>";
            else this.Message = message; 
            this.buildText();
        }
        private void buildText() {
            //this.Text = string.Empty;
            //this.Text = this.Sender.ToString();
            //if (!string.IsNullOrEmpty(this.SubSender.ToString())) this.Text += "." + this.SubSender.ToString();
            //this.Text += ": ";
            //this.Text += this.Message;
            this.Text = this.Message;
            this.ConsoleText = string.Format("### ERROR {0}", this.Text);
        }
    }

    public class ErrorListChangedEventArgs : EventArgs {
        public ErrorEventArgs[] ErrorList { get; private set; }
        public string[] ErrorTextList { get; private set; }
        public ErrorListChangedEventArgs(
            ErrorEventArgs[] errorList,
            string[] errorTextList) {
            this.ErrorList = errorList;
            this.ErrorTextList = errorTextList;
        }
    }

    public class MessageStatusChangedEventArgs : EventArgs {
        public ErrorStatus Status { get; private set; }
        public string LatestMessage { get; private set; }
        public MessageStatusChangedEventArgs(
            ErrorStatus status,
            string latestMessage) {
            this.Status = status;
            this.LatestMessage = latestMessage; 
        }
    }

    public class Message {

        public event EventHandler<Cliparts.Messaging.ErrorEventArgs> Error;
        protected void on_Error(object subSender, string errorMsg) { this.on_Error(this, new Messaging.ErrorEventArgs(this, subSender, DateTime.Now, errorMsg)); }
        protected void on_Error(object sender, Messaging.ErrorEventArgs e) {
            if (this.Error == null) Console.WriteLine(e.ConsoleText);
            else this.Error(this, e);
        }
    }

    public class Business {

        #region Properties

        public ErrorStatus Status { get; private set; }

        public string LatestMessage { get; private set; }

        public List<ErrorEventArgs> ErrorList { get; private set; }

        public List<string> ErrorTextList { get; private set; }

        private FormError errorForm;

        #endregion


        #region Funktionen

        public Business() {
            this.Status = ErrorStatus.NoMessage;
            this.LatestMessage = string.Empty;
            this.ErrorList = new List<ErrorEventArgs>();
            this.ErrorTextList = new List<string>();
        }

        public void Dispatch() {
        }

        public void AddErrorMessage(
            ErrorEventArgs error) {
            if (error != null) {
                this.LatestMessage = error.Message;
                this.Status = ErrorStatus.NewMessage;
                this.ErrorList.Add(error);
                this.ErrorTextList.Add(error.Text);
                this.on_ErrorListChanged(this, new ErrorListChangedEventArgs(this.ErrorList.ToArray(), this.ErrorTextList.ToArray()));
                this.on_StatusChanged(this, new MessageStatusChangedEventArgs(this.Status, this.LatestMessage));
            }
        }

        public void ClearErrorList() {
            this.ErrorList.Clear();
            this.ErrorTextList.Clear();
            this.Status = ErrorStatus.NoMessage;
            this.LatestMessage = string.Empty;
            this.on_ErrorListChanged(this, new ErrorListChangedEventArgs(this.ErrorList.ToArray(), this.ErrorTextList.ToArray()));
            this.on_StatusChanged(this, new MessageStatusChangedEventArgs(this.Status, this.LatestMessage));
        }

        public void ShowForm() {
            if (this.Status != ErrorStatus.NoMessage) {
                if (this.errorForm == null) this.errorForm = new FormError(this, this.ErrorList.ToArray(), this.ErrorTextList.ToArray(), this.Status);
                else if (this.errorForm.IsDisposed) this.errorForm = new FormError(this, this.ErrorList.ToArray(), this.ErrorTextList.ToArray(), this.Status);
                if (this.Status == ErrorStatus.NewMessage) {
                    this.Status = ErrorStatus.MessageAvailable;
                    this.on_StatusChanged(this, new MessageStatusChangedEventArgs(this.Status, this.LatestMessage));
                }
                this.errorForm.ShowDialog();
                this.errorForm = null;
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<ErrorListChangedEventArgs> ErrorListChanged;
        private void on_ErrorListChanged(object sender, ErrorListChangedEventArgs e) {
            if (this.ErrorListChanged != null) this.ErrorListChanged(sender, e);
        }

        public event EventHandler<MessageStatusChangedEventArgs> StatusChanged;
        private void on_StatusChanged(object sender, MessageStatusChangedEventArgs e) {
            if (this.StatusChanged != null) this.StatusChanged(sender, e);
        }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
