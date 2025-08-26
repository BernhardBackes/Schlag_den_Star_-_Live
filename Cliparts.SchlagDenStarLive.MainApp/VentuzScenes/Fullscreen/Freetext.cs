using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public class Freetext : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        /*
        [Path]= .Text.Value (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Value (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Value (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.String (type of the current instance)
            [Default]= text (the default value for this DataItem)
            [MaxLines]=  (the maximum number of lines for this String. A value of null does not define any value.)
            [MinLines]=  (the minimum number of lines for this String. A value of null does not define any value.)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
            [RegEx]=  (a regular expression used to validate the value of this String)
         */

        /*
        [Path]= .Text.Color (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= MaterialLightingModelBaseColor (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Color (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Color (type of the current instance)
            [Alpha]= False (whether this Color item accepts alpha or not)
            [Default]= Color [Black] (the default value for this DataItem)
            [PropertyType]= System.Drawing.Color (the underlaying system type of this DataItem)
         */

        #region Properties     

        private const string sceneID = "project/fullscreen/freetext";

        #endregion


        #region Funktionen

        public Freetext(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Freetext(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void SetTextValue(string value) { this.SetDataItemValue(".Text.Value", value); }
        public void SetTextColor(System.Drawing.Color value) { this.SetDataItemValue(".Text.Color", value); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
