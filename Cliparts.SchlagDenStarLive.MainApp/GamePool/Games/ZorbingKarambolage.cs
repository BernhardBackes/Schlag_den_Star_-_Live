namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ZorbingKarambolage : Templates.TimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ZorbingKarambolage()
            : base("Zorbing-Karambolage") {
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatSentenceTime = 10;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
