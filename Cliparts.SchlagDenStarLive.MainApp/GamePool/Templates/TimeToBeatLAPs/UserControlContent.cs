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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatLAPs {

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

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownLAPsCount.Minimum = int.MinValue;
            this.numericUpDownLAPsCount.Maximum = int.MaxValue;

            this.numericUpDownSentenceTime.Minimum = int.MinValue;
            this.numericUpDownSentenceTime.Maximum = int.MaxValue;
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

            bind = new Binding("Value", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatStopTimeText.DataBindings.Add(bind);

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

            this.numericUpDownLAPsCount.DataBindings.Clear();

            this.numericUpDownSentenceTime.DataBindings.Clear();
            this.labelSentenceTimeText.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
        }

        protected virtual void numericUpDownTimeToBeatPositionX_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionX = (int)this.numericUpDownTimeToBeatPositionX.Value; }
        protected virtual void numericUpDownTimeToBeatPositionY_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatPositionY = (int)this.numericUpDownTimeToBeatPositionY.Value; }
        protected virtual void comboBoxTimeToBeatStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TimeToBeat.Styles style;
            if (Enum.TryParse(this.comboBoxTimeToBeatStyle.Text, out style)) this.business.TimeToBeatStyle = style;
        }
        protected virtual void numericUpDownTimeToBeatStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimeToBeatStopTime = (int)this.numericUpDownTimeToBeatStopTime.Value; }

        private void numericUpDownLAPsCount_ValueChanged(object sender, EventArgs e) { this.business.LAPsCount = (int)this.numericUpDownLAPsCount.Value; }

        private void numericUpDownSentenceTime_ValueChanged(object sender, EventArgs e) { this.business.SentenceTime = (int)this.numericUpDownSentenceTime.Value; }

        #endregion

    }
}
