
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TextAufgaben : Templates.TextInsertBuzzerAudioScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TextAufgaben()
            : base("Text-Aufgaben") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TimeoutPositionY = 20;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 40;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 11;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
