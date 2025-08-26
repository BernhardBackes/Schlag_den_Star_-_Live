using System;
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WerWarDasMal : Templates.MorveMovieScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WerWarDasMal()
            : base("Wer war das mal?") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.TimeoutPositionY = 20;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 40;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 13;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
