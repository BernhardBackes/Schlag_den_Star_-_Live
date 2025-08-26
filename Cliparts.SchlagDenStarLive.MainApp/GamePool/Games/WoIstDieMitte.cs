
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WoIstDieMitte : Templates.InsertMovieBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WoIstDieMitte()
            : base("Wo ist die Mitte?") {
            this.ContentPositionX = 0;
            this.ContentPositionY = 0;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
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
