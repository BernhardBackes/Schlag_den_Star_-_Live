using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WeightLifting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WeightLifting {

    public partial class UserControlContent : _Base.Timer.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownMarkerSetsPositionX.Minimum = int.MinValue;
            this.numericUpDownMarkerSetsPositionX.Maximum = int.MaxValue;

            this.numericUpDownMarkerSetsPositionY.Minimum = int.MinValue;
            this.numericUpDownMarkerSetsPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "MarkerSetsPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownMarkerSetsPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "MarkerSetsPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownMarkerSetsPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "MarkerBeginning");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownMarkerBeginning.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "MarkerExpanse");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownMarkerExpanse.DataBindings.Add(bind);

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

            this.numericUpDownMarkerSetsPositionX.DataBindings.Clear();
            this.numericUpDownMarkerSetsPositionY.DataBindings.Clear();
            this.numericUpDownMarkerBeginning.DataBindings.Clear();
            this.numericUpDownMarkerExpanse.DataBindings.Clear();
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
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_Timer.Set(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected virtual void setMarkerSetsPreview() {
            if (this.previewSceneIsAvailable) {
                List<MarkerSet> markerList = new List<MarkerSet>();
                for (int i = 0; i < Insert.MarkersCountMax; i++) markerList.Add(new MarkerSet(this.business.MarkerBeginning + i * this.business.MarkerExpanse));
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetMarkerSets(
                    previewScene.Insert, 
                    this.business.MarkerSetsPositionX, 
                    this.business.MarkerSetsPositionY, 
                    this.business.LeftPlayerName, 
                    this.business.RightPlayerName,
                    Insert.MarkersCountMax - 1, 
                    markerList.ToArray());
                previewScene.Insert.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimerPreview();
            this.setMarkerSetsPreview();
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownDecimalSetsPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.MarkerSetsPositionX = (int)this.numericUpDownMarkerSetsPositionX.Value;
                this.setMarkerSetsPreview();
            }
        }
        protected virtual void numericUpDownDecimalSetsPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.MarkerSetsPositionY = (int)this.numericUpDownMarkerSetsPositionY.Value;
                this.setMarkerSetsPreview();
            }
        }

        private void numericUpDownMarkerBeginning_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) this.business.MarkerBeginning = (int)this.numericUpDownMarkerBeginning.Value;
            this.setMarkerSetsPreview();
        }

        private void numericUpDownMarkerExpanse_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) this.business.MarkerExpanse = (int)this.numericUpDownMarkerExpanse.Value;
            this.setMarkerSetsPreview();
        }
        
        #endregion

    }
}
