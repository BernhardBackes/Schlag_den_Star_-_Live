using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DoubleStopClockScore {

    public class DoubleStopClock : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.Stop
        //	[Path]=.StopClockLeft.Enable
        //	[Path]=.StopClockLeft.Reset
        //	[Path]=.StopClockLeft.Stop
        //	[Path]=.StopClockLeft.IsWinner
        //	[Path]=.StopClockLeft.IsWinnerChanged
        //	[Path]=.StopClockLeft.LeftBuzzer.Down
        //	[Path]=.StopClockLeft.LeftBuzzer.Up
        //	[Path]=.StopClockLeft.LeftBuzzer.IsDown
        //	[Path]=.StopClockLeft.LeftBuzzer.IsDownChanged
        //	[Path]=.StopClockLeft.RightBuzzer.Down
        //	[Path]=.StopClockLeft.RightBuzzer.Up
        //	[Path]=.StopClockLeft.RightBuzzer.IsDown
        //	[Path]=.StopClockLeft.RightBuzzer.IsDownChanged
        //	[Path]=.StopClockRight.Enable
        //	[Path]=.StopClockRight.Reset
        //	[Path]=.StopClockRight.Stop
        //	[Path]=.StopClockRight.IsWinner
        //	[Path]=.StopClockRight.IsWinnerChanged
        //	[Path]=.StopClockRight.LeftBuzzer.Down
        //	[Path]=.StopClockRight.LeftBuzzer.Up
        //	[Path]=.StopClockRight.LeftBuzzer.IsDown
        //	[Path]=.StopClockRight.LeftBuzzer.IsDownChanged
        //	[Path]=.StopClockRight.RightBuzzer.Down
        //	[Path]=.StopClockRight.RightBuzzer.Up
        //	[Path]=.StopClockRight.RightBuzzer.IsDown
        //	[Path]=.StopClockRight.RightBuzzer.IsDownChanged
        
        #region Properties

        private const string sceneID = "project/gamepool/doublestopclockscore/doublestopclock";

        private bool leftClockIsWinner = false;
        public bool LeftClockIsWinner {
            get { return this.leftClockIsWinner; }
            set {
                if (this.leftClockIsWinner != value) {
                    this.leftClockIsWinner = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftClockLeftBuzzerIsDown = false;
        public bool LeftClockLeftBuzzerIsDown {
            get { return this.leftClockLeftBuzzerIsDown; }
            set {
                if (this.leftClockLeftBuzzerIsDown != value) {
                    this.leftClockLeftBuzzerIsDown = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftClockRightBuzzerIsDown = false;
        public bool LeftClockRightBuzzerIsDown {
            get { return this.leftClockRightBuzzerIsDown; }
            set {
                if (this.leftClockRightBuzzerIsDown != value) {
                    this.leftClockRightBuzzerIsDown = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightClockIsWinner = false;
        public bool RightClockIsWinner {
            get { return this.rightClockIsWinner; }
            set {
                if (this.rightClockIsWinner != value) {
                    this.rightClockIsWinner = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightClockLeftBuzzerIsDown = false;
        public bool RightClockLeftBuzzerIsDown {
            get { return this.rightClockLeftBuzzerIsDown; }
            set {
                if (this.rightClockLeftBuzzerIsDown != value) {
                    this.rightClockLeftBuzzerIsDown = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightClockRightBuzzerIsDown = false;
        public bool RightClockRightBuzzerIsDown {
            get { return this.rightClockRightBuzzerIsDown; }
            set {
                if (this.rightClockRightBuzzerIsDown != value) {
                    this.rightClockRightBuzzerIsDown = value;
                    this.on_PropertyChanged();
                }
            }
        }


        #endregion


        #region Funktionen

        public DoubleStopClock(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void Stop() { this.SetDataItemTrigger(".Stop"); }

        public void EnableLeftClock() { this.SetDataItemTrigger(".StopClockLeft.Enable"); }
        public void ResetLeftClock() { this.SetDataItemTrigger(".StopClockLeft.Reset"); }
        public void StopLeftClock() { this.SetDataItemTrigger(".StopClockLeft.Stop"); }

        public void EnableRightClock() { this.SetDataItemTrigger(".StopClockRight.Enable"); }
        public void ResetRightClock() { this.SetDataItemTrigger(".StopClockRight.Reset"); }
        public void StopRightClock() { this.SetDataItemTrigger(".StopClockRight.Stop"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".StopClockLeft.IsWinner") this.LeftClockIsWinner = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockLeft.LeftBuzzer.IsDown") this.LeftClockLeftBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockLeft.RightBuzzer.IsDown") this.LeftClockRightBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.IsWinner") this.RightClockIsWinner = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.LeftBuzzer.IsDown") this.RightClockLeftBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.RightBuzzer.IsDown") this.RightClockRightBuzzerIsDown = e.Value.ToString() == "1";
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".StopClockLeft.IsWinnerChanged") this.LeftClockIsWinner = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockLeft.LeftBuzzer.IsDownChanged") this.LeftClockLeftBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockLeft.RightBuzzer.IsDownChanged") this.LeftClockRightBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.IsWinnerChanged") this.RightClockIsWinner = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.LeftBuzzer.IsDownChanged") this.RightClockLeftBuzzerIsDown = e.Value.ToString() == "1";
                else if (e.Path == ".StopClockRight.RightBuzzer.IsDownChanged") this.RightClockRightBuzzerIsDown = e.Value.ToString() == "1";
            }
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
