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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Geography;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PlaceImageScore {

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
                    this.decimalDegrees = Math.Abs(value);
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
                    string[] values = value.Split(';');
                    int result = 0;
                    if (values.Length >= 1) {
                        values[0] = values[0].Trim();
                        if (int.TryParse(values[0], out result)) this.Degrees = result;
                    }
                    else this.Degrees = 0;
                    if (values.Length >= 2) {
                        values[1] = values[1].Trim();
                        if (int.TryParse(values[1], out result)) this.Minutes = result;
                    }
                    else this.Minutes = 0;
                    if (values.Length >= 3) {
                        values[2] = values[2].Trim();
                        if (int.TryParse(values[2], out result)) this.Seconds = result;
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
            return Math.Abs(decimalDegrees);
        }

        public void SetDecimalDegrees(
            double value) { 
            this.DecimalDegrees = value;
            this.Text = textBuilder();
        }

        #endregion

        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class CoordinateSet : Messaging.Message, INotifyPropertyChanged {

        public const double HD_ResolutionY = 1080;
        public const double HD_ResolutionX = 1920;

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

        private double hd_OriginX = -12.5;
        private double hd_ScaleX = 1721 / Transformation.EarthOutline;
        private double hd_OriginY = -167.29;
        private double hd_ScaleY = 1616.08 / Transformation.EarthOutline;

        public double Mercator_PositionY {
            get {
                double value = Transformation.GetMercatorYCoordinateFromLatitude(this.Latitude);
                if (value < Transformation.EarthOutline * (-0.5)) value = Transformation.EarthOutline * (-0.5);
                if (value > Transformation.EarthOutline * (0.5)) value = Transformation.EarthOutline * (0.5);
                return value;
            }
        }

        public double Mercator_PositionX {
            get {
                double value = Transformation.GetMercatorXCoordinateFromLongitude(this.Longitude);
                if (value < Transformation.EarthOutline * (-0.5)) value = Transformation.EarthOutline * (-0.5);
                if (value > Transformation.EarthOutline * (0.5)) value = Transformation.EarthOutline * (0.5);
                return value;
            }
        }

        public double HD_PositionY {
            get {
                double value = this.Mercator_PositionY * this.hd_ScaleY + this.hd_OriginY;
                if (value <= 0 - HD_ResolutionY / 2) value = 0 - HD_ResolutionY / 2 + 1;
                if (value >= HD_ResolutionY / 2) value = HD_ResolutionY / 2 - 1;
                return value;
            }
        }

        public double HD_PositionX {
            get {
                double value = this.Mercator_PositionX * this.hd_ScaleX + this.hd_OriginX;
                if (value <= 0 - HD_ResolutionX / 2) value = 0 - HD_ResolutionX / 2 + 1;
                if (value >= HD_ResolutionX / 2) value = HD_ResolutionX / 2 - 1;
                return value;
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
            double hd_PositionX,
            double hd_PositionY) {
            this.pose();
            double mercatorX = (hd_PositionX - this.hd_OriginX) / this.hd_ScaleX;
            this.Longitude = Transformation.GetLongitudeFromMercatorXCoordinate(mercatorX);
            double mercatorY = (hd_PositionY - this.hd_OriginY) / this.hd_ScaleY;
            this.Latitude = Transformation.GetLatitudeFromMercatorYCoordinate(mercatorY);
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
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        void latitude_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged("Latitude");
            //this.calcLocalYPositions();
            if (!this.repressTextBuilder) this.Text = this.textBuilder();
        }
        void longitude_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged("Longitude");
            //this.calcLocalXPositions();
            if (!this.repressTextBuilder) this.Text = this.textBuilder();
        }

        #endregion

    }

    public static class Transformation {

        public static double EarthOutline = 40054.61; // Erdumfang am Äquator in Kilometer
        public static double EarthDiameter = 6378.12; // Äquatorradius in Kilometer

        public static double GetMercatorYCoordinateFromLatitude(
            Coordinate latitude) {

            double yCoordinate = 0;

            if (latitude.DecimalDegrees < 90.0
                && (latitude.Hemisphere == Coordinate.Hemispheres.North || latitude.Hemisphere == Coordinate.Hemispheres.South)) {

                double radianDegrees = latitude.DecimalDegrees / 180 * Math.PI;

                double radian = Math.Log((1 + Math.Sin(radianDegrees)) / (1 - Math.Sin(radianDegrees)), Math.E) / 2;

                yCoordinate = EarthDiameter * radian;

                if (latitude.Hemisphere == Coordinate.Hemispheres.South) yCoordinate = yCoordinate * (-1);

            }

            return yCoordinate;
        }

        public static double GetMercatorXCoordinateFromLongitude(
            Coordinate longitude) {

            double xCoordinate = 0;

            if (longitude.Hemisphere == Coordinate.Hemispheres.West || longitude.Hemisphere == Coordinate.Hemispheres.East) {

                xCoordinate = EarthOutline / 360.0 * longitude.DecimalDegrees;

                if (longitude.Hemisphere == Coordinate.Hemispheres.West) xCoordinate = xCoordinate * (-1);

            }

            return xCoordinate;
        }

        public static Coordinate GetLatitudeFromMercatorYCoordinate(
            double yCoordinate) {
            Coordinate latitude = new Coordinate();
            if (yCoordinate < 0) latitude.Hemisphere = Coordinate.Hemispheres.South;
            else latitude.Hemisphere = Coordinate.Hemispheres.North;
            double radian = Math.Abs(yCoordinate) / EarthDiameter;
            double radianDegrees = Math.Asin(Math.Tanh(radian));
            double decimalDegrees = radianDegrees * 180 / Math.PI;
            latitude.SetDecimalDegrees(decimalDegrees);
            return latitude;
        }

        public static Coordinate GetLongitudeFromMercatorXCoordinate(
            double xCoordinate) {
            Coordinate longitude = new Coordinate();
            if (xCoordinate < 0) longitude.Hemisphere = Coordinate.Hemispheres.West;
            else longitude.Hemisphere = Coordinate.Hemispheres.East;
            double decimalDegrees = Math.Abs(xCoordinate) / EarthOutline * 360.0;
            longitude.SetDecimalDegrees(decimalDegrees);
            return longitude;
        }

        public static double GetDistance(
            CoordinateSet source,
            CoordinateSet target) {

            double distance = 0;

            if (source.Latitude.Degrees <= 90
                && (source.Latitude.Hemisphere == Coordinate.Hemispheres.North || source.Latitude.Hemisphere == Coordinate.Hemispheres.South)
                && (source.Longitude.Hemisphere == Coordinate.Hemispheres.West || source.Longitude.Hemisphere == Coordinate.Hemispheres.East)
                && target.Latitude.Degrees <= 90
                && (target.Latitude.Hemisphere == Coordinate.Hemispheres.North || target.Latitude.Hemisphere == Coordinate.Hemispheres.South)
                && (target.Longitude.Hemisphere == Coordinate.Hemispheres.West || target.Longitude.Hemisphere == Coordinate.Hemispheres.East)) {

                double sourceLatitudeRadianDegrees = source.Latitude.DecimalDegrees / 180 * Math.PI;
                if (source.Latitude.Hemisphere == Coordinate.Hemispheres.South) sourceLatitudeRadianDegrees = sourceLatitudeRadianDegrees * (-1);
                double sourceLongitudeRadianDegrees = source.Longitude.DecimalDegrees / 180 * Math.PI;
                if (source.Longitude.Hemisphere == Coordinate.Hemispheres.West) sourceLongitudeRadianDegrees = sourceLongitudeRadianDegrees * (-1);
                double targetLatitudeRadianDegrees = target.Latitude.DecimalDegrees / 180 * Math.PI;
                if (target.Latitude.Hemisphere == Coordinate.Hemispheres.South) targetLatitudeRadianDegrees = targetLatitudeRadianDegrees * (-1);
                double targetLongitudeRadianDegrees = target.Longitude.DecimalDegrees / 180 * Math.PI;
                if (target.Longitude.Hemisphere == Coordinate.Hemispheres.West) targetLongitudeRadianDegrees = targetLongitudeRadianDegrees * (-1);

                double radian =
                    Math.Acos(
                        Math.Sin(sourceLatitudeRadianDegrees)
                        * Math.Sin(targetLatitudeRadianDegrees)
                        + Math.Cos(sourceLatitudeRadianDegrees)
                        * Math.Cos(targetLatitudeRadianDegrees)
                        * Math.Cos(targetLongitudeRadianDegrees - sourceLongitudeRadianDegrees));

                distance = radian * EarthDiameter;

            }

            return distance;
        }
    }

}
