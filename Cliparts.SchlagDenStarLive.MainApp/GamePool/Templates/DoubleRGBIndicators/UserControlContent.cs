using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DoubleRGBIndicators;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownIndicatorsCount.Minimum = int.MinValue;
            this.numericUpDownIndicatorsCount.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "IndicatorsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownIndicatorsCount.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightColor");
            bind.Format += this.bind_colorLabel_ForeColor;
            this.labelRightColor.DataBindings.Add(bind);

            for (int i = 0; i < Business.IndicatorsCountMax; i++) {
                TabPage tabPageIndicator;
                UserControlIndicatorSettings userControlIndicatorSettings;
                if (i == 0) {
                    tabPageIndicator = this.tabControlIndicators.TabPages[0];
                    userControlIndicatorSettings = this.userControlIndicatorSettings_00;
                }
                else {
                    tabPageIndicator = new TabPage(string.Format("#{0}", this.business.Indicators[i].ID.ToString()));
                    tabPageIndicator.Name = "tabPageIndicator_" + i.ToString("00");
                    this.tabControlIndicators.TabPages.Add(tabPageIndicator);
                    userControlIndicatorSettings = new UserControlIndicatorSettings();
                    userControlIndicatorSettings.Left = userControlIndicatorSettings_00.Left;
                    userControlIndicatorSettings.Top = this.userControlIndicatorSettings_00.Top;
                    userControlIndicatorSettings.Size = this.userControlIndicatorSettings_00.Size;
                    userControlIndicatorSettings.Name = "userControlIndicatorSettings_" + i.ToString("00");
                    tabPageIndicator.Controls.Add(userControlIndicatorSettings);
                }
                tabPageIndicator.Text = string.Format("#{0}", this.business.Indicators[i].ID.ToString());
                userControlIndicatorSettings.BackColor = this.BackColor;
                userControlIndicatorSettings.Pose(this.business, this.business.Indicators[i]);
            }

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

            this.numericUpDownIndicatorsCount.DataBindings.Clear();
            this.labelOffColor.DataBindings.Clear();
            this.labelLeftColor.DataBindings.Clear();
            this.labelRightColor.DataBindings.Clear();

        }

        private void bind_colorLabel_ForeColor(object sender, ConvertEventArgs e) {
            Color value = (Color)e.Value;
            Color result = Color.FromArgb(255 - value.R, 255 - value.G, 255 - value.B);
            e.Value = result;
        }

        private void allLightsOn() {
            foreach (Indicator item in this.business.Indicators) {
                this.business.SetLeftColor(item.LeftPlayerStartAddress);
                this.business.SetRightColor(item.RightPlayerStartAddress);
            }
        }

        private void allLightsOff() {
            foreach (Indicator item in this.business.Indicators) {
                this.business.SetOffColor(item.LeftPlayerStartAddress);
                this.business.SetOffColor(item.RightPlayerStartAddress);
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

        protected override void setScorePreview() {
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
            this.setScorePreview();
        }

        protected void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
            foreach (TabPage tabPage in this.tabControlIndicators.TabPages) {
                foreach(Control control in tabPage.Controls) control.BackColor = this.BackColor;
            }
        }

        private void numericUpDownIndicatorsCount_ValueChanged(object sender, EventArgs e) { this.business.IndicatorsCount = (int)this.numericUpDownIndicatorsCount.Value; }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelLeftColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftColor = dialog.Color;
                    break;
            }
        }

        private void labelRightColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightColor = dialog.Color;
                    break;
            }
        }

        private void buttonAllOn_Click(object sender, EventArgs e) { this.allLightsOn(); }
        private void buttonAllOff_Click(object sender, EventArgs e) { this.allLightsOff(); }
        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }
}
