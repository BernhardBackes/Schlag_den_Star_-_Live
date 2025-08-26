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
using System.Xml.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes {

    public class MediaPlayer : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset.Invoke
        //	[Path]=.Position.X.Value
        //	[Path]=.Position.Y.Value
        //	[Path]=.ScalingFactor.Value
        //	[Path]=.Fader.Duration.Value
        //	[Path]=.Fader.Clear.Invoke
        //	[Path]=.Fader.Set.Invoke
        //	[Path]=.Fader.OnAir.Value
        //	[Path]=.Fader.OnAir.Changed
        //	[Path]=.Player.Filename.Value
        //	[Path]=.Player.Volume.Value
        //	[Path]=.Player.Loop.Value
        //	[Path]=.Player.Play.Invoke
        //	[Path]=.Player.Stop.Invoke
        //	[Path]=.Player.Continue.Invoke

        #region Properties

        private const string sceneID = "mediaplayer";

        private int positionX = 0;
        public int PositionX {
            get { return this.positionX; }
            private set {
                if (this.positionX != value) {
                    this.positionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int positionY = 0;
        public int PositionY {
            get { return this.positionY; }
            private set {
                if (this.positionY != value) {
                    this.positionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double scalingFactor = 100;
        public double ScalingFactor {
            get { return this.scalingFactor; }
            private set {
                if (this.scalingFactor != value) {
                    if (value <= 0) this.scalingFactor = 0;
                    else this.scalingFactor = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            private set {
                if (this.filename != value) {
                    this.filename = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool loop = false;
        public bool Loop {
            get { return this.loop; }
            private set {
                if (this.loop != value) {
                    this.loop = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public const int VolumeMin = 0;
        public const int VolumeMax = 100;
        private int volume = VolumeMax;
        public int Volume {
            get { return this.volume; }
            private set {
                if (this.volume != value) {
                    if (value < VolumeMin) this.volume = VolumeMin;
                    else if (value > VolumeMax) this.volume = VolumeMax;
                    else this.volume = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double fadeDuration = 0;
        public double FadeDuration {
            get { return this.fadeDuration; }
            private set {
                if (this.fadeDuration != value) {
                    if (value <= 0) this.fadeDuration = 0;
                    else this.fadeDuration = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool onAir = false;
        public bool OnAir {
            get { return this.onAir; }
            private set {
                if (this.onAir != value) {
                    this.onAir = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public MediaPlayer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public MediaPlayer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void Clear() { this.SetDataItemTrigger(".Fader.Clear.Invoke"); }
        public void Reset() { 
            this.SetDataItemTrigger(".Reset.Invoke");
            this.SetPositionX(0);
            this.SetPositionY(0);
            this.SetScalingFactor(100);
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X.Value", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y.Value", value); }
        public void SetScalingFactor(double value) { this.SetDataItemValue(".ScalingFactor.Value", value); }

        public void SetFilename(string value) { this.SetDataItemValue(".Player.Filename.Value", value); }
        public void SetLoop(bool value) { this.SetDataItemValue(".Player.Loop.Value", value); }
        public void SetVolume(int value) { this.SetDataItemValue(".Player.Volume.Value", value); }
        public void SetFadeDuration(double value) { this.SetDataItemValue(".Fader.Duration.Value", value); }

        public void Set() { this.SetDataItemTrigger(".Fader.Set.Invoke"); }
        public void Play() { this.SetDataItemTrigger(".Player.Play.Invoke"); }
        public void Stop() { this.SetDataItemTrigger(".Player.Stop.Invoke"); }
        public void Continue() { this.SetDataItemTrigger(".Player.Continue.Invoke"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs && e.Value != null) {
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs
                && e.Value != null) {
                if (e.Path == ".Position.X.Value") this.PositionX = Convert.ToInt32(e.Value);
                else if (e.Path == ".Position.Y.Value") this.PositionY = Convert.ToInt32(e.Value);
                else if (e.Path == ".ScalingFactor.Value") this.ScalingFactor = Convert.ToDouble(e.Value);
                else if (e.Path == ".Player.Filename.Value") this.Filename = e.Value.ToString();
                else if (e.Path == ".Player.Loop.Value") this.Loop = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Player.Volume.Value") this.Volume = Convert.ToInt32(e.Value);
                else if (e.Path == ".Fader.Duration.Value") this.FadeDuration = Convert.ToDouble(e.Value);
                else if (e.Path == ".Fader.OnAir.Value") this.OnAir = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Fader.OnAir.Changed") this.OnAir = Convert.ToBoolean(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
