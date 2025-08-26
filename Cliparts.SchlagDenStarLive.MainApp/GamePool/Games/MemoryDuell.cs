
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class MemoryDuell : Templates.Memory.Business {

        #region Properties
        #endregion


        #region Funktionen

        public MemoryDuell()
            : base("Memory-Duell") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
