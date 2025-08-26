using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScrabbleListCounterScoreTimer {

    public enum LetterValueElements { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, Ä, Ö, Ü }

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Scaling
        /*
        [Path]= .LeftPlayer.Letter_00.ValueInput (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Letter_00ValueInput (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= ValueInput (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= A (the default value for this DataItem)
            [Elements]= A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,Ä,Ö,Ü (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.LeftPlayer.Letter_00.ResetInvoke
        //	[Path]=.LeftPlayer.Letter_00.SetInInvoke
        //	[Path]=.LeftPlayer.Letter_00.SetOutInvoke
        //	[Path]=.LeftPlayer.Letter_00.ToInInvoke
        //	[Path]=.LeftPlayer.Letter_00.ToOutInvoke
        //	[Path]=.LeftPlayer.Letter_01.ValueInput
        //  .....
        //	[Path]=.RightPlayer.Letter_08.ToOutInvoke

        #region Properties

        private const string sceneID = "project/gamepool/scrabblelistcounterscoretimer/game";

        #endregion


        #region Funktionen
        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetScaling(int value) { this.SetDataItemValue(".Scaling", value); }

        private string getLeftPlayerPrefix(int id) { return string.Format(".LeftPlayer.Letter_{0}", id.ToString("00")); }
        public void SetLeftPlayerLetterValue(int id, LetterValueElements value) { this.SetDataItemValue(this.getLeftPlayerPrefix(id) + ".ValueInput", value); }
        public void ResetLeftPlayerLetter(int id) { this.SetDataItemTrigger(this.getLeftPlayerPrefix(id) + ".ResetInvoke"); }
        public void SetLeftPlayerLetterIn(int id) { this.SetDataItemTrigger(this.getLeftPlayerPrefix(id) + ".SetInInvoke"); }
        public void SetLeftPlayerLetterOut(int id) { this.SetDataItemTrigger(this.getLeftPlayerPrefix(id) + ".SetOutInvoke"); }
        public void LeftPlayerLetterToIn(int id) { this.SetDataItemTrigger(this.getLeftPlayerPrefix(id) + ".ToInInvoke"); }
        public void LeftPlayerLetterToOut(int id) { this.SetDataItemTrigger(this.getLeftPlayerPrefix(id) + ".ToOutInvoke"); }

        private string getRightPlayerPrefix(int id) { return string.Format(".RightPlayer.Letter_{0}", id.ToString("00")); }
        public void SetRightPlayerLetterValue(int id, LetterValueElements value) { this.SetDataItemValue(this.getRightPlayerPrefix(id) + ".ValueInput", value); }
        public void ResetRightPlayerLetter(int id) { this.SetDataItemTrigger(this.getRightPlayerPrefix(id) + ".ResetInvoke"); }
        public void SetRightPlayerLetterIn(int id) { this.SetDataItemTrigger(this.getRightPlayerPrefix(id) + ".SetInInvoke"); }
        public void SetRightPlayerLetterOut(int id) { this.SetDataItemTrigger(this.getRightPlayerPrefix(id) + ".SetOutInvoke"); }
        public void RightPlayerLetterToIn(int id) { this.SetDataItemTrigger(this.getRightPlayerPrefix(id) + ".ToInInvoke"); }
        public void RightPlayerLetterToOut(int id) { this.SetDataItemTrigger(this.getRightPlayerPrefix(id) + ".ToOutInvoke"); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion
    }
}
