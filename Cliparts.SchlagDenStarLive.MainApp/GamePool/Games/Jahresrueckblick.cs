
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Jahresrueckblick : Templates.ImageDateInputScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Jahresrueckblick()
            : base("Jahresrückblick") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.BorderPositionX = 0;
            this.BorderPositionY = 0;
            this.BorderScaling = 100;
            this.GamePositionX = 0;
            this.GamePositionY = 0;
            this.GameScaling = 100;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
