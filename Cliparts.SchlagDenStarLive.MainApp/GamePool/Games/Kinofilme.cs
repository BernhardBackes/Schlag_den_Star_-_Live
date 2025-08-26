
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Kinofilme : Templates.WindingQuiz.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Kinofilme()
            : base("Kinofilme") {
            this.TimeoutPositionX = 0;
            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
