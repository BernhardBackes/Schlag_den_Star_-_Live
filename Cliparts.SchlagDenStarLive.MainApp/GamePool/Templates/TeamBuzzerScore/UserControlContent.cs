using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamBuzzerScore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TeamBuzzerScore
{

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayer2ndBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayer2ndBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayer2ndBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayer2ndBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayer2ndDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayer2ndDMXStartchannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayer2ndDMXStartchannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayer2ndDMXStartchannel.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);
            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayer2ndBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayer2ndBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayer2ndBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayer2ndBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayer2ndDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayer2ndDMXStartchannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayer2ndDMXStartchannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayer2ndDMXStartchannel.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayer2ndBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayer2ndBuzzerChannel.DataBindings.Clear();

            this.numericUpDownLeftPlayer2ndDMXStartchannel.DataBindings.Clear();
            this.numericUpDownRightPlayer2ndDMXStartchannel.DataBindings.Clear();
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

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownLeftPlayer2ndBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayer2ndBuzzerChannel = (int)this.numericUpDownLeftPlayer2ndBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayer2ndBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayer2ndBuzzerChannel = (int)this.numericUpDownRightPlayer2ndBuzzerChannel.Value; }

        private void numericUpDownLeftPlayer2ndDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayer2ndDMXStartchannel = (int)this.numericUpDownLeftPlayer2ndDMXStartchannel.Value; }
        private void buttonLeftPlayer2ndOn_Click(object sender, EventArgs e) { this.business.SetLeftPlayer2ndOn(); }
        private void buttonLeftPlayer2ndOff_Click(object sender, EventArgs e) { this.business.SetLeftPlayer2ndOff(); }

        private void numericUpDownRightPlayer2ndDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayer2ndDMXStartchannel = (int)this.numericUpDownRightPlayer2ndDMXStartchannel.Value; }
        private void buttonRightPlayer2ndOn_Click(object sender, EventArgs e) { this.business.SetRightPlayer2ndOn(); }
        private void buttonRightPlayer2ndOff_Click(object sender, EventArgs e) { this.business.SetRightPlayer2ndOff(); }

        #endregion
    }
}
