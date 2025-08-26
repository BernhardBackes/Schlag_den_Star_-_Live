using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes {

    public partial class UserControlVentuzScenesMediaPlayer : UserControl {

        #region Properties

        MediaPlayer mediaPlayer;

        #endregion


        #region Funktionen

        public UserControlVentuzScenesMediaPlayer() { 
            InitializeComponent();

            this.numericUpDownPositionX.Minimum = int.MinValue;
            this.numericUpDownPositionX.Maximum = int.MaxValue;
            this.numericUpDownPositionX.Increment = 1;

            this.numericUpDownPositionY.Minimum = int.MinValue;
            this.numericUpDownPositionY.Maximum = int.MaxValue;
            this.numericUpDownPositionY.Increment = 1;

            this.numericUpDownScaling.Minimum = decimal.MinValue;
            this.numericUpDownScaling.Maximum = decimal.MaxValue;
            this.numericUpDownScaling.Increment = 0.1m;

            this.numericUpDownFaderDuration.Minimum = decimal.MinValue;
            this.numericUpDownFaderDuration.Maximum = decimal.MaxValue;
            this.numericUpDownFaderDuration.Increment = 0.1m;

            this.trackBarVolume.Minimum = MediaPlayer.VolumeMin;
            this.trackBarVolume.Maximum = MediaPlayer.VolumeMax;
            this.trackBarVolume.TickFrequency = MediaPlayer.VolumeMax / 10;
        }

        public void Pose(
            MediaPlayer mediaPlayer) {

            this.mediaPlayer = mediaPlayer;
            this.mediaPlayer.PropertyChanged += this.mediaPlayer_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.mediaPlayer, "Filename");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxFilename.DataBindings.Add(bind);

            bind = new Binding("Value", this.mediaPlayer, "PositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.mediaPlayer, "PositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.mediaPlayer, "ScalingFactor");
            bind.Format += (s, e) => { e.Value = (double)e.Value; };
            this.numericUpDownScaling.DataBindings.Add(bind);

            bind = new Binding("Value", this.mediaPlayer, "FadeDuration");
            bind.Format += (s, e) => { e.Value = (double)e.Value; };
            this.numericUpDownFaderDuration.DataBindings.Add(bind);

            bind = new Binding("Image", this.mediaPlayer, "OnAir");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Properties.Resources.onair : Properties.Resources.offair; };
            this.pictureBoxOnAir.DataBindings.Add(bind);

            bind = new Binding("Checked", this.mediaPlayer, "Loop");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxLoop.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.mediaPlayer, "Loop");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.checkBoxLoop.DataBindings.Add(bind);

            bind = new Binding("Value", this.mediaPlayer, "Volume");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.trackBarVolume.DataBindings.Add(bind);
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

            this.mediaPlayer.PropertyChanged -= this.mediaPlayer_PropertyChanged;

            this.textBoxFilename.DataBindings.Clear();
            this.numericUpDownPositionX.DataBindings.Clear();
            this.numericUpDownPositionY.DataBindings.Clear();
            this.numericUpDownScaling.DataBindings.Clear();
            this.numericUpDownFaderDuration.DataBindings.Clear();
            this.pictureBoxOnAir.DataBindings.Clear();
            this.checkBoxLoop.DataBindings.Clear();
            this.trackBarVolume.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming

        void mediaPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.mediaPlayer_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private void buttonSelectFile_Click(object sender, EventArgs e) { this.mediaPlayer.SetFilename(Helper.selectMediaFile("Select Media File", this.mediaPlayer.Filename)); }
        private void numericUpDownPositionX_ValueChanged(object sender, EventArgs e) { this.mediaPlayer.SetPositionX((int)this.numericUpDownPositionX.Value); }
        private void numericUpDownPositionY_ValueChanged(object sender, EventArgs e) { this.mediaPlayer.SetPositionY((int)this.numericUpDownPositionY.Value); }
        private void numericUpDownScaling_ValueChanged(object sender, EventArgs e) { this.mediaPlayer.SetScalingFactor((double)this.numericUpDownScaling.Value); }
        private void buttonSet_Click(object sender, EventArgs e) { this.mediaPlayer.Set(); }
        private void buttonClear_Click(object sender, EventArgs e) { this.mediaPlayer.Clear(); }
        private void numericUpDownFaderDuration_ValueChanged(object sender, EventArgs e) { this.mediaPlayer.SetFadeDuration((double)this.numericUpDownFaderDuration.Value); }
        private void buttonPlay_Click(object sender, EventArgs e) { this.mediaPlayer.Play(); }
        private void buttonStop_Click(object sender, EventArgs e) { this.mediaPlayer.Stop(); }
        private void buttonContinue_Click(object sender, EventArgs e) { this.mediaPlayer.Continue(); }
        private void checkBoxMediaPlayerLoop_CheckedChanged(object sender, EventArgs e) { this.mediaPlayer.SetLoop(this.checkBoxLoop.Checked); }
        private void trackBarVolume_Scroll(object sender, EventArgs e) { this.mediaPlayer.SetVolume(this.trackBarVolume.Value); }
        private void buttonReset_Click(object sender, EventArgs e) { this.mediaPlayer.Reset(); }
        #endregion

    }
}
