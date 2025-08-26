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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SingleCounterOfSoloScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SingleCounterOfSoloScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool _showCounterInsert = false;
        public bool showCounterInsert {
            get { return this._showCounterInsert; }
            set {
                if (this._showCounterInsert != value) {
                    this._showCounterInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownCounterTarget.Minimum = int.MinValue;
            this.numericUpDownCounterTarget.Maximum = int.MaxValue;

            this.numericUpDownBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannel.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Target");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterTarget.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Title");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxCounterTitle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannel.DataBindings.Add(bind);

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannel.DataBindings.Clear();

            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
            this.numericUpDownCounterTarget.DataBindings.Clear();
            this.textBoxCounterTitle.DataBindings.Clear();
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
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((Preview)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Preview(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        private void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showCounterInsert;
            this.radioButtonSourceCounter.Checked = this.showCounterInsert;
            this.setScorePreview();
            this.setCounterPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showCounterInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
            }
        }

        protected void setCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetCounter(previewScene.Insert.SingleCounterOf, 2);
                if (this.showCounterInsert) previewScene.Insert.SingleCounterOf.SetIn();
                else previewScene.Insert.SingleCounterOf.SetOut();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.BuzzerChannel = (int)this.numericUpDownBuzzerChannel.Value; }

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value;
                this.setCounterPreview();
            }
        }
        private void textBoxCounterTitle_TextChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.Title = this.textBoxCounterTitle.Text;
                this.setCounterPreview();
            }
        }
        private void numericUpDownCounterTarget_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.Target = (int)this.numericUpDownCounterTarget.Value;
                this.setCounterPreview();
            }
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceCounter_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = this.radioButtonSourceCounter.Checked; }

        #endregion

    }
}
