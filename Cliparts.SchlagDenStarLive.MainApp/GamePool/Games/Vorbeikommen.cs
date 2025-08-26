
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Vorbeikommen : Templates.TeamBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Vorbeikommen()
            : base("Vorbeikommen") {

            this.LeftPlayerBuzzerChannel = 1;
            this.LeftPlayer2ndBuzzerChannel = 2;
            this.LeftPlayerDMXStartchannel = 1;
            this.LeftPlayer2ndDMXStartchannel = 4;

            this.RightPlayerBuzzerChannel = 3;
            this.RightPlayer2ndBuzzerChannel = 4;
            this.RightPlayerDMXStartchannel = 7;
            this.RightPlayer2ndDMXStartchannel = 10;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;

            this.TimeoutDuration = 0;
            this.TimeoutIsVisible = false;
            this.TimeoutPositionX = 0;
            this.TimeoutPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
