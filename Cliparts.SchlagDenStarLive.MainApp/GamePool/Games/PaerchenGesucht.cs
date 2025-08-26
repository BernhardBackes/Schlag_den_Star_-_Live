
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PaerchenGesucht : Templates.MorveMovieScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PaerchenGesucht()
            : base("Pärchen gesucht") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.TimeoutPositionY = 20;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.FourDots;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -200;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = -200;
            this.TaskCounterSize = 13;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
