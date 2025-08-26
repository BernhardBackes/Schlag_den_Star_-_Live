using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CableTangleTimeToBeat {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        //private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.Games.BestRatedPuls.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimeToBeatPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionY.Maximum = int.MaxValue;

            this.comboBoxTimeToBeatStyle.BeginUpdate();
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TimeToBeat.Styles)));
            this.comboBoxTimeToBeatStyle.EndUpdate();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimeToBeatPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimeToBeatStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitAName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnitA.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitAStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnitA.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitBName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnitB.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitBStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnitB.DataBindings.Add(bind);

            string key;
            UserControlCableTangleTimeToBeatCable userControlCableTangleTimeToBeatCable;
            foreach (Cable cable in this.business.Cables) {
                key = "userControlCableTangleTimeToBeatCable_" + cable.Index.ToString("00");
                userControlCableTangleTimeToBeatCable = this.groupBoxBuzzer.Controls[key] as UserControlCableTangleTimeToBeatCable;
                if (userControlCableTangleTimeToBeatCable is UserControlCableTangleTimeToBeatCable) userControlCableTangleTimeToBeatCable.Pose(cable);
            }

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();
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

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();

            this.comboBoxIOUnitA.DataBindings.Clear();
            this.comboBoxIOUnitB.DataBindings.Clear();

            string key;
            UserControlCableTangleTimeToBeatCable userControlCableTangleTimeToBeatCable;
            foreach (Cable cable in this.business.Cables) {
                key = "userControlCableTangleTimeToBeatCable_" + cable.Index.ToString("00");
                userControlCableTangleTimeToBeatCable = this.Controls[key] as UserControlCableTangleTimeToBeatCable;
                if (userControlCableTangleTimeToBeatCable is UserControlCableTangleTimeToBeatCable) userControlCableTangleTimeToBeatCable.Dispose();
            }

        }

        private void fillIOUnitList() {
            this.comboBoxIOUnitA.BeginUpdate();
            this.comboBoxIOUnitA.Items.Clear();
            this.comboBoxIOUnitA.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnitA.EndUpdate();
            this.comboBoxIOUnitB.BeginUpdate();
            this.comboBoxIOUnitB.Items.Clear();
            this.comboBoxIOUnitB.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnitB.EndUpdate();
        }

        private void bind_comboBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
            string key;
            UserControlCableTangleTimeToBeatCable userControlCableTangleTimeToBeatCable;
            foreach (Cable cable in this.business.Cables) {
                key = "userControlCableTangleTimeToBeatCable_" + cable.Index.ToString("00");
                userControlCableTangleTimeToBeatCable = this.groupBoxBuzzer.Controls[key] as UserControlCableTangleTimeToBeatCable;
                if (userControlCableTangleTimeToBeatCable is UserControlCableTangleTimeToBeatCable) userControlCableTangleTimeToBeatCable.BackColor = this.BackColor;
            }
        }

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value; }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value; }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
        }

        protected virtual void comboBoxIOUnitA_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitAName = this.comboBoxIOUnitA.Text; }
        protected virtual void comboBoxIOUnitB_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitBName = this.comboBoxIOUnitB.Text; }

        #endregion
    }
}
