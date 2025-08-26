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

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard {

    public enum PlayerSelection { NotSelected, LeftPlayer, RightPlayer }

    public class Level : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        public ushort ID { get; private set; }

        public ushort Value { get; private set; }

        private PlayerSelection winner = PlayerSelection.NotSelected;
        public PlayerSelection Winner {
            get { return this.winner; }
            set {
                if (this.winner != value) {
                    this.winner = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool enabled = true;
        [NotSerialized]
        public bool Enabled {
            get { return this.enabled; }
            set {
                if (this.enabled != value) {
                    this.enabled = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool selected = false;
        [NotSerialized]
        public bool Selected {
            get { return this.selected; }
            set {
                if (this.selected != value) {
                    this.selected = value;
                    this.on_PropertyChanged();
                    if (value) this.on_SelectionGotten(this, new EventArgs());
                }
            }
        }

        public ushort LeftPlayerValue {
            get {
                if (this.Enabled &&
                    this.Winner == PlayerSelection.LeftPlayer) return this.Value;
                else return 0;
            }
        }

        public ushort RightPlayerValue {
            get {
                if (this.Enabled && 
                    this.Winner == PlayerSelection.RightPlayer) return this.Value;
                else return 0;
            }
        }

        #endregion


        #region Funktionen

        public Level() {}
        public Level(
            ushort id,
            ushort value) {
            this.ID = id;
            this.Value = value;
        }

        public void Dispose() {
        }

        public void Clone(
            Level level) {
            if (level is Level) {
                this.Winner = level.Winner;
            }
        }

        public void Reset() {
            this.Winner = PlayerSelection.NotSelected;
            this.Enabled = true;
        }

        public void ToggleWinner(
            PlayerSelection winner) {
            if (this.Winner == winner) this.Winner = PlayerSelection.NotSelected;
            else this.Winner = winner;
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler SelectionGotten;
        private void on_SelectionGotten(object sender, EventArgs e) { Helper.raiseEvent(sender, this.SelectionGotten, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
