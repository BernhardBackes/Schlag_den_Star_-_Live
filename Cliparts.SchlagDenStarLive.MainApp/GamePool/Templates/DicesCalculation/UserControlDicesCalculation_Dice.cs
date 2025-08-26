using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DicesCalculation {

    public partial class UserControlDicesCalculation_Dice : UserControl {

        #region Properties

        private Business business;

        private int diceID;

        #endregion


        #region Funktionen

        public UserControlDicesCalculation_Dice() {
            InitializeComponent();
        }

        public void Pose(
            Business business,
            int diceID) {

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.diceID = diceID;

            Binding bind;

            string dataMember = "Dice_" + this.diceID.ToString();

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 1 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_1 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_1; };
            this.pictureBoxDice_01.DataBindings.Add(bind);

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 2 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_2 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_2; };
            this.pictureBoxDice_02.DataBindings.Add(bind);

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 3 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_3 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_3; };
            this.pictureBoxDice_03.DataBindings.Add(bind);

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 4 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_4 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_4; };
            this.pictureBoxDice_04.DataBindings.Add(bind);

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 5 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_5 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_5; };
            this.pictureBoxDice_05.DataBindings.Add(bind);

            bind = new Binding("Image", this.business, dataMember);
            bind.Format += (s, e) => { e.Value = (int)(e.Value) == 6 ? global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_selected_6 : global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.Dice_6; };
            this.pictureBoxDice_06.DataBindings.Add(bind);

        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.pictureBoxDice_01.DataBindings.Clear();
            this.pictureBoxDice_02.DataBindings.Clear();
            this.pictureBoxDice_03.DataBindings.Clear();
            this.pictureBoxDice_04.DataBindings.Clear();
            this.pictureBoxDice_05.DataBindings.Clear();
            this.pictureBoxDice_06.DataBindings.Clear();
        }

        #endregion

        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private void pictureBoxDice_Click(object sender, EventArgs e) {
            int value = 0;
            if (Helper.tryParseIndexFromControl(sender as Control, out value)) {
                if (this.diceID == 1) this.business.Dice_1 = value;
                else if (this.diceID == 2) this.business.Dice_2 = value;
                else if (this.diceID == 3) this.business.Dice_3 = value;
            }
        }

        #endregion

    }
}
