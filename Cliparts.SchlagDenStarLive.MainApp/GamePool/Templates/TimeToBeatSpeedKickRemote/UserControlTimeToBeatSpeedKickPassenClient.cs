using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.TCPCom;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKickRemote {

    public partial class UserControlTimeToBeatSpeedKickRemotePassenClient : UserControl {

        #region Properties

        PassenClient client;

        #endregion


        #region Funktionen

        public UserControlTimeToBeatSpeedKickRemotePassenClient() {
            InitializeComponent();
        }

        public void Pose(
            PassenClient client) {

            this.client = client;
            this.client.PropertyChanged += client_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.client, "HostName");
            this.textBoxHostName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.client, "ConnectionStatus");
            bind.Format += this.bind_textBoxMeasurementHostNameTeam_BackColor;
            this.textBoxHostName.DataBindings.Add(bind);

            this.fill_listBoxCourses(this.client.CourseNameList);
            this.listBoxCourses.Text = this.client.SelectedCourse;
            this.setProgress(this.client.CourseStepIndex, this.client.SelectedCourseLength);
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.textBoxHostName.DataBindings.Clear();
        }

        private void fill_listBoxCourses(
            string[] courseNameList) {
            this.listBoxCourses.BeginUpdate();
            this.listBoxCourses.Items.Clear();
            if (courseNameList is string[] &&
                courseNameList.Length > 0) this.listBoxCourses.Items.AddRange(courseNameList);
            this.listBoxCourses.EndUpdate();
        }

        private void setProgress(
            int courseStepIndex,
            int selectedCourseLength) {
            this.labelProgress.Text = string.Format("step {0} of {1}", courseStepIndex.ToString("00"), selectedCourseLength.ToString("00"));
        }

        void bind_textBoxMeasurementHostNameTeam_BackColor(object sender, ConvertEventArgs e) {
            ConnectionStates state = (ConnectionStates)e.Value;
            switch (state) {
                case ConnectionStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case ConnectionStates.Connected:
                    e.Value = Constants.ColorEnabled;
                    break;
                case ConnectionStates.Disconnecting:
                    e.Value = Constants.ColorDisabling;
                    break;
                case ConnectionStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case ConnectionStates.Idle:
                default:
                    e.Value = SystemColors.Control;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void client_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.client_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "CourseNameList") this.fill_listBoxCourses(this.client.CourseNameList);
                else if (e.PropertyName == "SelectedCourse") this.listBoxCourses.Text = this.client.SelectedCourse;
                else if (e.PropertyName == "SelectedCourseLength") this.setProgress(this.client.CourseStepIndex, this.client.SelectedCourseLength);
                else if (e.PropertyName == "CourseStepIndex") this.setProgress(this.client.CourseStepIndex, this.client.SelectedCourseLength);
            }
        }

        #endregion

        #region Events.Controls

        private void buttonConnect_Click(object sender, EventArgs e) { this.client.Connect(); }
        private void buttonDisconnect_Click(object sender, EventArgs e) { this.client.Disconnect(); }
        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e) { this.client.SelectCourse(this.listBoxCourses.Text); }
        private void buttonStartCourse_Click(object sender, EventArgs e) { this.client.StartCourse(); }
        private void buttonStopCourse_Click(object sender, EventArgs e) { this.client.StopCourse(); }
        private void buttonResetCourse_Click(object sender, EventArgs e) { this.client.ResetCourse(); }

        #endregion

    }
}
