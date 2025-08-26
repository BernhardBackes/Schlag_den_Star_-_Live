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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.DecimalAddition {

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

        private VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle;
        public VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool active;
        public bool Active {
            get { return this.active; }
            set {
                if (this.active != value) {
                    this.active = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDecimalSet() { }
        public SingleDecimalSet(
            float value,
            VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates status,
            bool active) {
            this.Value = value;
            this.Status = status;
            this.Active = active;
        }

        public void Reset() {
            this.Value = 0;
            this.Active = false;
        }

        public void Clone(
            SingleDecimalSet set) {
            if (set is SingleDecimalSet) {
                this.Value = set.Value;
                this.Active = set.Active;
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

        private int decimalAdditionPositionX = 0;
        public int DecimalAdditionPositionX {
            get { return this.decimalAdditionPositionX; }
            set {
                if (this.decimalAdditionPositionX != value) {
                    this.decimalAdditionPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetDecimalAddition();
                }
            }
        }

        private int decimalAdditionPositionY = 0;
        public int DecimalAdditionPositionY {
            get { return this.decimalAdditionPositionY; }
            set {
                if (this.decimalAdditionPositionY != value) {
                    this.decimalAdditionPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetDecimalAddition();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.DecimalAddition.Styles decimalAdditionStyle = VentuzScenes.GamePool._Modules.DecimalAddition.Styles.FourSets;
        public VentuzScenes.GamePool._Modules.DecimalAddition.Styles DecimalAdditionStyle {
            get { return this.decimalAdditionStyle; }
            set {
                if (this.decimalAdditionStyle != value) {
                    this.decimalAdditionStyle = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("DecimalAdditionCount");
                    this.calcSums();
                    this.Vinsert_SetDecimalAddition();
                }
            }
        }

        public int DecimalAdditionCount {
            get {
                int count = 0;
                switch (this.DecimalAdditionStyle) {
                    //case VentuzScenes.GamePool._Modules.DecimalAddition.Styles.FiveSets:
                    //    count = 5;
                    //    break;
                    //case VentuzScenes.GamePool._Modules.DecimalAddition.Styles.SixSets:
                    //    count = 6;
                    //    break;
                    case VentuzScenes.GamePool._Modules.DecimalAddition.Styles.FourSets:
                    default:
                        count = 4;
                        break;
                }
                return count;
            }
        }

        public const int DecimalAdditionCountMin = 4;
        public const int DecimalAdditionCountMax = 6;

        private int validSetsCount = 3;
        public int ValidSetsCount {
            get { return this.validSetsCount; }
            set {
                if (this.validSetsCount != value) {
                    if (value < 1) this.validSetsCount = 1;
                    else this.validSetsCount = value;
                    this.on_PropertyChanged();
                    this.calcSums();
                    this.Vinsert_SetDecimalAddition();
                }
            }
        }

        protected List<SingleDecimalSet> leftPlayerDecimalAddition = new List<SingleDecimalSet>();
        public SingleDecimalSet[] LeftPlayerDecimalAddition {
            get {
                this.fillDecimalAddition();
                return this.leftPlayerDecimalAddition.ToArray(); 
            }
            set {
                this.fillDecimalAddition();
                for (int i = 0; i < DecimalAdditionCountMax; i++) {
                    if (value is SingleDecimalSet[] &&
                        value.Length > i) this.leftPlayerDecimalAddition[i].Clone(value[i]);
                    else this.leftPlayerDecimalAddition[i].Reset();
                }
                this.calcSums();
            }
        }

        private Single leftPlayerSum = 0;
        public Single LeftPlayerSum {
            get { return this.leftPlayerSum; }
            private set {
                if (this.leftPlayerSum != value) {
                    this.leftPlayerSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<SingleDecimalSet> rightPlayerDecimalAddition = new List<SingleDecimalSet>();
        public SingleDecimalSet[] RightPlayerDecimalAddition {
            get { return this.rightPlayerDecimalAddition.ToArray(); }
            set {
                for (int i = 0; i < DecimalAdditionCountMax; i++) {
                    if (value is SingleDecimalSet[] &&
                        value.Length > i) this.rightPlayerDecimalAddition[i].Clone(value[i]);
                    else this.rightPlayerDecimalAddition[i].Reset();
                }
                this.calcSums();
            }
        }

        private Single rightPlayerSum = 0;
        public Single RightPlayerSum {
            get { return this.rightPlayerSum; }
            private set {
                if (this.rightPlayerSum != value) {
                    this.rightPlayerSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Business() {
            this.fillDecimalAddition();
            this.calcSums();
        }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {
            this.fillDecimalAddition();
            this.calcSums();
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
            this.clearDecimalAddition();
        }

        public override void ResetData() {
            base.ResetData();
            foreach (SingleDecimalSet item in this.leftPlayerDecimalAddition) item.Reset();
            foreach (SingleDecimalSet item in this.rightPlayerDecimalAddition) item.Reset();
        }

        protected void fillDecimalAddition() {
            while (this.leftPlayerDecimalAddition.Count < DecimalAdditionCountMax) {
                SingleDecimalSet item = new SingleDecimalSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.leftPlayerDecimalAddition.Add(item);
            }
            while (this.rightPlayerDecimalAddition.Count < DecimalAdditionCountMax) {
                SingleDecimalSet item = new SingleDecimalSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.rightPlayerDecimalAddition.Add(item);
            }
        }

        protected void clearDecimalAddition() {
            foreach (SingleDecimalSet item in this.leftPlayerDecimalAddition) item.PropertyChanged -= this.set_PropertyChanged;
            foreach (SingleDecimalSet item in this.rightPlayerDecimalAddition) item.PropertyChanged -= this.set_PropertyChanged;
        }

        protected void calcSums() {
            List<float> validValues = new List<float>();
            float sum = 0;
            foreach (SingleDecimalSet item in this.leftPlayerDecimalAddition) if (item.Active) validValues.Add(item.Value);
            validValues = validValues.OrderByDescending(i => i).ToList();
            if (validValues.Count > this.ValidSetsCount) validValues.RemoveRange(this.ValidSetsCount, validValues.Count - this.ValidSetsCount);
            foreach (SingleDecimalSet item in this.leftPlayerDecimalAddition) {
                if (item.Active) {
                    if (validValues.Contains(item.Value)) {
                        item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid;
                        validValues.Remove(item.Value);
                        sum += item.Value;
                    }
                    else item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Invalid;
                }
                else item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle;
            }
            this.LeftPlayerSum = sum;

            validValues.Clear();
            sum = 0;
            foreach (SingleDecimalSet item in this.rightPlayerDecimalAddition) if (item.Active) validValues.Add(item.Value);
            validValues = validValues.OrderByDescending(i => i).ToList();
            if (validValues.Count > this.ValidSetsCount) validValues.RemoveRange(this.ValidSetsCount, validValues.Count - this.ValidSetsCount);
            foreach (SingleDecimalSet item in this.rightPlayerDecimalAddition) {
                if (item.Active) {
                    if (validValues.Contains(item.Value)) {
                        item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Valid;
                        validValues.Remove(item.Value);
                        sum += item.Value;
                    }
                    else item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Invalid;
                }
                else item.Status = VentuzScenes.GamePool._Modules.DecimalAddition.ValueStates.Idle;
            }
            this.RightPlayerSum = sum;
        }

        public virtual void Vinsert_DecimalAdditionIn() { }
        public virtual void Vinsert_DecimalAdditionIn(VentuzScenes.GamePool._Modules.DecimalAddition scene) {
            this.Vinsert_SetDecimalAddition(scene);
            if (scene is VentuzScenes.GamePool._Modules.DecimalAddition) scene.ToIn();
        }
        public virtual void Vinsert_SetDecimalAddition() { }
        public virtual void Vinsert_SetDecimalAddition(VentuzScenes.GamePool._Modules.DecimalAddition scene) { this.Vinsert_SetDecimalAddition(scene, this.LeftPlayerDecimalAddition, this.LeftPlayerSum, this.RightPlayerDecimalAddition, this.RightPlayerSum); }
        public virtual void Vinsert_SetDecimalAddition(
            VentuzScenes.GamePool._Modules.DecimalAddition scene,
            SingleDecimalSet[] leftPlayerDecimalAddition,
            float leftPlayerSum,
            SingleDecimalSet[] rightPlayerDecimalAddition,
            float rightPlayerSum) {
            if (scene is VentuzScenes.GamePool._Modules.DecimalAddition) {
                scene.SetPositionX(this.DecimalAdditionPositionX);
                scene.SetPositionY(this.DecimalAdditionPositionY);
                scene.SetStyle(this.DecimalAdditionStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopSum(leftPlayerSum);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomSum(rightPlayerSum);
                int id = 1;
                foreach (SingleDecimalSet set in leftPlayerDecimalAddition) {
                    scene.SetTopStatus(id, set.Status);
                    scene.SetTopValue(id, set.Value);
                    id++;
                }
                id = 1;
                foreach (SingleDecimalSet set in rightPlayerDecimalAddition) {
                    scene.SetBottomStatus(id, set.Status);
                    scene.SetBottomValue(id, set.Value);
                    id++;
                }
            }
        }
        public virtual void Vinsert_DecimalAdditionOut() { }
        public virtual void Vinsert_DecimalAdditionOut(VentuzScenes.GamePool._Modules.DecimalAddition scene) {
            this.Vinsert_SetDecimalAddition(scene);
            if (scene is VentuzScenes.GamePool._Modules.DecimalAddition) scene.ToOut();
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
                if (e.PropertyName == "Active" || e.PropertyName == "Status" || e.PropertyName == "Value") this.calcSums();
            }
        }

        #endregion
    }
}
