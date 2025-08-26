using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddEstimations {

    public class Player : _Base {

        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Questions.Reset
        //	[Path]=.Questions.Start
        //	[Path]=.Questions.Lock
        //	[Path]=.Questions.Release
        //	[Path]=.Questions.OKPressed
        //	[Path]=.Questions.Question_1.Text
        //	[Path]=.Questions.Question_1.Amount
        //	[Path]=.Questions.Question_1.AmountChanged
        //	[Path]=.Questions.Question_2.Text
        //	[Path]=.Questions.Question_2.Amount
        //	[Path]=.Questions.Question_2.AmountChanged
        //	[Path]=.Questions.Question_3.Text
        //	[Path]=.Questions.Question_3.Amount
        //	[Path]=.Questions.Question_3.AmountChanged

        #region Properties

        private const string sceneID = "project/gamepool/addestimations/player";

        private string textQuestion_1 = string.Empty;
        public string TextQuestion_1 {
            get { return this.textQuestion_1; }
            protected set {
                if (this.textQuestion_1 != value) {
                    if (string.IsNullOrEmpty(value)) this.textQuestion_1 = string.Empty;
                    else this.textQuestion_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int amountQuestion_1 = 0;
        public int AmountQuestion_1 {
            get { return this.amountQuestion_1; }
            protected set {
                if (this.amountQuestion_1 != value) {
                    this.amountQuestion_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string textQuestion_2 = string.Empty;
        public string TextQuestion_2 {
            get { return this.textQuestion_2; }
            protected set {
                if (this.textQuestion_2 != value) {
                    if (string.IsNullOrEmpty(value)) this.textQuestion_2 = string.Empty;
                    else this.textQuestion_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int amountQuestion_2 = 0;
        public int AmountQuestion_2 {
            get { return this.amountQuestion_2; }
            protected set {
                if (this.amountQuestion_2 != value) {
                    this.amountQuestion_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string textQuestion_3 = string.Empty;
        public string TextQuestion_3 {
            get { return this.textQuestion_3; }
            protected set {
                if (this.textQuestion_3 != value) {
                    if (string.IsNullOrEmpty(value)) this.textQuestion_3 = string.Empty;
                    else this.textQuestion_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int amountQuestion_3 = 0;
        public int AmountQuestion_3 {
            get { return this.amountQuestion_3; }
            protected set {
                if (this.amountQuestion_3 != value) {
                    this.amountQuestion_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void ResetQuestion() { this.SetDataItemTrigger(".Questions.Reset"); }
        public void StartQuestion() { this.SetDataItemTrigger(".Questions.Start"); }
        public void LockQuestion() { this.SetDataItemTrigger(".Questions.Lock"); }
        public void ReleaseQuestion() { this.SetDataItemTrigger(".Questions.Release"); }

        public void SetQuestionText(int id, string value) { this.SetDataItemValue(string.Format(".Questions.Question_{0}.Text", id.ToString()), value); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Questions.Question_1.Text") this.TextQuestion_1 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_1.Amount") this.AmountQuestion_1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_1.AmountChanged") this.AmountQuestion_1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_2.Text") this.TextQuestion_2 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_2.Amount") this.AmountQuestion_2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_2.AmountChanged") this.AmountQuestion_2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_3.Text") this.TextQuestion_3 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_3.Amount") this.AmountQuestion_3 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_3.AmountChanged") this.AmountQuestion_3 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.OKPressed") this.on_OKButtonPressed(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Questions.Question_1.Text") this.TextQuestion_1 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_1.Amount") this.AmountQuestion_1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_2.Text") this.TextQuestion_2 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_2.Amount") this.AmountQuestion_2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Questions.Question_3.Text") this.TextQuestion_3 = e.Value.ToString();
                else if (e.Path == ".Questions.Question_3.Amount") this.AmountQuestion_3 = Convert.ToInt32(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler OKButtonPressed;
        private void on_OKButtonPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKButtonPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
