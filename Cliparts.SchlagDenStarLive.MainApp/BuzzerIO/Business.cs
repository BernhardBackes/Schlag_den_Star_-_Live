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

using Cliparts.IOnet.IOUnit.IONbase;

namespace Cliparts.SchlagDenStarLive.MainApp.BuzzerIO {

    public enum BuzzerUnitStates {
        NotAvailable,
        Missing,
        Disconnected,
        Connecting,
        Connected,
        Locked,
        BuzzerMode,
        EventMode
    }

    public enum VeloUnitStates {
        NotAvailable,
        Missing,
        Disconnected,
        Connecting,
        Connected,
        Locked,
        SingleMode,
        DoubleMode
    }


    public class Business : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        public const int BuzzerChannelMin = 1;
        public const int BuzzerChannelMax = 8;

        private SynchronizationContext syncContext;

        private Tools.NetContact.XPort.Tools xPortTools;
        private IOnet.IOUnit.ClassUnitControl ioNet;

        private List<InfoParam> unitInfoList = new List<InfoParam>();
        public InfoParam[] UnitInfoList { get { return this.unitInfoList.ToArray(); } }

        private Settings.Business settings;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            Settings.Business settings) {

            this.syncContext = syncContext;

            this.xPortTools = new Tools.NetContact.XPort.Tools();

            this.ioNet = new IOnet.IOUnit.ClassUnitControl(syncContext, this.xPortTools);

            this.ioNet.BuzUnit_Buzzer += this.on_BuzUnit_Buzzered;
            this.ioNet.BuzUnit_InputChannelChanged += this.on_BuzUnit_InputChannelChanged;
            this.ioNet.BuzUnit_InputChannelRequest += this.on_BuzUnit_InputChannelRequest;
            this.ioNet.BuzUnit_WorkModeChanged += this.on_BuzUnit_WorkmodeChanged;
            this.ioNet.BuzUnit_WorkModeRequest += this.on_BuzUnit_WorkmodeRequest;

            this.ioNet.VeloUnit_PassingDirectionChanged += this.on_VeloUnit_PassingDirectionChanged;
            this.ioNet.VeloUnit_PassingDirectionRequest += this.on_VeloUnitPassing_DirectionRequest;
            this.ioNet.VeloUnit_PassingsCountABChanged += this.on_VeloUnit_PassingsCountABChanged;
            this.ioNet.VeloUnit_PassingsCountBAChanged += this.on_VeloUnit_PassingsCountBAChanged;
            this.ioNet.VeloUnit_PassingsCountRequest += this.on_VeloUnit_PassingsCountRequest;
            this.ioNet.VeloUnit_TimespanABChanged += this.on_VeloUnit_TimespanABChanged;
            this.ioNet.VeloUnit_TimespanBAChanged += this.on_VeloUnit_TimespanBAChanged;
            this.ioNet.VeloUnit_TimespanDblChanged += this.on_VeloUnit_TimespanDblChanged;
            this.ioNet.VeloUnit_TimespansRequest += this.on_VeloUnit_TimespansRequest;
            this.ioNet.VeloUnit_WorkModeChanged += this.on_VeloUnit_WorkModeChanged;
            this.ioNet.VeloUnit_WorkModeRequest += this.on_VeloUnit_WorkModeRequest;

            this.ioNet.FilenameChanged += this.on_Unit_FilenameChanged;
            this.ioNet.UnitConnectionStatusChanged += this.on_Unit_ConnectionStatusChanged;
            this.ioNet.UnitConnectionStatusRequest += this.on_Unit_ConnectionStatusRequest;
            this.ioNet.UnitInfoListChanged += this.on_Unit_InfoListChanged;

            this.settings = settings;
        }

        public void Dispose() {
            this.DisonnectAllUnits();

            this.ioNet.BuzUnit_Buzzer -= this.on_BuzUnit_Buzzered;
            this.ioNet.BuzUnit_InputChannelChanged -= this.on_BuzUnit_InputChannelChanged;
            this.ioNet.BuzUnit_InputChannelRequest -= this.on_BuzUnit_InputChannelRequest;
            this.ioNet.BuzUnit_WorkModeChanged -= this.on_BuzUnit_WorkmodeChanged;
            this.ioNet.BuzUnit_WorkModeRequest -= this.on_BuzUnit_WorkmodeRequest;

            this.ioNet.VeloUnit_PassingDirectionChanged -= this.on_VeloUnit_PassingDirectionChanged;
            this.ioNet.VeloUnit_PassingDirectionRequest -= this.on_VeloUnitPassing_DirectionRequest;
            this.ioNet.VeloUnit_PassingsCountABChanged -= this.on_VeloUnit_PassingsCountABChanged;
            this.ioNet.VeloUnit_PassingsCountBAChanged -= this.on_VeloUnit_PassingsCountBAChanged;
            this.ioNet.VeloUnit_PassingsCountRequest -= this.on_VeloUnit_PassingsCountRequest;
            this.ioNet.VeloUnit_TimespanABChanged -= this.on_VeloUnit_TimespanABChanged;
            this.ioNet.VeloUnit_TimespanBAChanged -= this.on_VeloUnit_TimespanBAChanged;
            this.ioNet.VeloUnit_TimespanDblChanged -= this.on_VeloUnit_TimespanDblChanged;
            this.ioNet.VeloUnit_TimespansRequest -= this.on_VeloUnit_TimespansRequest;
            this.ioNet.VeloUnit_WorkModeChanged -= this.on_VeloUnit_WorkModeChanged;
            this.ioNet.VeloUnit_WorkModeRequest -= this.on_VeloUnit_WorkModeRequest;

            this.ioNet.FilenameChanged -= this.on_Unit_FilenameChanged;
            this.ioNet.UnitConnectionStatusChanged -= this.on_Unit_ConnectionStatusChanged;
            this.ioNet.UnitConnectionStatusRequest -= this.on_Unit_ConnectionStatusRequest;
            this.ioNet.UnitInfoListChanged -= this.on_Unit_InfoListChanged;
        }

        public void RequestUnitConnectionStatus(
            string unitName) {
            this.ioNet.Execute(unitName, new RequestConnectionStatus());
        }

        public void RequestUnitWorkMode(
            string unitName) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuz_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.RequestWorkMode());
                    else if (item.Type == Types.IONbuz_I8W8) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.RequestWorkMode());
                    else if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.RequestWorkMode());
                    else if (item.Type == Types.IONvelo_S1C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONvelo.RequestWorkMode());
                    break;
                }
            }
        }

        public void RequestUnitInputChannels(
            string unitName) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuz_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.RequestInputChannels());
                    else if (item.Type == Types.IONbuz_I8W8) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.RequestInputChannels());
                    else if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.RequestInputChannels());
                    break;
                }
            }
        }

        public void LoadUnitList() {
            this.ioNet.Execute(new IOnet.IOUnit.LoadUnits(this.settings.IONetFilename));
        }

        public void ConnectAllUnits() {
            foreach (InfoParam item in this.unitInfoList) this.ioNet.Execute(item.Name, new Connect(false));
        }
        public void DisonnectAllUnits() {
            foreach (InfoParam item in this.unitInfoList) this.ioNet.Execute(item.Name, new Disconnect());
        }
        public void GetBuzzerUnitConnectionStatus(
            string unitName) {
            this.ioNet.Execute(unitName, new IOnet.IOUnit.IONbase.RequestConnectionStatus());
        }

        public void GetBuzzerUnitWorkmode(
            string unitName) {
            this.ioNet.Execute(unitName, new IOnet.IOUnit.IONbuzDX.RequestWorkMode());
        }

        public void SetAllInputMasks(
            bool[] inputMask) {
            foreach (InfoParam item in this.unitInfoList) this.SetInputMask(item.Name, inputMask);
        }
        public void ReleaseAllBuzzer(
            IOnet.IOUnit.IONbuz.WorkModes mode) {
            foreach (InfoParam item in this.unitInfoList) this.ReleaseBuzzer(item.Name, mode);
        }
        public void LockAllBuzzer() {
            foreach (InfoParam item in this.unitInfoList) this.LockBuzzer(item.Name);
        }

        public void SetInputMask(
            string unitName,
            bool[] inputMask) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuz_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetInputMask(inputMask));
                    else if (item.Type == Types.IONbuz_I8W8) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetInputMask(inputMask));
                    else if (item.Type == Types.IONbuz_I32C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetInputMask(inputMask));
                    else if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetInputMask(inputMask));
                    break;
                }
            }
        }
        public void ReleaseBuzzer(
            string unitName,
            IOnet.IOUnit.IONbuz.WorkModes mode) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuz_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(mode));
                    else if (item.Type == Types.IONbuz_I8W8) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(mode));
                    else if (item.Type == Types.IONbuz_I32C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(mode));
                    else if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetWorkMode(mode));
                    else if (item.Type == Types.IONvelo_S1C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetWorkMode(mode));
                }
            }
        }
        public void LockBuzzer(
            string unitName) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuz_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(IOnet.IOUnit.IONbuz.WorkModes.LOCK));
                    else if (item.Type == Types.IONbuz_I8W8) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(IOnet.IOUnit.IONbuz.WorkModes.LOCK));
                    else if (item.Type == Types.IONbuz_I32C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuz.SetWorkMode(IOnet.IOUnit.IONbuz.WorkModes.LOCK));
                    else if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetWorkMode(IOnet.IOUnit.IONbuz.WorkModes.LOCK));
                    else if (item.Type == Types.IONvelo_S1C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetWorkMode(IOnet.IOUnit.IONbuz.WorkModes.LOCK));
                    break;
                }
            }
        }

        public void SetDMXMode(
            string unitName,
            IOnet.IOUnit.IONbuzDX.DMXBuzzerModes mode) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetDMXBuzzerMode(mode));
                    break;
                }
            }
        }

        public void SetDMXSettings(
            string unitName,
            byte offValue,
            byte onValue) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetDMXSettings(offValue, onValue));
                    break;
                }
            }
        }

        public void SetDMXChannel(
            string unitName,
            int startAddress,
            byte[] valueList) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName &&
                    valueList is byte[]) {
                    if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetDMXOutput(startAddress, valueList));
                    break;
                }
            }
        }

        public void SetDMXChannel(
            string unitName,
            IOnet.IOUnit.IONbuzDX.DMXStates status) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetDMXOutput(status));
                    break;
                }
            }
        }

        public void SetDMXList(
            string unitName,
            int listIndex,
            byte[] valueList) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName &&
                    valueList is byte[]) {
                        if (item.Type == Types.IONbuzDX_I8C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONbuzDX.SetDMXList(listIndex, valueList));
                    break;
                }
            }
        }


        public void GetVeloUnitWorkmode(
            string unitName) {
            this.ioNet.Execute(unitName, new IOnet.IOUnit.IONvelo.RequestWorkMode());
        }
        public void ReleaseAllVelo(
            IOnet.IOUnit.IONvelo.WorkModes mode) {
            foreach (InfoParam item in this.unitInfoList) this.ReleaseVelo(item.Name, mode);
        }
        public void LockAllVelo() {
            foreach (InfoParam item in this.unitInfoList) this.LockVelo(item.Name);
        }
        public void ReleaseVelo(
            string unitName,
            IOnet.IOUnit.IONvelo.WorkModes mode) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONvelo_S1C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONvelo.SetWorkMode(mode));
                    break;
                }
            }
        }
        public void LockVelo(
            string unitName) {
            foreach (InfoParam item in this.unitInfoList) {
                if (item.Name == unitName) {
                    if (item.Type == Types.IONvelo_S1C) this.ioNet.Execute(item.Name, new IOnet.IOUnit.IONvelo.SetWorkMode(IOnet.IOUnit.IONvelo.WorkModes.LOCK));
                    break;
                }
            }
        }

        public void ShowForm() { this.ioNet.Execute(new IOnet.IOUnit.ShowForm()); }

        #endregion


        #region Events.Outgoing

        public event EventHandler<string> FilenameChanged;
        private void on_FilenameChanged(object sender, string e) { Helper.raiseEvent(sender, this.FilenameChanged, e); }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }

        public event EventHandler<IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs> BuzUnit_Buzzered;
        private void on_BuzUnit_Buzzered(object sender, Cliparts.IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) { Helper.raiseEvent(sender, this.BuzUnit_Buzzered, e); }

        public event EventHandler<IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs> BuzUnit_InputChannelChanged;
        private void on_BuzUnit_InputChannelChanged(object sender, Cliparts.IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs e) { Helper.raiseEvent(sender, this.BuzUnit_InputChannelChanged, e); }

        public event EventHandler<IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs> BuzUnit_InputChannelRequest;
        private void on_BuzUnit_InputChannelRequest(object sender, Cliparts.IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs e) { Helper.raiseEvent(sender, this.BuzUnit_InputChannelRequest, e); }

        public event EventHandler<IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs> BuzUnit_WorkmodeChanged;
        private void on_BuzUnit_WorkmodeChanged(object sender, Cliparts.IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) { Helper.raiseEvent(sender, this.BuzUnit_WorkmodeChanged, e); }

        public event EventHandler<IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs> BuzUnit_WorkmodeRequest;
        private void on_BuzUnit_WorkmodeRequest(object sender, Cliparts.IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) { Helper.raiseEvent(sender, this.BuzUnit_WorkmodeRequest, e); }


        /// <summary>
        /// Wird ausgelöst wenn der Arbeitsmodus einer VeloUnit beim Controller angefragt wurde
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.WorkModeParam_EventArgs> VeloUnit_WorkModeRequest;
        private void on_VeloUnit_WorkModeRequest(object sender, IOnet.IOUnit.IONvelo.WorkModeParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_WorkModeRequest, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich der Arbeitsmodus einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.WorkModeParam_EventArgs> VeloUnit_WorkModeChanged;
        private void on_VeloUnit_WorkModeChanged(object sender, IOnet.IOUnit.IONvelo.WorkModeParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_WorkModeChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn die Durchgangsrichtung einer VeloUnit beim Controller angefragt wurde
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.PassingDirectionParam_EventArgs> VeloUnit_PassingDirectionRequest;
        private void on_VeloUnitPassing_DirectionRequest(object sender, IOnet.IOUnit.IONvelo.PassingDirectionParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_PassingDirectionRequest, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangsrichtung einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.PassingDirectionParam_EventArgs> VeloUnit_PassingDirectionChanged;
        private void on_VeloUnit_PassingDirectionChanged(object sender, IOnet.IOUnit.IONvelo.PassingDirectionParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_PassingDirectionChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn die Durchgangsrichtungszähler (AB und BA)  einer VeloUnit beim Controller angefragt wurde
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.PassingsCountParam_EventArgs> VeloUnit_PassingsCountRequest;
        private void on_VeloUnit_PassingsCountRequest(object sender, IOnet.IOUnit.IONvelo.PassingsCountParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_PassingsCountRequest, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangszähler in Richtung AB einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.CounterParam_EventArgs> VeloUnit_PassingsCountABChanged;
        private void on_VeloUnit_PassingsCountABChanged(object sender, IOnet.IOUnit.IONvelo.CounterParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_PassingsCountABChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangszähler in Richtung BA einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.CounterParam_EventArgs> VeloUnit_PassingsCountBAChanged;
        private void on_VeloUnit_PassingsCountBAChanged(object sender, IOnet.IOUnit.IONvelo.CounterParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_PassingsCountBAChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn die Durchgangszeiten (AB, BA und Arbeitsmodus Dbl) einer VeloUnit beim Controller angefragt wurde
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.TimespansParam_EventArgs> VeloUnit_TimespansRequest;
        private void on_VeloUnit_TimespansRequest(object sender, IOnet.IOUnit.IONvelo.TimespansParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_TimespansRequest, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangszeit in Richtung AB einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.TimeParam_EventArgs> VeloUnit_TimespanABChanged;
        private void on_VeloUnit_TimespanABChanged(object sender, IOnet.IOUnit.IONvelo.TimeParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_TimespanABChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangszeit in Richtung BA einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.TimeParam_EventArgs> VeloUnit_TimespanBAChanged;
        private void on_VeloUnit_TimespanBAChanged(object sender, IOnet.IOUnit.IONvelo.TimeParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_TimespanBAChanged, e); }

        /// <summary>
        /// Wird ausgelöst wenn sich die Durchgangszeit im Arbeitsmodus Dbl einer VeloUnit geändert hat
        /// </summary>
        public event EventHandler<IOnet.IOUnit.IONvelo.TimeParam_EventArgs> VeloUnit_TimespanDblChanged;
        private void on_VeloUnit_TimespanDblChanged(object sender, IOnet.IOUnit.IONvelo.TimeParam_EventArgs e) { Helper.raiseEvent(sender, this.VeloUnit_TimespanDblChanged, e); }


        public event EventHandler<InfoParamArray_EventArgs> Unit_InfoListChanged;
        private void on_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) { Helper.raiseEvent(sender, this.Unit_InfoListChanged, e); }

        public event EventHandler<ConnectionStatusParam_EventArgs> Unit_ConnectionStatusChanged;
        private void on_Unit_ConnectionStatusChanged(object sender, ConnectionStatusParam_EventArgs e) { Helper.raiseEvent(sender, this.Unit_ConnectionStatusChanged, e); }

        public event EventHandler<ConnectionStatusParam_EventArgs> Unit_ConnectionStatusRequest;
        private void on_Unit_ConnectionStatusRequest(object sender, ConnectionStatusParam_EventArgs e) { Helper.raiseEvent(sender, this.Unit_ConnectionStatusRequest, e); }
        #endregion

        #region Events.Incoming

        private void on_Unit_FilenameChanged(object sender, Tools.Base.StringEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ioNet_FilenameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ioNet_FilenameChanged(object content) {
            Tools.Base.StringEventArgs e = content as Tools.Base.StringEventArgs;
            if (e is Tools.Base.StringEventArgs) this.on_FilenameChanged(this, e.Arg);
        }

        void on_Unit_InfoListChanged(object sender, InfoParamArray_EventArgs e) {
            this.on_UnitInfoListChanged(this, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ioNet_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ioNet_UnitInfoListChanged(object content) {
            InfoParamArray_EventArgs e = content as InfoParamArray_EventArgs;
            if (e is InfoParamArray_EventArgs) {
                this.unitInfoList.Clear();
                this.unitInfoList.AddRange(e.Arg);
            }
        }

        #endregion

    }

}
