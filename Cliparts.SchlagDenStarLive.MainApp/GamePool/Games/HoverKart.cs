
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class HoverKart : Templates.TimerForTwoSoloScoreCounter.Business {

        #region Properties
        #endregion


        #region Funktionen

        public HoverKart()
            : base("Hover-Kart") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 8;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
