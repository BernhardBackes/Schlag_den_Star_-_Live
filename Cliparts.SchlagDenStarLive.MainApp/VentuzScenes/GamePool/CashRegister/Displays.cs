using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CashRegister {

    public class Displays : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.LeftDisplay.Value
        //	[Path]=.LeftDisplay.ShowLogo
        //	[Path]=.LeftDisplay.HideLogo
        //	[Path]=.RightDisplay.Value
        //	[Path]=.RightDisplay.ShowLogo
        //	[Path]=.RightDisplay.HideLogo
        
        #region Properties

        private const string sceneID = "project/gamepool/cashregister/displays";

        #endregion


        #region Funktionen

        public Displays(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void ShowLeftDisplayLogo() { this.SetDataItemTrigger(".LeftDisplay.ShowLogo"); }
        public void HideLeftDisplayLogo() { this.SetDataItemTrigger(".LeftDisplay.HideLogo"); }
        public void SetLeftDisplayValue(double value) { this.SetDataItemValue(".LeftDisplay.Value", value); }

        public void ShowRightDisplayLogo() { this.SetDataItemTrigger(".RightDisplay.ShowLogo"); }
        public void HideRightDisplayLogo() { this.SetDataItemTrigger(".RightDisplay.HideLogo"); }
        public void SetRightDisplayValue(double value) { this.SetDataItemValue(".RightDisplay.Value", value); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
