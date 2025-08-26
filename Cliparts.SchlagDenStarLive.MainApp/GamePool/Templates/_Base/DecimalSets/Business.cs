using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalSets {

    public class SingleDecimalSet : INotifyPropertyChanged {

        #region Properties

        private Single value = 0;
        public Single Value {
            get { return this.value; }
            set {
                if (this.value != value) {
                    if (value < 0) value = 0;
                    this.value = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.DecimalSets.ValueStates status = VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Idle;
        public VentuzScenes.GamePool._Modules.DecimalSets.ValueStates Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool borderIsSet;
        public bool BorderIsSet {
            get { return this.borderIsSet; }
            set {
                if (this.borderIsSet != value) {
                    this.borderIsSet = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDecimalSet() { }
        public SingleDecimalSet(
            Single value,
            VentuzScenes.GamePool._Modules.DecimalSets.ValueStates status,
            bool borderIsSet) {
            this.Value = value;
            this.Status = status;
            this.BorderIsSet = borderIsSet;
        }

        public void Reset() {
            this.Value = 0;
            this.Status = VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Idle;
            this.BorderIsSet = false;
        }

        public void Clone(
            SingleDecimalSet set) {
            if (set is SingleDecimalSet) {
                this.Value = set.Value;
                this.Status = set.Status;
                this.BorderIsSet = set.BorderIsSet;
            }
            else this.Reset();
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

    public class Business : _Base.Business {

        #region Properties

        private int decimalSetsPositionX = 0;
        public int DecimalSetsPositionX {
            get { return this.decimalSetsPositionX; }
            set {
                if (this.decimalSetsPositionX != value) {
                    this.decimalSetsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetDecimalSets();
                }
            }
        }

        private int decimalSetsPositionY = 0;
        public int DecimalSetsPositionY {
            get { return this.decimalSetsPositionY; }
            set {
                if (this.decimalSetsPositionY != value) {
                    this.decimalSetsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetDecimalSets();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.DecimalSets.Styles decimalSetsStyle = VentuzScenes.GamePool._Modules.DecimalSets.Styles.SixSets;
        public VentuzScenes.GamePool._Modules.DecimalSets.Styles DecimalSetsStyle {
            get { return this.decimalSetsStyle; }
            set {
                if (this.decimalSetsStyle != value) {
                    this.decimalSetsStyle = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("DecimalSetsCount");
                    this.Vinsert_SetDecimalSets();
                    this.checkBestValue();
                }
            }
        }

        public int DecimalSetsCount {
            get {
                int count = 0;
                switch (this.DecimalSetsStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.Styles.FiveSets:
                        count = 5;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.Styles.SixSets:
                        count = 6;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.Styles.SevenSets:
                        count = 7;
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.DecimalSets.Styles.EightSets:
                    default:
                        count = 8;
                        break;
                }
                return count;
            }
        }

        public const int DecimalSetsCountMax = 8;

        protected List<SingleDecimalSet> leftPlayerDecimalSets = new List<SingleDecimalSet>();
        public SingleDecimalSet[] LeftPlayerDecimalSets {
            get {
                this.fillDecimalSets();
                return this.leftPlayerDecimalSets.ToArray(); 
            }
            set {
                this.fillDecimalSets();
                for (int i = 0; i < DecimalSetsCountMax; i++) {
                    if (value is SingleDecimalSet[] &&
                        value.Length > i) this.leftPlayerDecimalSets[i].Clone(value[i]);
                    else this.leftPlayerDecimalSets[i].Reset();
                }
                this.checkBestValue();
            }
        }

        protected List<SingleDecimalSet> rightPlayerDecimalSets = new List<SingleDecimalSet>();
        public SingleDecimalSet[] RightPlayerDecimalSets {
            get { return this.rightPlayerDecimalSets.ToArray(); }
            set {
                for (int i = 0; i < DecimalSetsCountMax; i++) {
                    if (value is SingleDecimalSet[] &&
                        value.Length > i) this.rightPlayerDecimalSets[i].Clone(value[i]);
                    else this.rightPlayerDecimalSets[i].Reset();
                }
                this.checkBestValue();
            }
        }

        #endregion


        #region Funktionen

        public Business() {
            this.fillDecimalSets();
            this.checkBestValue();
        }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {
            this.fillDecimalSets();
            this.checkBestValue();
        }

        public override void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Devantech.Controller devantechHandler,
            Content.Gameboard.Business gameboard,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene,
            VentuzScenes.Host.Business hostMasterScene,
            VentuzScenes.Player.Business leftPlayerMasterScene,
            VentuzScenes.Player.Business rightPlayerMasterScene,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {

            base.Pose(syncContext,
                midiHandler, buzzerHandler, ambHandler, devantechHandler, gameboard,
                insertMasterScene, fullscreenMasterScene, hostMasterScene, leftPlayerMasterScene, rightPlayerMasterScene, previewPipe);
        }

        public override void Dispose() {
            base.Dispose();
            this.clearDecimalSets();
        }

        public override void ResetData() {
            base.ResetData();
            foreach (SingleDecimalSet item in this.leftPlayerDecimalSets) item.Reset();
            foreach (SingleDecimalSet item in this.rightPlayerDecimalSets) item.Reset();
        }

        protected void fillDecimalSets() {
            while (this.leftPlayerDecimalSets.Count < DecimalSetsCountMax) {
                SingleDecimalSet item = new SingleDecimalSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.leftPlayerDecimalSets.Add(item);
            }
            while (this.rightPlayerDecimalSets.Count < DecimalSetsCountMax) {
                SingleDecimalSet item = new SingleDecimalSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.rightPlayerDecimalSets.Add(item);
            }
        }

        protected void clearDecimalSets() {
            foreach (SingleDecimalSet item in this.leftPlayerDecimalSets) item.PropertyChanged -= this.set_PropertyChanged;
            foreach (SingleDecimalSet item in this.rightPlayerDecimalSets) item.PropertyChanged -= this.set_PropertyChanged;
        }

        protected void checkBestValue() {
            int topValidValuesCount = 0;
            int bottomValidValuesCount = 0;
            Single bestValue = Single.MinValue;
            foreach (SingleDecimalSet item in this.leftPlayerDecimalSets) {
                if (item.Status == VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid) {
                    topValidValuesCount++;
                    if (bestValue < item.Value) bestValue = item.Value;
                }
            }
            foreach (SingleDecimalSet item in this.rightPlayerDecimalSets) {
                if (item.Status == VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid) {
                    bottomValidValuesCount++;
                    if (bestValue < item.Value) bestValue = item.Value;
                }
            }
            bool setBestValue = topValidValuesCount > 0 && bottomValidValuesCount > 0;
            foreach (SingleDecimalSet item in this.leftPlayerDecimalSets) {
                if (setBestValue &&
                    item.Status == VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid) item.BorderIsSet = item.Value == bestValue;
                else item.BorderIsSet = false;
            }
            foreach (SingleDecimalSet item in this.rightPlayerDecimalSets) {
                if (setBestValue &&
                    item.Status == VentuzScenes.GamePool._Modules.DecimalSets.ValueStates.Valid) item.BorderIsSet = item.Value == bestValue;
                else item.BorderIsSet = false;
            }
        }

        public virtual void Vinsert_DecimalSetsIn() { }
        public virtual void Vinsert_DecimalSetsIn(VentuzScenes.GamePool._Modules.DecimalSets scene) {
            this.Vinsert_SetDecimalSets(scene);
            if (scene is VentuzScenes.GamePool._Modules.DecimalSets) scene.ToIn();
        }
        public virtual void Vinsert_SetDecimalSets() { }
        public virtual void Vinsert_SetDecimalSets(VentuzScenes.GamePool._Modules.DecimalSets scene) { this.Vinsert_SetDecimalSets(scene, this.LeftPlayerDecimalSets, this.RightPlayerDecimalSets); }
        public virtual void Vinsert_SetDecimalSets(
            VentuzScenes.GamePool._Modules.DecimalSets scene,
            SingleDecimalSet[] leftPlayerDecimalSets,
            SingleDecimalSet[] rightPlayerDecimalSets) {
            if (scene is VentuzScenes.GamePool._Modules.DecimalSets) {
                scene.SetPositionX(this.DecimalSetsPositionX);
                scene.SetPositionY(this.DecimalSetsPositionY);
                scene.SetStyle(this.DecimalSetsStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                int id = 1;
                foreach (_Base.DecimalSets.SingleDecimalSet set in leftPlayerDecimalSets) {
                    scene.SetTopStatus(id, set.Status);
                    scene.SetTopValue(id, set.Value);
                    scene.SetTopBorder(id, set.BorderIsSet);
                    id++;
                }
                id = 1;
                foreach (_Base.DecimalSets.SingleDecimalSet set in rightPlayerDecimalSets) {
                    scene.SetBottomStatus(id, set.Status);
                    scene.SetBottomValue(id, set.Value);
                    scene.SetBottomBorder(id, set.BorderIsSet);
                    id++;
                }
            }
        }
        public virtual void Vinsert_DecimalSetsOut() { }
        public virtual void Vinsert_DecimalSetsOut(VentuzScenes.GamePool._Modules.DecimalSets scene) {
            this.Vinsert_SetDecimalSets(scene);
            if (scene is VentuzScenes.GamePool._Modules.DecimalSets) scene.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_set_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_set_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Status" || e.PropertyName == "Value") this.checkBestValue();
            }
        }

        #endregion
    }
}
