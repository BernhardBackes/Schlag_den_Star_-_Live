using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Fencing {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownScorePositionX.Minimum = int.MinValue;
            this.numericUpDownScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownScorePositionY.Minimum = int.MinValue;
            this.numericUpDownScorePositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "ScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownScorePositionY.DataBindings.Add(bind);

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

            this.numericUpDownScorePositionX.DataBindings.Clear();
            this.numericUpDownScorePositionY.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) { this.business.ScorePositionX = (int)this.numericUpDownScorePositionX.Value; }
        protected virtual void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) { this.business.ScorePositionY = (int)this.numericUpDownScorePositionY.Value; }

        #endregion

    }
}
