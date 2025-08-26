using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatLAPsCountScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatLAPsCountScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

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

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownLAPsCount.Minimum = int.MinValue;
            this.numericUpDownLAPsCount.Maximum = int.MaxValue;

            this.numericUpDownSentenceTime.Minimum = int.MinValue;
            this.numericUpDownSentenceTime.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

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

            bind = new Binding("Value", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "TakeLAPTime");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTakeLAPTime.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "StartAtZero");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxStartAtZero.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LAPsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLAPsCount.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSentenceTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelSentenceTimeText.DataBindings.Add(bind);

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
            this.numericUpDownTimeToBeatStopTime.DataBindings.Clear();
            this.labelTimeToBeatStopTimeText.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannel.DataBindings.Clear();

            this.checkBoxTakeLAPTime.DataBindings.Clear();
            this.checkBoxStartAtZero.DataBindings.Clear();
            this.numericUpDownLAPsCount.DataBindings.Clear();
            this.numericUpDownSentenceTime.DataBindings.Clear();
            this.labelSentenceTimeText.DataBindings.Clear();

        }

        private void fillIOUnitList() {
            this.comboBoxIOUnit.BeginUpdate();
            this.comboBoxIOUnit.Items.Clear();
            this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnit.EndUpdate();
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

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value; }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value; }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
        }
        protected virtual void numericUpDownTimeToBeatStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimeToBeatStopTime = (int)this.numericUpDownTimeToBeatStopTime.Value; }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.BuzzerChannel = (int)this.numericUpDownBuzzerChannel.Value; }

        private void checkBoxTakeLAPTime_CheckedChanged(object sender, EventArgs e) { this.business.TakeLAPTime = this.checkBoxTakeLAPTime.Checked; }
        private void checkBoxStartAtZero_CheckedChanged(object sender, EventArgs e) { this.business.StartAtZero = this.checkBoxStartAtZero.Checked; }
        private void numericUpDownLAPsCount_ValueChanged(object sender, EventArgs e) { this.business.LAPsCount = (int)this.numericUpDownLAPsCount.Value; }
        private void numericUpDownSentenceTime_ValueChanged(object sender, EventArgs e) { this.business.SentenceTime = (int)this.numericUpDownSentenceTime.Value; }

        #endregion
    }

}
