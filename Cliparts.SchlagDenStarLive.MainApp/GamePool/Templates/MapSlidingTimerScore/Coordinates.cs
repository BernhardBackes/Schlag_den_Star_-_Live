using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MapSlidingTimerScore {

    public class Coordinate : Messaging.Message, INotifyPropertyChanged {

        public enum Hemispheres { North, South, West, East }

        #region Properties

        private Hemispheres hemisphere = Hemispheres.East;
        [NotSerialized]
        public Hemispheres Hemisphere {
            get { return this.hemisphere; }
            set {
                if (this.hemisphere != value) {
                    this.hemisphere = value;
                }
            }
        }

        private int degrees = 0;
        [XmlIgnore]
        public int Degrees {
            get { return this.degrees; }
            private set {
                if (this.degrees != value) {
                    this.degrees = value;
                    if (this.degrees < 0) this.degrees = 0;
                    if (this.degrees > 180) this.degrees = 180;
                    if (this.degrees == 180) {
                        this.Minutes = 0;
                        this.Seconds = 0;
                    }
                }
            }
        }

        private int minutes = 0;
        [XmlIgnore]
        public int Minutes {
            get { return this.minutes; }
            private set {
                if (this.minutes != value) {
                    this.minutes = value;
                    if (this.minutes < 0) this.minutes = 0;
                    if (this.minutes >= 60) this.minutes = 59;
                    if (this.Degrees == 180) this.minutes = 0;
                }
            }
        }

        private int seconds = 0;
        [XmlIgnore]
        public int Seconds {
            get { return this.seconds; }
            private set {
                if (this.seconds != value) {
                    this.seconds = value;
                    if (this.seconds < 0) this.seconds = 0;
                    if (this.seconds >= 60) this.seconds = 59;
                    if (this.Degrees == 180) this.seconds = 0;
                }
            }
        }

        private double decimalDegrees = 0;
        [XmlIgnore]
        public double DecimalDegrees {
            get { return this.decimalDegrees; }
            private set {
                if (this.decimalDegrees != value) {
                    this.decimalDegrees = value;
                    this.Degrees = (int)(decimalDegrees / 1);
                    this.Minutes = (int)(((decimalDegrees * 60) % 60) / 1);
                    this.Seconds = (int)(((decimalDegrees * 3600) % 60) / 1);
                }
            }
        }

        private string text = "0° 00' 00\" E";
        public string Text {
            get { return this.text; }
            set {
                if (value != this.Text) {
                    value = value.Trim().Replace("°", ";");
                    value = value.Replace("'", ";");
                    value = value.Replace("′", ";");
                    value = value.Replace("\"", ";");
                    value = value.Replace("″", ";");
                    value = value.Replace(";;", ";");
                    value = value.Replace(".", ",");
                    string[] values = value.Split(';');
                    double result = 0;
                    if (values.Length >= 1) {
                        values[0] = values[0].Trim();
                        if (double.TryParse(values[0], out result)) this.Degrees = Convert.ToInt32(result);
                        else this.Degrees = 0;
                    }
                    else this.Degrees = 0;
                    if (values.Length >= 2) {
                        values[1] = values[1].Trim();
                        if (double.TryParse(values[1], out result)) this.Minutes = Convert.ToInt32(result);
                        else this.Minutes = 0;
                    }
                    else this.Minutes = 0;
                    if (values.Length >= 3) {
                        values[2] = values[2].Trim();
                        if (double.TryParse(values[2], out result)) this.Seconds = Convert.ToInt32(result);
                        else this.Seconds = 0;
                    }
                    else this.Seconds = 0;
                    if (values.Length >= 1) {
                        int index = values.Length - 1;
                        values[index] = values[index].Trim().ToUpper();
                        if (values[index].StartsWith("N")) this.Hemisphere = Coordinate.Hemispheres.North;
                        else if (values[index].StartsWith("S")) this.Hemisphere = Coordinate.Hemispheres.South;
                        else if (values[index].StartsWith("W")) this.Hemisphere = Coordinate.Hemispheres.West;
                        else if (values[index].StartsWith("E")) this.Hemisphere = Coordinate.Hemispheres.East;
                        else if (values[index].StartsWith("O")) this.Hemisphere = Coordinate.Hemispheres.East;
                    }
                    this.text = this.textBuilder();
                    this.DecimalDegrees = this.decimalDegreesBuilder();
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Coordinate() { }
        public Coordinate(
            string text) {
            this.Text = text;
        }

        private string textBuilder() {
            return string.Format("{0}° {1}′ {2}″ {3}",
                        this.Degrees.ToString(),
                        this.Minutes.ToString("00"),
                        this.Seconds.ToString("00"),
                        this.Hemisphere.ToString().Substring(0, 1));
        }

        private double decimalDegreesBuilder() {
            double decimalDegrees = this.Degrees + (this.Minutes / 60.0) + (this.Seconds / 3600.0);
            if (this.Hemisphere == Hemispheres.South ||
                this.Hemisphere == Hemispheres.West) decimalDegrees = decimalDegrees * -1;
            return decimalDegrees;
        }

        public void SetDecimalDegrees(
            double value) {
            this.DecimalDegrees = value;
            this.Text = textBuilder();
        }

        #endregion

        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class CoordinateSet : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private Coordinate latitude = new Coordinate("0° 00' 00\" N");
        public Coordinate Latitude {
            get { return this.latitude; }
            set {
                if (!(value is Coordinate)) value = new Coordinate("0° 00' 00\" N");
                if (this.latitude.Text != value.Text) {
                    this.latitude.Text = value.Text;
                    if (this.latitude.Hemisphere == Coordinate.Hemispheres.West
                        || this.latitude.Hemisphere == Coordinate.Hemispheres.East) this.latitude.Hemisphere = Coordinate.Hemispheres.North;
                    this.on_PropertyChanged();
                }
            }
        }

        private Coordinate longitude = new Coordinate("0° 00' 00\" E");
        public Coordinate Longitude {
            get { return this.longitude; }
            set {
                if (!(value is Coordinate)) value = new Coordinate("0° 00' 00\" E");
                if (this.longitude.Text != value.Text) {
                    this.longitude.Text = value.Text;
                    if (this.longitude.Hemisphere == Coordinate.Hemispheres.North
                        || this.longitude.Hemisphere == Coordinate.Hemispheres.South) this.longitude.Hemisphere = Coordinate.Hemispheres.East;
                    this.on_PropertyChanged();
                }
            }
        }
        
        private string text = string.Empty; //"50° 56′ 17″ N, 6° 57′ 25″ E";
        [NotSerialized]
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    string[] values = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2) {
                        this.repressTextBuilder = true;
                        this.Latitude.Text = values[0].Trim();
                        this.Longitude.Text = values[1].Trim();
                        this.repressTextBuilder = false;
                    }
                    this.text = this.textBuilder();
                    this.on_PropertyChanged();
                }
            }
        }

        private bool repressTextBuilder = false;

        #endregion


        #region Funktionen

        public CoordinateSet() {
            this.pose();
        }
        public CoordinateSet(
            string text) {
            this.pose();
            this.Text = text;
        }
        public CoordinateSet(
            Coordinate latitude,
            Coordinate longitude) {
            this.pose();
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Text = textBuilder();
        }

        private void pose() {
            this.latitude.PropertyChanged += this.latitude_PropertyChanged;
            this.longitude.PropertyChanged += this.longitude_PropertyChanged;
        }

        private string textBuilder() {
            return string.Format("{0}, {1}",
                        this.Latitude.Text,
                        this.Longitude.Text);
        }
      
        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        void latitude_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged("Latitude");
            if (!this.repressTextBuilder) this.Text = this.textBuilder();
        }
        void longitude_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged("Longitude");
            if (!this.repressTextBuilder) this.Text = this.textBuilder();
        }

        #endregion

    }

}
