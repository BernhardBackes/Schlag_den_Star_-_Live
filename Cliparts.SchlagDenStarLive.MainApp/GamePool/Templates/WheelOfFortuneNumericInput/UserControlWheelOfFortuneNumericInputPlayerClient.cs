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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneNumericInput {

    public partial class UserControlWheelOfFortuneNumericInputPlayerClient : UserControl {

        #region Properties

        PlayerClient client;

        #endregion


        #region Funktionen

        public UserControlWheelOfFortuneNumericInputPlayerClient() {
            InitializeComponent();
        }

        public void Pose(
            string name,
            PlayerClient client) {

            this.labelName.Text = name;

            this.client = client;
            this.client.PropertyChanged += client_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.client, "HostName");
            this.textBoxHostName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.client, "ConnectionStatus");
            bind.Format += this.bind_textBoxMeasurementHostNameTeam_BackColor;
            this.textBoxHostName.DataBindings.Add(bind);

            bind = new Binding("Text", this.client, "PlayerValue");
            bind.Format += (s, e) => { e.Value = ((int?)e.Value).HasValue ? ((int?)e.Value).Value.ToString() : string.Empty; };
            this.textBoxPlayerValue.DataBindings.Add(bind);
        }

        void client_PropertyChanged(object sender, PropertyChangedEventArgs e) {
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
            this.textBoxPlayerValue.DataBindings.Clear();
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
        #endregion
        
        #region Events.Controls

        private void buttonConnect_Click(object sender, EventArgs e) { this.client.Connect(); }
        private void buttonDisconnect_Click(object sender, EventArgs e) { this.client.Disconnect(); }
        private void buttonSetIdle_Click(object sender, EventArgs e) { this.client.SetIdle(); }
        private void buttonSetActive_Click(object sender, EventArgs e) { this.client.SetActive(); }
        private void buttonSetLocked_Click(object sender, EventArgs e) { this.client.SetLocked(); }
        private void buttonSetUnlocked_Click(object sender, EventArgs e) { this.client.SetUnlocked(); }

        #endregion

    }
}
