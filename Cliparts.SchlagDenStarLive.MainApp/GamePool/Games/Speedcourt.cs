using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Speedcourt : Templates.MemoCourseCourt.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Speedcourt() : base("Speedcourt") { 
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CoursePositionX = 0;
            this.CoursePositionY = -375;
            this.CourseScaling = 20;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 300;
            this.TimerStopTime = 0;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    //public class Speedcourt : GamePool.Templates.MemoCourt.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public Speedcourt() : base("Speedcourt") {
            //this.CounterPositionX = 0;
            //this.CounterPositionY = 0;
            //this.SequencePositionX = 0;
            //this.SequencePositionY = 0;
            //this.TimerAlarmTime1 = 10;
            //this.TimerAlarmTime2 = -1;
            //this.TimerPositionX = 0;
            //this.TimerPositionY = 0;
            //this.TimerStartTime = 300;
            //this.TimerStopTime = 0;
            //this.ShowFullscreenTimer = true;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

    }
