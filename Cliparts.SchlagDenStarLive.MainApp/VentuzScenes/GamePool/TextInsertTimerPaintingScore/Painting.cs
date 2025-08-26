using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertTimerPaintingScore
{

    public class Painting : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.Text.Value
        //	[Path]=.Text.Reset
        //	[Path]=.Text.SetOut
        //	[Path]=.Text.ToOut
        //	[Path]=.Text.SetIn
        //	[Path]=.Text.ToIn
        //	[Path]=.Paint.Reset

        #region Properties

        //private const string sceneID = "project/gamepool/textinserttimerpaintingscore/painting";
        private const string sceneID = "project/gamepool/textinserttimerpaintingscore/text";

        #endregion


        #region Funktionen

        public Painting(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID)
        {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }
        public void ResetText() { this.SetDataItemTrigger(".Text.Reset"); }
        public void SetTextOut() { this.SetDataItemTrigger(".Text.SetOut"); }
        public void ToTextOut() { this.SetDataItemTrigger(".Text.ToOut"); }
        public void SetTextIn() { this.SetDataItemTrigger(".Text.SetIn"); }
        public void ToTextIn() { this.SetDataItemTrigger(".Text.ToIn"); }

        public void ResetPaint() { this.SetDataItemTrigger(".Paint.Reset"); }

        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.ToTextOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
