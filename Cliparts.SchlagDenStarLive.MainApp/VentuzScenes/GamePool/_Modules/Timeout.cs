using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Timeout : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Timer.Visible
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.StartLeft
        //	[Path]=.Timer.BuzzerLeft
        //	[Path]=.Timer.StartRight
        //	[Path]=.Timer.BuzzerRight
        //	[Path]=.Timer.StartCenter
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.LeftRightToGreen
        //	[Path]=.Timer.CenterToGreen
        //	[Path]=.BuzzerSoundLeft
        //	[Path]=.BuzzerSoundRight

        public enum Duration { FiveSeconds, TenSeconds }

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/timeout";

        #endregion


        #region Funktionen

        public Timeout(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public Timeout(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void Reset() { this.SetDataItemTrigger(".Timer.Reset"); }

        public void SetIsVisible(bool value) { this.SetDataItemValue(".Timer.Visible", value); }

        public void BuzzerLeft(
            Duration duration,
            bool visible) {
            this.SetDataItemValue(".Timer.Visible", visible);
            this.SetDataItemTrigger(".Timer.BuzzerLeft", getDuration(duration));
        }
        public void StartLeft(
            Duration duration,
            bool visible) {
            this.SetDataItemValue(".Timer.Visible", visible);
            this.SetDataItemTrigger(".Timer.StartLeft", getDuration(duration));
        }

        public void StartCenter(
            Duration duration,
            bool visible) {
            this.SetDataItemValue(".Timer.Visible", visible);
            this.SetDataItemTrigger(".Timer.StartCenter", getDuration(duration));
        }

        public void BuzzerRight(
            Duration duration,
            bool visible) {
            this.SetDataItemValue(".Timer.Visible", visible);
            this.SetDataItemTrigger(".Timer.BuzzerRight", getDuration(duration));
        }
        public void StartRight(
            Duration duration,
            bool visible) {
            this.SetDataItemValue(".Timer.Visible", visible);
            this.SetDataItemTrigger(".Timer.StartRight", getDuration(duration));
        }

        public void Stop() { this.SetDataItemTrigger(".Timer.Stop"); }

        public void SetLeftRightToGreen(
            Duration duration) {
            this.SetDataItemTrigger(".Timer.LeftRightToGreen", getDuration(duration));
        }
        public void SetCenterToGreen(
            Duration duration) {
            this.SetDataItemTrigger(".Timer.CenterToGreen", getDuration(duration));
        }

        public void BuzzerSoundLeft() { this.SetDataItemTrigger(".BuzzerSoundLeft"); }
        public void BuzzerSoundRight() { this.SetDataItemTrigger(".BuzzerSoundRight"); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

        public override void Clear() {
            base.Clear();
            this.Stop();
        }

        private static int getDuration(
            Duration value) {
            int result = 5;
            switch (value) {
                case Duration.TenSeconds:
                    result = 10;
                    break;
            }
            return result;
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion
    }
    }
