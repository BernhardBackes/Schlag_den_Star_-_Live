
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schmecken : Templates.PictureTwoTimersScores.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schmecken()
            : base("Schmecken") {
            this.TwoTimersScoresPositionX = 0;
            this.TwoTimersScoresPositionX = 0;
            this.TwoTimersScoresTimerStyle = VentuzScenes.GamePool._Modules.TwoTimersScores.Styles.MinSec;
            this.TwoTimersScoresStartTime = 300;
            this.TwoTimersScoresAlarmTime1 = 10;
            this.TwoTimersScoresAlarmTime2 = -1;
            this.TwoTimersScoresStopTime = 0;
            this.ContentPositionX = 0;
            this.ContentPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
