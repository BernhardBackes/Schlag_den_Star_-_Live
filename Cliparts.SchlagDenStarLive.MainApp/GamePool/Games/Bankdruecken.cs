namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Bankdruecken : Templates.WeightLifting.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Bankdruecken()
            : base("Bankdrücken") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 45;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 5;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.MarkerSetsPositionX = 0;
            this.MarkerSetsPositionY = 0;
            this.MarkerBeginning = 40;
            this.MarkerExpanse = 5;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
