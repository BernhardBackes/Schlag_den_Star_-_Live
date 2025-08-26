using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Insert {

    public class Sampler : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {


        /*
        [Path]= .Sample (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Sample (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= BuzzerLeft (the default value for this DataItem)
            [Elements]= BuzzerLeft,BuzzerRight,Flip,Select,Deselect,Bad,Good,Resolve,Bell,SpeedcourtRichtig (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */        
        //	[Path]=.Play

        public enum SampleElements { BuzzerLeft, BuzzerRight, Flip, Select, Deselect, Bad, Good, Resolve, Bell, SpeedcourtRichtig, KeyPress }

        #region Properties

        private const string sceneID = "project/insert/sampler";

        #endregion


        #region Funktionen

        public Sampler(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, 
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Sampler(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, 
            string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void Play(
            SampleElements value)
        {
            this.SetDataItemValue(".Sample", value);
            this.SetDataItemTrigger(".Play");
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
