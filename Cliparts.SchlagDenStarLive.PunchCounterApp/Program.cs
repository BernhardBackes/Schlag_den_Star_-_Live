using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Phidget22;

namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    static class Program {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormProgram());
        }
    }

    public class Channel : INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        public int Index { get; private set; }

        private System.Windows.Forms.Timer attachDetachTimer;

        private VoltageRatioInput voltageRatioInput;

        private bool opened = false;
        public bool Opened {
            get { return this.opened; }
            private set {
                if (this.opened != value) {
                    this.opened = value;
                    this.on_PropertyChanged();
                    this.setProperties(value);
                }
            }
        }

        private bool? enabled = null;
        public bool? Enabled {
            get { return this.enabled; }
            private set {
                if (this.enabled != value) {
                    this.enabled = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private BridgeGain? gain = null;
        public BridgeGain? Gain {
            get { return this.gain; }
            private set {
                if (this.gain != value) {
                    this.gain = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? minDataInterval = null;
        public int? MinDataInterval {
            get { return this.minDataInterval; }
            private set {
                if (this.minDataInterval != value) {
                    this.minDataInterval = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? maxDataInterval = null;
        public int? MaxDataInterval {
            get { return this.maxDataInterval; }
            private set {
                if (this.maxDataInterval != value) {
                    this.maxDataInterval = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? dataInterval = null;
        public int? DataInterval {
            get { return this.dataInterval; }
            private set {
                if (this.dataInterval != value) {
                    this.dataInterval = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? minDataRate = null;
        public double? MinDataRate {
            get { return this.minDataRate; }
            private set {
                if (this.minDataRate != value) {
                    this.minDataRate = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? maxDataRate = null;
        public double? MaxDataRate {
            get { return this.maxDataRate; }
            private set {
                if (this.maxDataRate != value) {
                    this.maxDataRate = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? dataRate = null;
        public double? DataRate {
            get { return this.dataRate; }
            private set {
                if (this.dataRate != value) {
                    this.dataRate = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? minVoltageRatio = null;
        public double? MinVoltageRatio {
            get { return this.minVoltageRatio; }
            private set {
                if (this.minVoltageRatio != value) {
                    this.minVoltageRatio = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? maxVoltageRatio = null;
        public double? MaxVoltageRatio {
            get { return this.maxVoltageRatio; }
            private set {
                if (this.maxVoltageRatio != value) {
                    this.maxVoltageRatio = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? voltageRatio = null;
        public double? VoltageRatio {
            get { return this.voltageRatio; }
            private set {
                if (this.voltageRatio != value) {
                    this.voltageRatio = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? minVoltageRatioChangeTrigger = null;
        public double? MinVoltageRatioChangeTrigger {
            get { return this.minVoltageRatioChangeTrigger; }
            private set {
                if (this.minVoltageRatioChangeTrigger != value) {
                    this.minVoltageRatioChangeTrigger = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? maxVoltageRatioChangeTrigger = null;
        public double? MaxVoltageRatioChangeTrigger {
            get { return this.maxVoltageRatioChangeTrigger; }
            private set {
                if (this.maxVoltageRatioChangeTrigger != value) {
                    this.maxVoltageRatioChangeTrigger = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? voltageRatioChangeTrigger = null;
        public double? VoltageRatioChangeTrigger {
            get { return this.voltageRatioChangeTrigger; }
            private set {
                if (this.voltageRatioChangeTrigger != value) {
                    this.voltageRatioChangeTrigger = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? voltage = null;
        public double? Voltage {
            get { return this.voltage; }
            private set {
                if (this.voltage != value) {
                    if (value.HasValue && value.Value < 0) this.voltage = 0;
                    else this.voltage = value;
                    this.on_PropertyChanged();
                    //if (this.voltage.HasValue) Console.WriteLine(voltage.Value.ToString("0.0"));
                }
            }
        }

        private double? voltagePeak = null;
        public double? VoltagePeak {
            get { return this.voltagePeak; }
            private set {
                if (this.voltagePeak != value) {
                    if (value.HasValue && value.Value < 0) this.voltagePeak = 0;
                    else this.voltagePeak = value;
                    this.on_PropertyChanged();
                    //if (this.voltage.HasValue) Console.WriteLine(voltage.Value.ToString("0.0"));
                }
            }
        }

        public enum States { Idle, Growing, Shrinking }

        private States state = States.Idle;
        public States State {
            get { return this.state; }
            private set {
                if (this.state != value) {
                    this.state = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double lastVoltage = 0;

        #endregion


        #region Funktionen

        public Channel(
            SynchronizationContext syncContext,
            int index) {
            this.syncContext = syncContext;

            this.Index = index;

            this.attachDetachTimer = new System.Windows.Forms.Timer();
            this.attachDetachTimer.Interval = Phidget.DefaultTimeout;
            this.attachDetachTimer.Tick += this.AttachDetachTimer_Tick;

            this.voltageRatioInput = new VoltageRatioInput();
            this.voltageRatioInput.Channel = index;
            this.voltageRatioInput.Attach += this.VoltageRatioInput_Attach;
            this.voltageRatioInput.Detach += this.VoltageRatioInput_Detach;
            this.voltageRatioInput.Error += this.VoltageRatioInput_Error;
            this.voltageRatioInput.PropertyChange += this.VoltageRatioInput_PropertyChange;
            this.voltageRatioInput.SensorChange += this.VoltageRatioInput_SensorChange;
            this.voltageRatioInput.VoltageRatioChange += this.VoltageRatioInput_VoltageRatioChange;
        }

        public void Dispose() {
            this.voltageRatioInput.Attach -= this.VoltageRatioInput_Attach;
            this.voltageRatioInput.Detach -= this.VoltageRatioInput_Detach;
            this.voltageRatioInput.Error -= this.VoltageRatioInput_Error;
            this.voltageRatioInput.PropertyChange -= this.VoltageRatioInput_PropertyChange;
            this.voltageRatioInput.SensorChange -= this.VoltageRatioInput_SensorChange;
            this.voltageRatioInput.VoltageRatioChange -= this.VoltageRatioInput_VoltageRatioChange;
            this.Close();
        }

        private void setProperties(
            bool isOpen) {
            if (isOpen) {
                try {
                    this.Enabled = this.voltageRatioInput.BridgeEnabled;
                    this.Gain = this.voltageRatioInput.BridgeGain;
                    this.MinDataInterval = this.voltageRatioInput.MinDataInterval;
                    this.MaxDataInterval = this.voltageRatioInput.MaxDataInterval;
                    this.DataInterval = this.voltageRatioInput.DataInterval;
                    this.MinDataRate = this.voltageRatioInput.MinDataRate;
                    this.MaxDataRate = this.voltageRatioInput.MaxDataRate;
                    this.voltageRatioInput.DataRate = this.voltageRatioInput.MaxDataRate * 0.75;
                    this.DataRate = this.voltageRatioInput.DataRate;
                    this.MinVoltageRatio = this.voltageRatioInput.MinVoltageRatio;
                    this.MaxVoltageRatio = this.voltageRatioInput.MaxVoltageRatio;
                    this.VoltageRatio = this.voltageRatioInput.VoltageRatio;
                    this.MinVoltageRatioChangeTrigger = this.voltageRatioInput.MinVoltageRatioChangeTrigger;
                    this.MaxVoltageRatioChangeTrigger = this.voltageRatioInput.MaxVoltageRatioChangeTrigger;
                    this.VoltageRatioChangeTrigger = this.voltageRatioInput.VoltageRatioChangeTrigger;
                }
                catch (PhidgetException exc) {
                    this.on_Error(string.Format("{0} ({0})", exc.Description, exc.Detail));
                    this.Enabled = null;
                    this.Gain = null;
                    this.MinDataInterval = null;
                    this.MaxDataInterval = null;
                    this.DataInterval = null;
                    this.MinDataRate = null;
                    this.MaxDataRate = null;
                    this.DataRate = null;
                    this.MinVoltageRatio = null;
                    this.MaxVoltageRatio = null;
                    this.VoltageRatio = null;
                    this.MinVoltageRatioChangeTrigger = null;
                    this.MaxVoltageRatioChangeTrigger = null;
                    this.VoltageRatioChangeTrigger = null;
                }
            }
            else {
                this.Enabled = null;
                this.Gain = null;
                this.MinDataInterval = null;
                this.MaxDataInterval = null;
                this.DataInterval = null;
                this.MinDataRate = null;
                this.MaxDataRate = null;
                this.DataRate = null;
                this.MinVoltageRatio = null;
                this.MaxVoltageRatio = null;
                this.VoltageRatio = null;
                this.MinVoltageRatioChangeTrigger = null;
                this.MaxVoltageRatioChangeTrigger = null;
                this.VoltageRatioChangeTrigger = null;
            }
            this.Voltage = null;
            this.VoltagePeak = null;
            this.State = States.Idle;
            this.lastVoltage = 0;
        }

        public bool TryOpen() {
            try {
                if (this.Opened) return true;
                else {
                    this.voltageRatioInput.Open(Phidget.DefaultTimeout);
                    return true;
                }
            }
            catch (PhidgetException exc) {
                this.on_Error(string.Format("{0} ({0})", exc.Description, exc.Detail));
                return false;
            }
        }

        public void SetEnabled(
            bool value) {
            if (this.Opened) this.voltageRatioInput.BridgeEnabled = value;
            else this.Enabled = null;
        }

        public void SetGain(
            BridgeGain value) {

        }

        public void Close() {
            try {
                if (this.Opened) this.voltageRatioInput.Close();
            }
            catch (PhidgetException exc) {
                this.on_Error(string.Format("{0} ({0})", exc.Description, exc.Detail));
            }
            this.Opened = false;
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler<string> Error;
        protected void on_Error(string e) { Helper.raiseEvent(this, this.Error, e); }

        #endregion

        #region Events.Incoming

        private void AttachDetachTimer_Tick(object sender, EventArgs e) {
            this.attachDetachTimer.Stop();
            this.Opened = this.voltageRatioInput.Attached;
        }

        private void VoltageRatioInput_Attach(object sender, Phidget22.Events.AttachEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_VoltageRatioInput_AttachDetach);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void VoltageRatioInput_Detach(object sender, Phidget22.Events.DetachEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_VoltageRatioInput_AttachDetach);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_VoltageRatioInput_AttachDetach(object content) {
            this.attachDetachTimer.Start();
        }

        private void VoltageRatioInput_Error(object sender, Phidget22.Events.ErrorEventArgs e) {
            if (e.Description == "") {

            }
            else {

            }
        }

        private void VoltageRatioInput_PropertyChange(object sender, Phidget22.Events.PropertyChangeEventArgs e) {
        }

        private void VoltageRatioInput_SensorChange(object sender, Phidget22.Events.VoltageRatioInputSensorChangeEventArgs e) {
        }

        private void VoltageRatioInput_VoltageRatioChange(object sender, Phidget22.Events.VoltageRatioInputVoltageRatioChangeEventArgs e) {
            if (this.Opened &&
                this.VoltageRatio.HasValue) {
                try {
                    double v = Math.Round((e.VoltageRatio - this.VoltageRatio.Value) * 100000, 0);
                    if (v <= 0) this.Voltage = 0;
                    else this.Voltage = Math.Round(e.VoltageRatio * 500000, 0);
                    if (this.Voltage.Value > 0) {
                        if (this.lastVoltage > this.Voltage.Value) {
                            this.State = States.Shrinking;
                            this.lastVoltage = this.Voltage.Value;
                        }
                        else if (this.lastVoltage < this.Voltage.Value &&
                            this.State == States.Shrinking) {
                            this.State = States.Growing;
                            this.lastVoltage = this.Voltage.Value;
                        }
                        else {
                            this.State = States.Growing;
                            this.lastVoltage = this.Voltage.Value;
                        }
                        if (this.State == States.Growing) this.VoltagePeak = this.Voltage;
                    }
                    else {
                        this.State = States.Idle;
                        this.lastVoltage = 0;
                    }
                }
                catch (Exception exc) {
                    this.on_Error(exc.Message);
                }
            }
            else {
                this.Voltage = null;
                this.VoltagePeak = null;
                this.State = States.Idle;
                this.lastVoltage = 0;
            }
        }

        #endregion

    }

    public class Player : INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        public string ID { get; private set; }

        public Channel Channel0 { get; private set; }
        public Channel Channel1 { get; private set; }

        private bool enabled = false;
        public bool Enabled {
            get { return this.enabled; }
            set {
                if (this.enabled != value) {
                    this.enabled = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double threshold = 0;
        public double Threshold {
            get { return this.threshold; }
            set {
                if (this.threshold != value) {
                    if (value <= 0.1) this.threshold = 0.1;
                    else this.threshold = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counter = 0;
        public int Counter {
            get { return this.counter; }
            set {
                if (this.counter != value) {
                    this.counter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            string id,
            Channel channel0,
            Channel channel1) {
            this.syncContext = syncContext;

            this.ID = id;

            this.Channel0 = channel0;
            this.Channel0.PropertyChanged += this.Channel_PropertyChanged;

            this.Channel1 = channel1;
            this.Channel1.PropertyChanged += this.Channel_PropertyChanged;
        }

        public void Dispose() {
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler<string> Error;
        protected void on_Error(string e) { Helper.raiseEvent(this, this.Error, e); }

        #endregion

        #region Events.Incoming

        private void Channel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "State" &&
                this.Enabled) {
                if (sender == this.Channel0) {
                    if (this.Channel0.State == Channel.States.Idle &&
                        this.Channel0.VoltagePeak.HasValue &&
                        this.Channel0.VoltagePeak.Value >= this.Threshold) this.Counter++;
                }
                else if (sender == this.Channel1) {
                    if (this.Channel1.State == Channel.States.Idle &&
                        this.Channel1.VoltagePeak.HasValue &&
                        this.Channel1.VoltagePeak.Value >= this.Threshold) this.Counter++;
                }
            }
        }

        #endregion

    }
}
