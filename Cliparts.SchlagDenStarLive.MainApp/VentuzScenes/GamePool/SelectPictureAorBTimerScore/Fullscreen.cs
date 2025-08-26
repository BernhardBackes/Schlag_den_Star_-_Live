using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectPictureAorBTimerScore {

    //	[Path]=.Picture.Filename
    //	[Path]=.Picture.Fader.Reset
    //	[Path]=.Picture.Fader.SetOut
    //	[Path]=.Picture.Fader.ToOut
    //	[Path]=.Picture.Fader.SetIn
    //	[Path]=.Picture.Fader.ToIn
    /*
    [Path]= .Selection.LeftPlayer (full path of the DataItem)
    [Description]=  (the description of the DataItem)
    [Label]= Input (the display label of the DataItem)
    [Mode]= RW (read/write mode of the DataItem)
    [Name]= LeftPlayer (the name of the DataItem)
    [UserData]=  (user-defined information of the DataItem)
    [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
        [Default]= NA (the default value for this DataItem)
        [Elements]= NA,PictureA,PictureB (an array of strings containing the choice of enum values)
        [PropertyType]= System.String (the underlaying system type of this DataItem)
     */
    //	[Path]=.Selection.RightPlayer
    //	[Path]=.Selection.Fader.Reset
    //	[Path]=.Selection.Fader.SetOut
    //	[Path]=.Selection.Fader.ToOut
    //	[Path]=.Selection.Fader.SetIn
    //	[Path]=.Selection.Fader.ToIn
    //	[Path]=.Solution.Filename
    //	[Path]=.Solution.Fader.Reset
    //	[Path]=.Solution.Fader.Show
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Show

    public class Fullscreen : _Base {

        public enum SelectionElements { NA, PictureA, PictureB }

        #region Properties

        private const string sceneID = "project/gamepool/selectpictureaorbtimerscore/fullscreen";

        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Picture.Filename", value); }

        public void SetOut() { this.SetDataItemTrigger(".Picture.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Picture.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Picture.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Picture.Fader.ToIn"); }

        public void SetLeftPlayerSelection(SelectionElements value) { this.SetDataItemValue(".Selection.LeftPlayer", value); }
        public void SetRightPlayerSelection(SelectionElements value) { this.SetDataItemValue(".Selection.RightPlayer", value); }
        public void ResetSelection() { this.SetDataItemTrigger(".Selection.Fader.Reset"); }
        public void SetSelectionOut() { this.SetDataItemTrigger(".Selection.Fader.SetOut"); }
        public void SelectionToOut() { this.SetDataItemTrigger(".Selection.Fader.ToOut"); }
        public void SetSelectionIn() { this.SetDataItemTrigger(".Selection.Fader.SetIn"); }
        public void SelectionToIn() { this.SetDataItemTrigger(".Selection.Fader.ToIn"); }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }
        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Fader.Reset"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.Fader.Show"); }

        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }
        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }

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
