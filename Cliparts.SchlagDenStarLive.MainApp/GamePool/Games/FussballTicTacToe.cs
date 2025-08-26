namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FussballTicTacToe : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FussballTicTacToe()
            : base("Fußball Tic Tac Toe") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
