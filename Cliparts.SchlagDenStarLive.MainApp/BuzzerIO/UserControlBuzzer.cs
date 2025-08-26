using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.IOnet.IOUnit.IONbase;

namespace Cliparts.SchlagDenStarLive.MainApp.BuzzerIO {

    public partial class UserControlBuzzer : UserControl {

        #region Properties

        Business business;

        #endregion


        #region Funktionen

        public UserControlBuzzer() {
            InitializeComponent();
        }

        public void Pose(
            Business business) {

            this.BackColor = this.Parent.BackColor;

            this.business = business;
            this.business.Unit_InfoListChanged += this.business_UnitInfoListChanged;

            this.business_UnitInfoListChanged(this, new InfoParamArray_EventArgs(this.business.UnitInfoList));
        }

        public new void Dispose() {
            this.business.Unit_InfoListChanged -= this.business_UnitInfoListChanged;
        }

        #endregion


        #region Events.Incoming

        private void business_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_UnitInfoListChanged(sender, e)));
            else {

                if (e is InfoParamArray_EventArgs) {
                    this.listBoxUnits.BeginUpdate();
                    this.listBoxUnits.Items.Clear();
                    if (e.Arg is InfoParam[] && e.Arg.Length > 0) {
                        foreach (InfoParam item in e.Arg) this.listBoxUnits.Items.Add(item.Name);
                    }
                    this.listBoxUnits.EndUpdate();
                }
            }
        }

        #endregion

        #region Events.Controls

        private void buttonLock_Click(object sender, EventArgs e) { this.business.ConnectAllUnits(); }

        private void buttonRelease_Click(object sender, EventArgs e) { this.business.ReleaseAllBuzzer(IOnet.IOUnit.IONbuz.WorkModes.BUZZER); }

        #endregion

    }
}
