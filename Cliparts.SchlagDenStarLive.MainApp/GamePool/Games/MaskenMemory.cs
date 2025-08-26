
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class MaskenMemory : Templates.Memory.Business {

        #region Properties
        #endregion


        #region Funktionen

        public MaskenMemory()
            : base("Masken-Memory") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
            this.DuellMode = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
