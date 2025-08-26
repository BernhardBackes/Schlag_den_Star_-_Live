using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterInOutScore {

    public partial class UserControlContent : _Base.UserControlContent {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownCounterInOutScorePositionX.Minimum = int.MinValue;
            this.numericUpDownCounterInOutScorePositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterInOutScorePositionY.Minimum = int.MinValue;
            this.numericUpDownCounterInOutScorePositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterInOutScorePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterInOutScorePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterInOutScorePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterInOutScorePositionY.DataBindings.Add(bind);

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

            this.numericUpDownCounterInOutScorePositionX.DataBindings.Clear();
            this.numericUpDownCounterInOutScorePositionY.DataBindings.Clear();
        }

        protected virtual void setCounterInOutScorePreview() { }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        protected virtual void numericUpDownCounterInOutScorePositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterInOutScorePositionX = (int)this.numericUpDownCounterInOutScorePositionX.Value;
                this.setCounterInOutScorePreview();
            }
        }
        protected virtual void numericUpDownCounterInOutScorePositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterInOutScorePositionY = (int)this.numericUpDownCounterInOutScorePositionY.Value;
                this.setCounterInOutScorePreview();
            }
        }

        #endregion

    }
}
