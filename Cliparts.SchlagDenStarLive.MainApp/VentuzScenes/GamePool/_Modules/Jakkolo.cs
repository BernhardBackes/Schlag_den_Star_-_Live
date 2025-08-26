using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Jakkolo : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name.Value
        //	[Path]=.Top.Coins.Coin1.Reset.Invoke
        //	[Path]=.Top.Coins.Coin1.SetValue.Invoke
        //	[Path]=.Top.Coins.Coin1.ToValue.Invoke
        //	[Path]=.Top.Coins.Coin2.Reset.Invoke
        //	...
        //	[Path]=.Top.Coins.Coin4.ToValue.Invoke
        //	[Path]=.Top.Bonus.SetBonus.Invoke
        //	[Path]=.Top.Bonus.ToBonus.Invoke
        //	[Path]=.Top.Score
        //	[Path]=.Bottom.Name.Value
        //	[Path]=.Bottom.Coins.Coin1.Reset.Invoke
        //	[Path]=.Bottom.Coins.Coin1.SetValue.Invoke
        //	[Path]=.Bottom.Coins.Coin1.ToValue.Invoke
        //	[Path]=.Bottom.Coins.Coin2.Reset.Invoke
        //	...
        //	[Path]=.Bottom.Coins.Coin4.ToValue.Invoke
        //	[Path]=.Bottom.Bonus.SetBonus.Invoke
        //	[Path]=.Bottom.Bonus.ToBonus.Invoke
        //	[Path]=.Bottom.Score

        #region Properties

        private const string sceneID = "project/gamepool/_modules/jakkolo";

        #endregion


        #region Funktionen

        public Jakkolo(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Jakkolo(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }


        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name.Value", value); }
        public void ResetTopCoin(int id) { this.SetDataItemTrigger(string.Format(".Top.Coins.Coin{0}.Reset.Invoke", id.ToString())); }
        public void SetTopCoinValue(int id, int value) { this.SetDataItemTrigger(string.Format(".Top.Coins.Coin{0}.SetValue.Invoke", id.ToString()), value); }
        public void TopCoinToValue(int id, int value) { this.SetDataItemTrigger(string.Format(".Top.Coins.Coin{0}.ToValue.Invoke", id.ToString()), value); }
        public void SetTopBonus(int value) { this.SetDataItemTrigger(".Top.Bonus.SetBonus.Invoke", value); }
        public void TopBonusToValue(int value) { this.SetDataItemTrigger(".Top.Bonus.ToBonus.Invoke", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name.Value", value); }
        public void ResetBottomCoin(int id) { this.SetDataItemTrigger(string.Format(".Bottom.Coins.Coin{0}.Reset.Invoke", id.ToString())); }
        public void SetBottomCoinValue(int id, int value) { this.SetDataItemTrigger(string.Format(".Bottom.Coins.Coin{0}.SetValue.Invoke", id.ToString()), value); }
        public void BottomCoinToValue(int id, int value) { this.SetDataItemTrigger(string.Format(".Bottom.Coins.Coin{0}.ToValue.Invoke", id.ToString()), value); }
        public void SetBottomBonus(int value) { this.SetDataItemTrigger(".Bottom.Bonus.SetBonus.Invoke", value); }
        public void BottomBonusToValue(int value) { this.SetDataItemTrigger(".Bottom.Bonus.ToBonus.Invoke", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
