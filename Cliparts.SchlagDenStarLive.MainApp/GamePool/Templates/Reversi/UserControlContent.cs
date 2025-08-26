using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Reversi;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Reversi {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownScorePointerPositionX.Minimum = int.MinValue;
            this.numericUpDownScorePointerPositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePointerPositionY.Minimum = int.MinValue;
            this.numericUpDownScorePointerPositionY.Maximum = int.MaxValue;

            this.comboBoxScorePointerStyle.BeginUpdate();
            this.comboBoxScorePointerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.ScorePointer.Styles)));
            this.comboBoxScorePointerStyle.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            foreach (Field field in this.business.FieldList) {
                if (field.Index == 0) this.userControlGamePoolTemplatesReversiDMX_00.Pose(this.business, field);
                else {
                    int row = (field.Index / Business.MatrixDimension);
                    int column = (field.Index % Business.MatrixDimension);
                    UserControlGamePoolTemplatesReversiDMX control = new UserControlGamePoolTemplatesReversiDMX();
                    control.Left = this.userControlGamePoolTemplatesReversiDMX_00.Left + column * (control.Width + control.Margin.Left + control.Margin.Right);
                    control.Top = this.userControlGamePoolTemplatesReversiDMX_00.Top + row * (control.Height + control.Margin.Top + control.Margin.Bottom);
                    control.Pose(this.business, field);
                    this.panelDMX.Controls.Add(control);
                }
            }

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePointerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePointerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePointerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePointerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ScorePointerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxScorePointerStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TabletClientHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLocalVentuzServerTablet.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "NotSelectedColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelColorNotSelected.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelColorLeftPlayer.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelColorRightPlayer.DataBindings.Add(bind);

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

            this.comboBoxIOUnit.DataBindings.Clear();

            this.numericUpDownScorePointerPositionX.DataBindings.Clear();
            this.numericUpDownScorePointerPositionY.DataBindings.Clear();
            this.comboBoxScorePointerStyle.DataBindings.Clear();

            foreach (Control control in this.panelDMX.Controls) control.Dispose();
            this.labelColorNotSelected.DataBindings.Clear();
            this.labelColorLeftPlayer.DataBindings.Clear();
            this.labelColorRightPlayer.DataBindings.Clear();
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

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
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

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }

        protected virtual void numericUpDownScorePointerPositionX_ValueChanged(object sender, EventArgs e) { if (this.business is Business) { this.business.ScorePointerPositionX = (int)this.numericUpDownScorePointerPositionX.Value; } }
        protected virtual void numericUpDownScorePointerPositionY_ValueChanged(object sender, EventArgs e) { if (this.business is Business) { this.business.ScorePointerPositionY = (int)this.numericUpDownScorePointerPositionY.Value; } }
        protected virtual void comboBoxScorePointerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.ScorePointer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxScorePointerStyle.Text, out style)) {
                this.business.ScorePointerStyle = style;
            }
        }

        private void textBoxLocalVentuzServerTablet_TextChanged(object sender, EventArgs e) { this.business.TabletClientHostname = this.textBoxLocalVentuzServerTablet.Text; }

        private void labelColorNotSelected_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.NotSelectedColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.NotSelectedColor = dialog.Color;
                    this.business.SetDMXValues();
                    break;
            }
            dialog = null;
            this.business.SetDMXValues();
        }

        private void labelColorLeftPlayer_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftPlayerColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftPlayerColor = dialog.Color;
                    this.business.SetDMXValues();
                    break;
            }
            dialog = null;
            this.business.SetDMXValues();
        }

        private void labelColorRightPlayer_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightPlayerColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightPlayerColor = dialog.Color;
                    this.business.SetDMXValues();
                    break;
            }
            dialog = null;
            this.business.SetDMXValues();
        }

        private void buttonSetDMX_Click(object sender, EventArgs e) { this.business.SetDMXValues(); }

        #endregion

    }

}
