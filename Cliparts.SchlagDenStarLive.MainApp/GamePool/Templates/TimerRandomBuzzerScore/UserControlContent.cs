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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerRandomBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerRandomBuzzerScore {

    public partial class UserControlContent : _Base.Timer.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTopBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownTopBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownLeftBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownBottomBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBottomBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownScorePositionX.Minimum = int.MinValue;
            this.numericUpDownScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePositionY.Minimum = int.MinValue;
            this.numericUpDownScorePositionY.Maximum = int.MaxValue;

            this.comboBoxScoreStyle.BeginUpdate();
            this.comboBoxScoreStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxScoreStyle.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TopBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTopBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BottomBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBottomBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ScoreStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxScoreStyle.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            this.fillIOUnitList();

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownTopBuzzerChannel.DataBindings.Clear();
            this.numericUpDownLeftBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightBuzzerChannel.DataBindings.Clear();
            this.numericUpDownBottomBuzzerChannel.DataBindings.Clear();

            this.numericUpDownScorePositionX.DataBindings.Clear();
            this.numericUpDownScorePositionY.DataBindings.Clear();
            this.comboBoxScoreStyle.DataBindings.Clear();

            this.labelOffColor.DataBindings.Clear();
            this.labelOnColor.DataBindings.Clear();

            this.business.PropertyChanged -= this.business_PropertyChanged;
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

        protected override void setTimerPreview() {
            base.setTimerPreview();
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_Timer.Set(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimerPreview();
            this.setScorePreview();
        }

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        private void numericUpDownTopBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TopBuzzerChannel = (int)this.numericUpDownTopBuzzerChannel.Value; }
        protected virtual void numericUpDownLeftBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftBuzzerChannel = (int)this.numericUpDownLeftBuzzerChannel.Value; }
        protected virtual void numericUpDownRightBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightBuzzerChannel = (int)this.numericUpDownRightBuzzerChannel.Value; }
        private void numericUpDownBottomBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.BottomBuzzerChannel = (int)this.numericUpDownBottomBuzzerChannel.Value; }

        protected virtual void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePositionX = (int)this.numericUpDownScorePositionX.Value;
                this.setScorePreview();
            }
        }
        protected virtual void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ScorePositionY = (int)this.numericUpDownScorePositionY.Value;
                this.setScorePreview();
            }
        }
        protected virtual void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxScoreStyle.Text, out style)) {
                this.business.ScoreStyle = style;
                this.setScorePreview();
            }
        }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelOnColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OnColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OnColor = dialog.Color;
                    break;
            }
        }

        #endregion
    }
}
