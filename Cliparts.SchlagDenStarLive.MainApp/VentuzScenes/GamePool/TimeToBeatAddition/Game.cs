using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatAddition
{
    public class Game : _Modules._InsertBase
    {
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= TwoRuns (the default value for this DataItem)
            [Elements]= TwoRuns (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        /*
        [Path]= .Run (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Run (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= FirstRun (the default value for this DataItem)
            [Elements]= FirstRun,SecondRun (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        /*
        [Path]= .SelectedTeam (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= SelectedTeam (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Top (the default value for this DataItem)
            [Elements]= Top,Bottom (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Timer.StopTime
        //	[Path]=.Timer.SentenceTime
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.Running
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.OnRunningChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.Top.Name
        //	[Path]=.Top.FirstRun
        //	[Path]=.Top.SecondRun
        //	[Path]=.Top.StartFlashing
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.FirstRun
        //	[Path]=.Bottom.SecondRun
        //	[Path]=.Bottom.StartFlashing

        public enum StyleElements { TwoRuns }
        public enum RunElements { FirstRun, SecondRun }
        public enum SelectedTeamElements { Top, Bottom }

        #region Properties

        private const string sceneID = "project/gamepool/timetobeataddition/game";

        private int stopTime = 600;
        public int StopTime
        {
            get { return this.stopTime; }
            private set
            {
                if (this.stopTime != value)
                {
                    this.stopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sentenceTime = 0;
        public int SentenceTime
        {
            get { return this.sentenceTime; }
            private set
            {
                if (this.sentenceTime != value)
                {
                    this.sentenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float? currentTime = null;
        public float CurrentTime
        {
            get
            {
                if (this.currentTime.HasValue) return this.currentTime.Value;
                else return 0;
            }
            private set
            {
                if (!this.currentTime.HasValue ||
                    this.currentTime.Value != value)
                {
                    this.currentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float? preciseTime = null;
        public float PreciseTime
        {
            get
            {
                if (this.preciseTime.HasValue) return this.preciseTime.Value;
                else return 0;
            }
            private set
            {
                if (!this.preciseTime.HasValue ||
                    this.preciseTime.Value != value)
                {
                    this.preciseTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? isRunning = null;
        public bool IsRunning
        {
            get
            {
                if (this.isRunning.HasValue) return this.isRunning.Value;
                else return false;
            }
            private set
            {
                if (!this.isRunning.HasValue ||
                    this.isRunning.Value != value)
                {
                    this.isRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }


        #endregion

        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID)
        {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID)
        {
        }

        public void ResetData()
        {
            this.CurrentTime = 0;
            this.PreciseTime = 0;
        }

        public void SetStyle(StyleElements value) { this.SetDataItemValue(".Style", value); }
        public void SetRun(RunElements value) { this.SetDataItemValue(".Run", value); }
        public void SetSelectedTeam(SelectedTeamElements value) { this.SetDataItemValue(".SelectedTeam", value); }


        public void SetStopTime(int value) { this.SetDataItemValue(".Timer.StopTime", value); }
        public void SetSentenceTime(int value) { this.SetDataItemValue(".Timer.SentenceTime", value); }
        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopFirstRun(float value) { this.SetDataItemValue(".Top.FirstRun", value); }
        public void SetTopSecondRun(float value) { this.SetDataItemValue(".Top.SecondRun", value); }
        public void StartTopFlashing() { this.SetDataItemTrigger(".Top.StartFlashing"); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomFirstRun(float value) { this.SetDataItemValue(".Bottom.FirstRun", value); }
        public void SetBottomSecondRun(float value) { this.SetDataItemValue(".Bottom.SecondRun", value); }
        public void StartBottomFlashing() { this.SetDataItemTrigger(".Bottom.StartFlashing"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.OnRunningChanged") this.IsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Timer.OnStop")
                {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.CurrentTime = time;
                    this.on_StopFired(this, new EventArgs());
                }
                else if (e.Path == ".Timer.OnGetPreciseTime")
                {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.PreciseTime = time;
                    this.on_PreciseTimeReceived(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e)
        {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs)
            {
                if (e.Path == ".Timer.StopTime") this.StopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.SentenceTime") this.SentenceTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.CurrentTime") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.Running") this.IsRunning = Convert.ToBoolean(e.Value);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            this.Reset();
        }

        public override void Clear()
        {
            base.Clear();
            this.ToOut();
        }

        #endregion

        #region Events.Outgoing

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        public event EventHandler PreciseTimeReceived;
        private void on_PreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.PreciseTimeReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
