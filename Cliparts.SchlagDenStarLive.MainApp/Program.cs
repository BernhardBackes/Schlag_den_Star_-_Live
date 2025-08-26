using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp {

    public class Constants {

        internal const string ApplicationName = "SchlagDenStarLive";

        internal const string SettingsFilename = @"Settings.xml";

        internal const string GameHistoryFilename = @"GameHistory.xml";

        internal const bool FlipPlayerColor = false;

        internal static Color ColorEnabled = Color.LawnGreen;
        internal static Color ColorEnabling = Color.Gold;
        internal static Color ColorDisabling = Color.LightSalmon;
        internal static Color ColorDisabled = Color.Salmon;
        internal static Color ColorMissing = Color.Red;
        internal static Color ColorBuzzered = Color.DarkOrange;
        internal static Color ColorSelected = Color.DarkOrange;
        internal static Color ColorDropped = Color.Gray;
        internal static Color ColorLost = Color.LightSalmon;
        internal static Color ColorGotten = Color.Gold;
        internal static Color ColorBank = Color.LawnGreen;
        internal static Color ColorWinner = Color.LawnGreen;
        internal static Color ColorLeftPlayer = Color.LightSalmon;
        internal static Color ColorRightPlayer = Color.LightSkyBlue;
        internal static Color ColorTrue = Color.LawnGreen;
        internal static Color ColorFalse = Color.Salmon;
    }

    static class Program {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationAttributes.Namespace = typeof(Program).Namespace;

            Application.Run(new MainForm());
            Program.Dispose();

        }

        public static void Dispose() {
            Application.Exit();
        }
    }
}
