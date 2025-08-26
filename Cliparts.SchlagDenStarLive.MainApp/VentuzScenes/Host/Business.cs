using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Host {

    public class Business : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.MainFader.Reset
        //	[Path]=.MainFader.In
        //	[Path]=.MainFader.Out
        //	[Path]=.Displays.StartIdentify
        //	[Path]=.Displays.ToLogo
        //	[Path]=.Displays.ToNeutral
        //	[Path]=.LeftPlayer.Name
        //	[Path]=.LeftPlayer.TotalScore
        //	[Path]=.LeftPlayer.GameScore
        //	[Path]=.LeftPlayer.Display.Reset
        //	[Path]=.LeftPlayer.Display.ToNeutral
        //	[Path]=.LeftPlayer.Display.ToLogo
        //	[Path]=.LeftPlayer.Display.ToRed
        //	[Path]=.LeftPlayer.Display.ToGreen
        //	[Path]=.LeftPlayer.Display.StartIdentify
        //	[Path]=.LeftPlayer.Display.Text.Value
        //	[Path]=.LeftPlayer.Display.Text.In
        //	[Path]=.LeftPlayer.Display.Text.Reset
        //	[Path]=.LeftPlayer.Display.Text.Out
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.TotalScore
        //	[Path]=.RightPlayer.GameScore
        //	[Path]=.RightPlayer.Display.Reset
        //	[Path]=.RightPlayer.Display.ToNeutral
        //	[Path]=.RightPlayer.Display.ToLogo
        //	[Path]=.RightPlayer.Display.ToRed
        //	[Path]=.RightPlayer.Display.ToGreen
        //	[Path]=.RightPlayer.Display.StartIdentify
        //	[Path]=.RightPlayer.Display.Text.Value
        //	[Path]=.RightPlayer.Display.Text.In
        //	[Path]=.RightPlayer.Display.Text.Reset
        //	[Path]=.RightPlayer.Display.Text.Out
        //	[Path]=.Buzzer.Reset
        //	[Path]=.Buzzer.Out
        //	[Path]=.Buzzer.LeftPlayerRed
        //	[Path]=.Buzzer.LeftPlayerGreen
        //	[Path]=.Buzzer.RightPlayerRed
        //	[Path]=.Buzzer.RightPlayerGreen
        //	[Path]=.ShowGameScore.Reset
        //	[Path]=.ShowGameScore.In
        //	[Path]=.ShowGameScore.Out
        //	[Path]=.LiveVideo.Reset
        //	[Path]=.LiveVideo.In
        //	[Path]=.LiveVideo.Out
        //	[Path]=.Clear
        //	[Path]=.FlipColor

        #region Properties

        private const string sceneID = "project/host";

        public VRemote4.HandlerSi.Port GamePort;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
            this.GamePort = this.addPort("GameLayer");
        }

        public void FadeIn() { this.SetDataItemTrigger(".MainFader.In"); }
        public void FadeOut() { this.SetDataItemTrigger(".MainFader.Out"); }

        public void GameScoreIn() { this.SetDataItemTrigger(".ShowGameScore.In"); }
        public void GameScoreOut() { this.SetDataItemTrigger(".ShowGameScore.Out"); }

        public void LiveVideoIn() { this.SetDataItemTrigger(".LiveVideo.In"); }
        public void LiveVideoOut() { this.SetDataItemTrigger(".LiveVideo.Out"); }

        public void SetFlipColor(bool value) { this.SetDataItemValue(".FlipColor", value); }

        public void SetDisplaysToNeutral() { this.SetDataItemTrigger(".Displays.ToNeutral"); }
        public void SetDisplaysToLogo() { this.SetDataItemTrigger(".Displays.ToLogo"); }
        public void StartDisplaysIdentify() { this.SetDataItemTrigger(".Displays.StartIdentify"); }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerTotalScore(ushort value) { this.SetDataItemValue(".LeftPlayer.TotalScore", value); }
        public void SetLeftPlayerGameScore(int value) { this.SetDataItemValue(".LeftPlayer.GameScore", value); }
        public void ResetLeftDisplay() { this.SetDataItemTrigger(".LeftPlayer.Display.Reset"); }
        public void SetLeftDisplayToNeutral() { this.SetDataItemTrigger(".LeftPlayer.Display.ToNeutral"); }
        public void SetLeftDisplayToLogo() { this.SetDataItemTrigger(".LeftPlayer.Display.ToLogo"); }
        public void SetLeftDisplayToRed() { this.SetDataItemTrigger(".LeftPlayer.Display.ToRed"); }
        public void SetLeftDisplayToGreen() { this.SetDataItemTrigger(".LeftPlayer.Display.ToGreen"); }
        public void StartLeftDisplayIdentify() { this.SetDataItemTrigger(".LeftPlayer.Display.StartIdentify"); }
        public void SetLeftDisplayText(string value) { this.SetDataItemValue(".LeftPlayer.Display.Text.Value", value); }
        public void LeftDisplayTextIn() { this.SetDataItemTrigger(".LeftPlayer.Display.Text.In"); }
        public void LeftDisplayTextOut() { this.SetDataItemTrigger(".LeftPlayer.Display.Text.Out"); }
        public void ResetLeftDisplayText() { this.SetDataItemTrigger(".LeftPlayer.Display.Text.Reset"); }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerTotalScore(ushort value) { this.SetDataItemValue(".RightPlayer.TotalScore", value); }
        public void SetRightPlayerGameScore(int value) { this.SetDataItemValue(".RightPlayer.GameScore", value); }
        public void ResetRightDisplay() { this.SetDataItemTrigger(".RightPlayer.Display.Reset"); }
        public void SetRightDisplayToNeutral() { this.SetDataItemTrigger(".RightPlayer.Display.ToNeutral"); }
        public void SetRightDisplayToLogo() { this.SetDataItemTrigger(".RightPlayer.Display.ToLogo"); }
        public void SetRightDisplayToRed() { this.SetDataItemTrigger(".RightPlayer.Display.ToRed"); }
        public void SetRightDisplayToGreen() { this.SetDataItemTrigger(".RightPlayer.Display.ToGreen"); }
        public void StartRightDisplayIdentify() { this.SetDataItemTrigger(".RightPlayer.Display.StartIdentify"); }
        public void SetRightDisplayText(string value) { this.SetDataItemValue(".RightPlayer.Display.Text.Value", value); }
        public void RightDisplayTextIn() { this.SetDataItemTrigger(".RightPlayer.Display.Text.In"); }
        public void RightDisplayTextOut() { this.SetDataItemTrigger(".RightPlayer.Display.Text.Out"); }
        public void ResetRightDisplayText() { this.SetDataItemTrigger(".RightPlayer.Display.Text.Reset"); }

        public void ResetBuzzer() { this.SetDataItemTrigger(".Buzzer.Reset"); }
        public void SetBuzzerOut() { this.SetDataItemTrigger(".Buzzer.Out"); }
        public void SetBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.LeftPlayerRed"); }
        public void SetBuzzerRight() { this.SetDataItemTrigger(".Buzzer.RightPlayerRed"); }

        public void Clear() { 
            this.SetDataItemTrigger(".Clear");
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
