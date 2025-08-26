using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard {

    public partial class UserControlGameboard : UserControl {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGameboard() { 
            InitializeComponent();

            this.numericUpDownPiechartGameID.Minimum = int.MinValue;
            this.numericUpDownPiechartGameID.Maximum = Business.GamesCount;
        }

        public void Pose(
            Business business) {

            this.BackColor = this.Parent.BackColor;

            this.business = business;

            if (this.business is Business) {

                Binding bind;

                bind = new Binding("Text", this.business, "LeftPlayerName");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxLeftPlayerName.DataBindings.Add(bind);

                bind = new Binding("Text", this.business, "LeftPlayerTotalScore");
                bind.Format += (s, e) => { e.Value = ((ushort)e.Value).ToString("00"); };
                this.labelLeftPlayerTotalScore.DataBindings.Add(bind);

                bind = new Binding("Text", this.business, "RightPlayerName");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxRightPlayerName.DataBindings.Add(bind);

                bind = new Binding("Text", this.business, "RightPlayerTotalScore");
                bind.Format += (s, e) => { e.Value = ((ushort)e.Value).ToString("00"); };
                this.labelRightPlayerTotalScore.DataBindings.Add(bind);

                int index = 0;
                foreach (Level item in this.business.LevelList) {
                    if (index == 0) this.userControlGameboardLevel_00.Pose(item);
                    else {
                        UserControlGameboardLevel userControlGameboardLevel = new UserControlGameboardLevel();
                        userControlGameboardLevel.Name = "userControlGameboardLevel_" + index.ToString("00");
                        userControlGameboardLevel.Left = this.userControlGameboardLevel_00.Left;
                        userControlGameboardLevel.Top = this.userControlGameboardLevel_00.Top + this.userControlGameboardLevel_00.Height * index;
                        this.Controls.Add(userControlGameboardLevel);
                        userControlGameboardLevel.Pose(item);
                    }
                    index++;
                }

                bind = new Binding("Value", this.business, "SelectedLevelIndex");
                bind.DataSourceUpdateMode = DataSourceUpdateMode.Never;
                bind.Format += (s, e) => { e.Value = (int)e.Value + 1; };
                this.numericUpDownPiechartGameID.DataBindings.Add(bind);

                this.business.PropertyChanged += this.business_PropertyChanged;
            }

        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);

            if (this.business is Business) this.business.PropertyChanged -= this.business_PropertyChanged;

            this.textBoxLeftPlayerName.DataBindings.Clear();
            this.labelLeftPlayerTotalScore.DataBindings.Clear();
            this.textBoxRightPlayerName.DataBindings.Clear();
            this.labelRightPlayerTotalScore.DataBindings.Clear();
            this.numericUpDownPiechartGameID.DataBindings.Clear();

            foreach (Control control in this.Controls) if (control is UserControlGameboardLevel) control.Dispose();
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {}
        }

        #endregion

        #region Events.Controls

        private void textBoxLeftPlayerName_TextChanged(object sender, EventArgs e) { this.business.LeftPlayerName = this.textBoxLeftPlayerName.Text; }
        private void textBoxRightPlayerName_TextChanged(object sender, EventArgs e) { this.business.RightPlayerName = this.textBoxRightPlayerName.Text; }

        private void buttonSet_Click(object sender, EventArgs e) { this.business.SetGraphic(); }
        private void buttonShow_Click(object sender, EventArgs e) { this.business.ShowGraphic(); }
        private void buttonUpdate_Click(object sender, EventArgs e) { this.business.UpdateGraphic(); }

        private void buttonShowPiechart_Click(object sender, EventArgs e) { this.business.ShowPiechart((int)this.numericUpDownPiechartGameID.Value); }

        #endregion


    }
}
