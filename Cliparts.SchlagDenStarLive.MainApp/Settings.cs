using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

using Cliparts.Serialization;

using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.Settings {

    public class Data {

        private string ventuzInsertServer = string.Empty;
        public string VentuzInsertServer {
            get { return this.ventuzInsertServer; }
            set {
                if (string.IsNullOrEmpty(value)) this.ventuzInsertServer = string.Empty;
                else this.ventuzInsertServer = value;
            }
        }

        private string ventuzFullscreenServer = string.Empty;
        public string VentuzFullscreenServer {
            get { return this.ventuzFullscreenServer; }
            set {
                if (string.IsNullOrEmpty(value)) this.ventuzFullscreenServer = string.Empty;
                else this.ventuzFullscreenServer = value;
            }
        }

        private string ventuzHostServer = string.Empty;
        public string VentuzHostServer {
            get { return this.ventuzHostServer; }
            set {
                if (string.IsNullOrEmpty(value)) this.ventuzHostServer = string.Empty;
                else this.ventuzHostServer = value;
            }
        }

        private string ventuzLeftPlayerServer = string.Empty;
        public string VentuzLeftPlayerServer {
            get { return this.ventuzLeftPlayerServer; }
            set {
                if (string.IsNullOrEmpty(value)) this.ventuzLeftPlayerServer = string.Empty;
                else this.ventuzLeftPlayerServer = value;
            }
        }

        private string ventuzRightPlayerServer = string.Empty;
        public string VentuzRightPlayerServer {
            get { return this.ventuzRightPlayerServer; }
            set {
                if (string.IsNullOrEmpty(value)) this.ventuzRightPlayerServer = string.Empty;
                else this.ventuzRightPlayerServer = value;
            }
        }

        private string midiFilename = string.Empty;
        public string MidiFilename {
            get { return this.midiFilename; }
            set {
                if (string.IsNullOrEmpty(value)) this.midiFilename = string.Empty;
                else if (!File.Exists(value)) this.midiFilename = string.Empty;
                else this.midiFilename = value;
            }
        }

        private string ioNetFilename = string.Empty;
        public string IONetFilename {
            get { return this.ioNetFilename; }
            set {
                if (string.IsNullOrEmpty(value)) this.ioNetFilename = string.Empty;
                else if (!File.Exists(value)) this.ioNetFilename = string.Empty;
                else this.ioNetFilename = value;
            }
        }

        private string devantechFilename = string.Empty;
        public string DevantechFilename {
            get { return this.devantechFilename; }
            set {
                if (string.IsNullOrEmpty(value)) this.devantechFilename = string.Empty;
                else if (!File.Exists(value)) this.devantechFilename = string.Empty;
                else this.devantechFilename = value;
            }
        }

        private string contentFilename = string.Empty;
        public string ContentFilename {
            get { return this.contentFilename; }
            set {
                if (string.IsNullOrEmpty(value)) this.contentFilename = string.Empty;
                else if (!File.Exists(value)) this.contentFilename = string.Empty;
                else this.contentFilename = value;
            }
        }

        private int timerPositionX = 0;
        public int TimerPositionX {
            get { return this.timerPositionX; }
            set {
                if (this.timerPositionX != value) {
                    this.timerPositionX = value;
                }
            }
        }

        private int timerPositionY = 0;
        public int TimerPositionY {
            get { return this.timerPositionY; }
            set {
                if (this.timerPositionY != value) {
                    this.timerPositionY = value;
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles timerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                }
            }
        }

        private int timerStartTime = 30;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                }
            }
        }

        private int timerAlarmTime1 = -1;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                }
            }
        }

        private int timerAlarmTime2 = -1;
        public int TimerAlarmTime2 {
            get { return this.timerAlarmTime2; }
            set {
                if (this.timerAlarmTime2 != value) {
                    this.timerAlarmTime2 = value;
                }
            }
        }

        private int timerStopTime = 0;
        public int TimerStopTime {
            get { return this.timerStopTime; }
            set {
                if (this.timerStopTime != value) {
                    this.timerStopTime = value;
                }
            }
        }

        private string car = string.Empty;
        public string Car {
            get { return this.car; }
            set {
                if (string.IsNullOrEmpty(value)) this.car = string.Empty;
                else this.car = value;
            }
        }

        private string gainer = string.Empty;
        public string Gainer {
            get { return this.gainer; }
            set {
                if (string.IsNullOrEmpty(value)) this.gainer = string.Empty;
                else this.gainer = value;
            }
        }

        private double xTippKey = 1.052632;
        public double XTippKey {
            get { return this.xTippKey; }
            set { this.xTippKey = value; }
        }

        private int votingTimerStartTime = 180;
        public int VotingTimerStartTime {
            get { return this.votingTimerStartTime; }
            set {
                if (this.votingTimerStartTime != value) {
                    this.votingTimerStartTime = value;
                }
            }
        }

        private int votingTimerAlarmTime = 10;
        public int VotingTimerAlarmTime {
            get { return this.timerAlarmTime1; }
            set {
                if (this.votingTimerAlarmTime != value) {
                    this.votingTimerAlarmTime = value;
                }
            }
        }

    }

    public class Business : Messaging.Message {

        private Data settingsData;

        public string VentuzInsertServer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.VentuzInsertServer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VentuzInsertServer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.VentuzInsertServer = value;
                    this.Save();
                }
            }
        }

        public string VentuzFullscreenServer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.VentuzFullscreenServer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VentuzFullscreenServer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.VentuzFullscreenServer = value;
                    this.Save();
                }
            }
        }

        public string VentuzHostServer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.VentuzHostServer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VentuzHostServer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.VentuzHostServer = value;
                    this.Save();
                }
            }
        }

        public string VentuzLeftPlayerServer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.VentuzLeftPlayerServer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VentuzLeftPlayerServer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.VentuzLeftPlayerServer = value;
                    this.Save();
                }
            }
        }

        public string VentuzRightPlayerServer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.VentuzRightPlayerServer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VentuzRightPlayerServer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.VentuzRightPlayerServer = value;
                    this.Save();
                }
            }
        }

        public string MidiFilename {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.MidiFilename;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.MidiFilename != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.MidiFilename = value;
                    this.Save();
                }
            }
        }

        public string IONetFilename {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.IONetFilename;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.IONetFilename != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.IONetFilename = value;
                    this.on_IONetFilenameChanged(this, new FilenameArgs(value));
                    this.Save();
                }
            }
        }

        public string DevantechFilename {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.DevantechFilename;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.DevantechFilename != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.DevantechFilename = value;
                    this.Save();
                }
            }
        }

        public string ContentFilename {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.ContentFilename;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.ContentFilename != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.ContentFilename = value;
                    this.on_ContentFilenameChanged(this, new FilenameArgs(value));
                    this.Save();
                }
            }
        }

        public int TimerPositionX {
            get {
                if (this.settingsData == null) return 0;
                else return this.settingsData.TimerPositionX;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerPositionX != value) {
                    this.settingsData.TimerPositionX = value;
                    this.Save();
                }
            }
        }

        public int TimerPositionY {
            get {
                if (this.settingsData == null) return 0;
                else return this.settingsData.TimerPositionY;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerPositionY != value) {
                        this.settingsData.TimerPositionY = value;
                    this.Save();
                }
            }
        }

        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get {
                if (this.settingsData == null) return VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
                return this.settingsData.TimerStyle;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerStyle != value) {
                    this.settingsData.TimerStyle = value;
                    this.Save();
                }
            }
        }

        public int TimerStartTime {
            get {
                if (this.settingsData == null) return 30;
                return this.settingsData.TimerStartTime;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerStartTime != value) {
                    this.settingsData.TimerStartTime = value;
                    this.Save();
                }
            }
        }

        public int TimerAlarmTime1 {
            get {
                if (this.settingsData == null) return -1;
                return this.settingsData.TimerAlarmTime1;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerAlarmTime1 != value) {
                    this.settingsData.TimerAlarmTime1 = value;
                    this.Save();
                }
            }
        }

        public int TimerAlarmTime2 {
            get {
                if (this.settingsData == null) return -1;
                return this.settingsData.TimerAlarmTime2;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerAlarmTime2 != value) {
                    this.settingsData.TimerAlarmTime2 = value;
                    this.Save();
                }
            }
        }

        public int TimerStopTime {
            get {
                if (this.settingsData == null) return 0;
                return this.settingsData.TimerStopTime;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.TimerStopTime != value) {
                    this.settingsData.TimerStopTime = value;
                    this.Save();
                }
            }
        }

        public string Car {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.Car;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.Car != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.Car = value;
                    this.Save();
                }
            }
        }

        public string Gainer {
            get {
                if (this.settingsData == null) return string.Empty;
                return this.settingsData.Gainer;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.Gainer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsData.Gainer = value;
                    this.Save();
                }
            }
        }

        public double XTippKey {
            get {
                if (this.settingsData == null) return 1d;
                return this.settingsData.XTippKey;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.XTippKey != value) {
                    this.settingsData.XTippKey = value;
                    this.Save();
                }
            }
        }

        public int VotingTimerStartTime {
            get {
                if (this.settingsData == null) return 30;
                return this.settingsData.VotingTimerStartTime;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VotingTimerStartTime != value) {
                    this.settingsData.VotingTimerStartTime = value;
                    this.Save();
                }
            }
        }

        public int VotingTimerAlarmTime {
            get {
                if (this.settingsData == null) return -1;
                return this.settingsData.VotingTimerAlarmTime;
            }
            set {
                if (this.settingsData != null &&
                    this.settingsData.VotingTimerAlarmTime != value) {
                    this.settingsData.VotingTimerAlarmTime = value;
                    this.Save();
                }
            }
        }


        public Business() {
            this.settingsData = new Data();
        }

        public void Load() {
            string filename = Path.Combine(ApplicationAttributes.RootPath, Constants.SettingsFilename);
            string subSender = "load";
            if (File.Exists(filename)) {
                try {
                    //XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    //Data settings;
                    //using (StreamReader reader = new StreamReader(filename)) settings = (Data)serializer.Deserialize(reader);
                    XElement xml = XElement.Load(filename);
                    Data settings = (Data)XmlSerializationHelper.getDeserialization(xml, typeof(Cliparts.SchlagDenStarLive.MainApp.Settings.Data));
                    this.settingsData = settings;
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }

        public void Save() {
            string filename = Path.Combine(ApplicationAttributes.RootPath, Constants.SettingsFilename);
            string subSender = "save";
            try {
                // Dokument speichern
                Data settings = this.settingsData;
                //XmlSerializer serializer = new XmlSerializer(typeof(Data));
                //using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, settings);
                XElement xml = XmlSerializationHelper.getXmlSerialization(settings, "Cliparts.SchlagDenStarLive.MainApp.Settings.Data", typeof(Cliparts.SchlagDenStarLive.MainApp.Settings.Data));
                xml.Save(filename);
            }
            catch (Exception exc) {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
        }

        public event EventHandler<FilenameArgs> IONetFilenameChanged;
        private void on_IONetFilenameChanged(object sender, FilenameArgs e) { if (this.IONetFilenameChanged != null) this.IONetFilenameChanged(sender, e); }

        public event EventHandler<FilenameArgs> ContentFilenameChanged;
        private void on_ContentFilenameChanged(object sender, FilenameArgs e) { if (this.ContentFilenameChanged != null) this.ContentFilenameChanged(sender, e); }
    }
}
