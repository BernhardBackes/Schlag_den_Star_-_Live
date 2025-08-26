using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Cliparts {

    public enum FileTypes {
        NotAvailable,
        Audio,
        Image,
        Movie
    }

    public class ClipartsColors {
        public static readonly Color TV_ORANGE = System.Drawing.Color.FromArgb(254, 107, 1);
        public static readonly Color TV_RED = System.Drawing.Color.FromArgb(160, 20, 57);
        public static readonly Color DE_LIGHTBLUE = System.Drawing.Color.FromArgb(70, 145, 215);
        public static readonly Color DE_DARKBLUE = System.Drawing.Color.FromArgb(50, 70, 100);
    }

    public class PropertyChangedEventSyncArgs : EventArgs {
        public object Sender { get; private set; }
        public PropertyChangedEventArgs EventArgs { get; private set; }
        public PropertyChangedEventSyncArgs(
            object sender,
            PropertyChangedEventArgs eventArgs) {
            this.Sender = sender;
            this.EventArgs = eventArgs;
        }
    }

    public class FilenameArgs : EventArgs {
        public string Filename { get; private set; }
        public FilenameArgs(
            string filename) {
            if (filename == null) filename = string.Empty;
            this.Filename = filename;
        }
    }

    public class Helper {

        public static void raiseEvent(object sender, EventHandler handler, EventArgs e) {
            var hnd = handler;
            if (hnd != null) hnd(sender, e);
        }

        public static void raiseEvent<T>(object sender, EventHandler<T> handler, T e) {
            var hnd = handler;
            if (hnd != null) hnd(sender, e);
        }

        public static void raisePropertyChangedEvent(object sender, PropertyChangedEventHandler handler, PropertyChangedEventArgs e) {
            var hnd = handler;
            if (hnd != null) hnd(sender, e);
        }

        public static void raisePropertyChangedEvent(object sender, PropertyChangedEventHandler handler, [CallerMemberName]string callerName = "") {
            var hnd = handler;
            if (hnd != null) hnd(sender, new PropertyChangedEventArgs(callerName));
        }

        public static bool controlHasEventHandler(Control control, string eventName) {
            EventHandlerList events =
                (EventHandlerList)
                typeof(Component)
                 .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                 .GetValue(control, null);

            object key = typeof(Control)
                .GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            Delegate handlers = events[key];

            return handlers != null && handlers.GetInvocationList().Any();
        }

        public static string getProductPath() {
            string name = Application.ProductName;
            string productPath = Application.StartupPath;
            int index = productPath.IndexOf(name);
            if (index > 0) productPath = productPath.Substring(0, index) + name;
            return productPath;
        }

        public static float getTruncationToNDecimals(float value, int N)
        {
            decimal valueDecimal = (decimal)value;
            decimal integralValue = Math.Truncate(valueDecimal);
            decimal fraction = valueDecimal - integralValue;

            decimal factor = (decimal)Math.Pow(10, N);
            decimal truncatedFraction = Math.Truncate(fraction * factor) / factor;

            decimal result = integralValue + truncatedFraction;

            return (float)result;
        }

        public static bool tryParseIndexFromName(
            string name,
            out int index) {
            if (!string.IsNullOrEmpty(name) && name.Length >= 2) {
                int indexOf = name.IndexOf("_");
                if (indexOf >= 0) {
                    string indexText = name.Substring(indexOf + 1);
                    if (int.TryParse(indexText, out index)) return true;
                    else {
                        index = -1;
                        return false;
                    }
                }
                else {
                    index = -1;
                    return false;
                }
            }
            else {
                index = -1;
                return false;
            }
        }

        public static bool tryParseIndexFromControl(
            Control control,
            out int index) {
            if (control != null &&
                control.Name.IndexOf("_") < control.Name.Length) {
                int indexOf = control.Name.IndexOf("_");
                string indexText = control.Name.Substring(indexOf + 1);
                if (int.TryParse(indexText, out index)) return true;
                else {
                    index = -1;
                    return false;
                }
            }
            else {
                index = -1;
                return false;
            }
        }

        public static bool tryParseTwoIndicesFromControl(
            Control control,
            out int firstIndex,
            out int secondIndex) {
            if (control != null && control.Name.Length >= 5) {
                string firstIndexText = control.Name.Substring(control.Name.Length - 5, 2);
                string secondIndexText = control.Name.Substring(control.Name.Length - 2);
                if (int.TryParse(firstIndexText, out firstIndex) &&
                    int.TryParse(secondIndexText, out secondIndex)) return true;
                else {
                    firstIndex = -1;
                    secondIndex = -1;
                    return false;
                }
            }
            else {
                firstIndex = -1;
                secondIndex = -1;
                return false;
            }
        }

        public static void convertDoubleToTime(
            double time,
            out int hours,
            out int minutes,
            out int seconds,
            out int milliSeconds) {
            hours = (int)Math.Truncate(time / (60 * 60));
            minutes = (int)Math.Truncate(time / 60 % 60);
            seconds = (int)Math.Truncate(time % 60);
            milliSeconds = Convert.ToInt32(time * 1000 % 1000);
        }

        public static string convertDoubleToClockTimeText(
            double time,
            bool leadingZero) {
            int hours, minutes, seconds, milliSeconds;
            Helper.convertDoubleToTime((double)time, out hours, out minutes, out seconds, out milliSeconds);
            if (time < 0) return "--:--";
            else if (leadingZero) return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
            else return string.Format("{0}:{1}", minutes.ToString(), seconds.ToString("00"));
        }

        public static string convertDoubleToStopwatchTimeText(
            double time,
            bool showMinutes,
            bool leadingZero) {
            if (showMinutes) {
                int hours, minutes, seconds, milliSeconds;
                Helper.convertDoubleToTime((double)time, out hours, out minutes, out seconds, out milliSeconds);
                milliSeconds = milliSeconds / 10;
                if (time < 0) return "--:--.--";
                else if (leadingZero) return string.Format("{0}:{1}.{2}", minutes.ToString("00"), seconds.ToString("00"), milliSeconds.ToString("00"));
                else return string.Format("{0}:{1}.{2}", minutes.ToString(), seconds.ToString("00"), milliSeconds.ToString("00"));
            }
            else {
                if (time < 0) return "--.--";
                else if (leadingZero) return time.ToString("00.00").Replace(",", ".");
                else return time.ToString("0.00").Replace(",", ".");
            }
        }

        public static void setControlBackColor(
            Control control) {
            setControlBackColor(control, false, Color.LawnGreen);
        }
        public static void setControlBackColor(
            Control control,
            bool highlight,
            System.Drawing.Color highlightColor) {
            if (control is Control) {
                if (control.Enabled) {
                    if (highlight) control.BackColor = highlightColor;
                    else if (control is Button) ((Button)control).UseVisualStyleBackColor = true;
                    else control.BackColor = SystemColors.Control;
                }
                else control.BackColor = SystemColors.ControlDark;
            }
        }

        public static string removeTextInBrackets(
            string text) {
            int index = text.IndexOf('(');
            if (index > 0) text = text.Substring(0, index).Trim();
            return text;
        }

        public static string getSettingsPath(
            string startupPath,
            string productName) {
            string path = startupPath;
            int index = startupPath.IndexOf(productName);
            if (index >= 0) {
                int length = index + productName.Length;
                if (length <= startupPath.Length) path = startupPath.Substring(0, length);
                if (!Directory.Exists(path)) path = startupPath;
            }
            return path;
        }

        public static string getContentPath(
            string startupPath,
            string productName) {
            string path = startupPath;
            string[] productNameItems = productName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (productNameItems.Length > 0
                && startupPath.IndexOf(productNameItems[0]) >= 0) {
                int index = startupPath.IndexOf(productNameItems[0]);
                path = Path.Combine(startupPath.Substring(0, index), "content");
                if (!Directory.Exists(path)) path = startupPath;
            }
            return path;
        }

        public static FileTypes getFileTypeFromMediaFile(
            string filename) {
            if (String.IsNullOrEmpty(filename)) return FileTypes.NotAvailable;
            else {
                try {
                    string[] audioExtension = new string[] { ".aac", ".aiff", ".au", ".flac", ".m4a", ".mp3", ".opus", ".wav" };
                    string[] imageExtension = new string[] { ".tiff", ".bmp", ".gif", ".msrle", ".png", ".tga", ".raw", ".dds", ".jpg" };
                    string[] movieExtension = new string[] { ".avi", ".mpg", ".asf", ".wmv", ".mov", ".mp4", ".mkv", ".mk3d", ".flv", ".m4v", ".mxf" };
                    string extension = Path.GetExtension(filename).ToLower();
                    if (audioExtension.Contains(extension)) return FileTypes.Audio;
                    else if (imageExtension.Contains(extension)) return FileTypes.Image;
                    else if (movieExtension.Contains(extension)) return FileTypes.Movie;
                    else return FileTypes.NotAvailable;
                }
                catch (Exception) {
                    return FileTypes.NotAvailable;
                }
            }
        }

        public static Image getThumbnailFromMediaFile(
            string filename)
        {
            return getThumbnailFromMediaFile(filename, 1, 0.5f);
        }
        public static Image getThumbnailFromMediaFile(
            string filename,
            double scaling)
        {
            return getThumbnailFromMediaFile(filename, scaling, 0.5f);
        }
        public static Image getThumbnailFromMediaFile(
            string filename,
            float frameTime)
        {
            return getThumbnailFromMediaFile(filename, 1, frameTime);
        }
        public static Image getThumbnailFromMediaFile(
            string filename,
            double scaling,
            float frameTime)
        {
            FileTypes type = getFileTypeFromMediaFile(filename);
            if (File.Exists(filename))
            {
                try
                {
                    MemoryStream ms = null;
                    switch (type)
                    {
                        case FileTypes.Image:
                            ms = new MemoryStream(File.ReadAllBytes(filename));
                            break;
                        case FileTypes.Movie:
                            NReco.VideoInfo.FFProbe ffprobe = new NReco.VideoInfo.FFProbe();
                            NReco.VideoInfo.MediaInfo info = ffprobe.GetMediaInfo(filename);
                            float duration = Convert.ToSingle(info.Duration.TotalSeconds);
                            NReco.VideoConverter.FFMpegConverter ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                            ms = new MemoryStream();
                            if (frameTime < 0.05) frameTime = 0.05f;
                            else if (frameTime > duration) frameTime = duration - 0.05f;
                            ffMpeg.GetVideoThumbnail(filename, ms, frameTime);// (filename, ms, 1);
                            break;
                        case FileTypes.NotAvailable:
                        default:
                            break;
                    }
                    if (ms is MemoryStream)
                    {
                        Bitmap original = (Bitmap)Bitmap.FromStream(ms);
                        int width = (int)(original.Width * scaling);
                        int height = (int)(original.Height * scaling);
                        return new Bitmap(original, new Size(width, height));
                    }
                    else return null;
                }
                catch (Exception exc)
                {
                    return null;
                }
            }
            else return null;
        }


        public static string selectMediaFile(
            string dialogTitle,
            string filename) {

            string selectedFile = filename;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = dialogTitle;
            if (File.Exists(filename)) {
                dialog.InitialDirectory = Path.GetDirectoryName(filename);
                dialog.FileName = Path.GetFileName(filename);
            }
            else dialog.InitialDirectory = filename;
            dialog.DefaultExt = "*.avi;*.mpg;*.asf;*.wmv;*.mov;*.mp4;*.mkv;*.mk3d;*.flv;*.m4v;*.mxf;*.aac;*.aiff;*.au;*.flac;*.m4a;*.mp3;*.opus;*.wav;*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter += "All Supported Files|**.avi;*.mpg;*.asf;*.wmv;*.mov;*.mp4;*.mkv;*.mk3d;*.flv;*.m4v;*.mxf;*.aac;*.aiff;*.au;*.flac;*.m4a;*.mp3;*.opus;*.wav;*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter += "|All Movies|*.avi;*.mpg;*.asf;*.wmv;*.mov;*.mp4;*.mkv;*.mk3d;*.flv;*.m4v;*.mxf";
            dialog.Filter += "|All Sounds|*.aac;*.aiff;*.au;*.flac;*.m4a;*.mp3;*.opus;*.wav";
            dialog.Filter += "|All Images|*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter += "|Audio Video Interleave (*.avi)|*.avi";
            dialog.Filter += "|MPEG Video (*.mpg)|*.mpg";
            dialog.Filter += "|Advanced Systems Format (*asf)|*.asf";
            dialog.Filter += "|Windows Media Video (*.wmv)|*.wmv";
            dialog.Filter += "|Quicktime Movie (*.mov)|*.mov";
            dialog.Filter += "|MPEG-4 Part 14 (*.mp4)|*.mp4";
            dialog.Filter += "|Matroska Video (*.mkv)|*.mkv";
            dialog.Filter += "|Matroska Stereoscopic Video (*.mk3d)|*.mk3d";
            dialog.Filter += "|Flash Video (*.flv)|*.flv";
            dialog.Filter += "|Apple MPEG-4 Video (*.m4v)|*.m4v";
            dialog.Filter += "|Material Exchange Format(*.mxf)|*.mxf";
            dialog.Filter += "|Advanced Audio Coding (*.aac)|*.aac";
            dialog.Filter += "|Audio Interchange File Format (*.aiff)|*.aiff";
            dialog.Filter += "|Sun/NeXT/DEC/UNIX sound file (*au)|*.au";
            dialog.Filter += "|Free Lossless Audio Codec (*.flac)|*.flac";
            dialog.Filter += "|MPEG-4 Audio (*.m4a)|*.m4a";
            dialog.Filter += "|MPEG Audio Layer III (*.mp3)|*.mp3";
            dialog.Filter += "|OPUS (*.opus)|*.opus";
            dialog.Filter += "|Waveform Audio File Format (*.wav)|*.wav";
            dialog.Filter += "|Tagged Image File Format (*.tiff)|*.tiff";
            dialog.Filter += "|Windows Bitmap (*.bmp)|*.bmp";
            dialog.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
            dialog.Filter += "|Microsoft Run Length Encoding (*.msrle)|*.msrle";
            dialog.Filter += "|Portable Network Graphics (*.png)|*.png";
            dialog.Filter += "|Targa Image File (*.tga)|*.tga";
            dialog.Filter += "|Raw Image File (*.raw)|*.raw";
            dialog.Filter += "|Direct Draw Surface (*.dds)|*.dds";
            dialog.Filter += "|JPEG-File (*.jpg)|*.jpg";
            dialog.Filter += "|All files(*.*)|*.*)";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    selectedFile = dialog.FileName;
                    break;
            }
            dialog = null;

            return selectedFile;
        }

        public static string selectVideoFile(
            string dialogTitle,
            string filename) {

            string selectedFile = filename;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = dialogTitle;
            if (File.Exists(filename)) {
                dialog.InitialDirectory = Path.GetDirectoryName(filename);
                dialog.FileName = Path.GetFileName(filename);
            }
            else dialog.InitialDirectory = filename;
            dialog.DefaultExt = "*.avi;*.mpg;*.asf;*.wmv;*.mov;*.mp4;*.mkv;*.mk3d;*.flv;*.m4v;*.mxf";
            dialog.Filter = "All Videos|*.avi;*.mpg;*.asf;*.wmv;*.mov;*.mp4;*.mkv;*.mk3d;*.flv;*.m4v;*.mxf";
            dialog.Filter += "|Audio Video Interleave (*.avi)|*.avi";
            dialog.Filter += "|MPEG Video (*.mpg)|*.mpg";
            dialog.Filter += "|Advanced Systems Format (*asf)|*.asf";
            dialog.Filter += "|Windows Media Video (*.wmv)|*.wmv";
            dialog.Filter += "|Quicktime Movie (*.mov)|*.mov";
            dialog.Filter += "|MPEG-4 Part 14 (*.mp4)|*.mp4";
            dialog.Filter += "|Matroska Video (*.mkv)|*.mkv";
            dialog.Filter += "|Matroska Stereoscopic Video (*.mk3d)|*.mk3d";
            dialog.Filter += "|Flash Video (*.flv)|*.flv";
            dialog.Filter += "|Apple MPEG-4 Video (*.m4v)|*.m4v";
            dialog.Filter += "|Material Exchange Format(*.mxf)|*.mxf";
            dialog.Filter += "|All files(*.*)|*.*)";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    selectedFile = dialog.FileName;
                    break;
            }
            dialog = null;

            return selectedFile;
        }

        public static string selectAudioFile(
            string dialogTitle,
            string filename) {

            string selectedFile = filename;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = dialogTitle;
            if (File.Exists(filename)) {
                dialog.InitialDirectory = Path.GetDirectoryName(filename);
                dialog.FileName = Path.GetFileName(filename);
            }
            else dialog.InitialDirectory = filename;
            dialog.DefaultExt = "*.aac;*.aiff;*.au;*.flac;*.m4a;*.mp3;*.opus;*.wav";
            dialog.Filter = "All Sounds|*.aac;*.aiff;*.au;*.flac;*.m4a;*.mp3;*.opus;*.wav";
            dialog.Filter += "|Advanced Audio Coding (*.aac)|*.aac";
            dialog.Filter += "|Audio Interchange File Format (*.aiff)|*.aiff";
            dialog.Filter += "|Sun/NeXT/DEC/UNIX sound file (*au)|*.au";
            dialog.Filter += "|Free Lossless Audio Codec (*.flac)|*.flac";
            dialog.Filter += "|MPEG-4 Audio (*.m4a)|*.m4a";
            dialog.Filter += "|MPEG Audio Layer III (*.mp3)|*.mp3";
            dialog.Filter += "|OPUS (*.opus)|*.opus";
            dialog.Filter += "|Waveform Audio File Format (*.wav)|*.wav";
            dialog.Filter += "|All files(*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    selectedFile = dialog.FileName;
                    break;
            }
            dialog = null;

            return selectedFile;
        }

        public static string selectImageFile(
            string dialogTitle,
            string filename) {

            string selectedFile = filename;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = dialogTitle;
            if (File.Exists(filename)) {
                dialog.InitialDirectory = Path.GetDirectoryName(filename);
                dialog.FileName = Path.GetFileName(filename);
            }
            else dialog.InitialDirectory = filename;
            dialog.DefaultExt = "*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter = "All Images|*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter += "|Tagged Image File Format (*.tiff)|*.tiff";
            dialog.Filter += "|Windows Bitmap (*.bmp)|*.bmp";
            dialog.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
            dialog.Filter += "|Microsoft Run Length Encoding (*.msrle)|*.msrle";
            dialog.Filter += "|Portable Network Graphics (*.png)|*.png";
            dialog.Filter += "|Targa Image File (*.tga)|*.tga";
            dialog.Filter += "|Raw Image File (*.raw)|*.raw";
            dialog.Filter += "|Direct Draw Surface (*.dds)|*.dds";
            dialog.Filter += "|JPEG-File (*.jpg)|*.jpg";
            dialog.Filter += "|all files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    selectedFile = dialog.FileName;
                    break;
            }
            dialog = null;

            return selectedFile;
        }
        public static List<string> selectImageFilelist(
            string dialogTitle) {

            List<string> selectedFiles = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = dialogTitle;
            dialog.DefaultExt = "*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter = "All Images|*.tiff;*.bmp;*.gif;*.msrle;*.png;*.tga;*.raw;*.dds;*.jpg";
            dialog.Filter += "|Tagged Image File Format (*.tiff)|*.tiff";
            dialog.Filter += "|Windows Bitmap (*.bmp)|*.bmp";
            dialog.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
            dialog.Filter += "|Microsoft Run Length Encoding (*.msrle)|*.msrle";
            dialog.Filter += "|Portable Network Graphics (*.png)|*.png";
            dialog.Filter += "|Targa Image File (*.tga)|*.tga";
            dialog.Filter += "|Raw Image File (*.raw)|*.raw";
            dialog.Filter += "|Direct Draw Surface (*.dds)|*.dds";
            dialog.Filter += "|JPEG-File (*.jpg)|*.jpg";
            dialog.Filter += "|all files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = true;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    selectedFiles = new List<string>(dialog.FileNames);
                    break;
            }
            dialog = null;

            return selectedFiles;
        }

        public static string removeFormatSettings(
            string text) {
            if (text == null) text = string.Empty;
            int startIndex = text.IndexOf('<');
            int endIndex = text.IndexOf('>');
            while (startIndex >= 0 &&
                endIndex >= 0 &&
                startIndex < endIndex) {
                text = text.Remove(startIndex, endIndex - startIndex + 1);
                startIndex = text.IndexOf('<');
                endIndex = text.IndexOf('>');
            }
            return text;
        }

        /// <summary>
        /// Aufruf dieser Funktion für eine Action mit oder ohne Parameter
        /// Helper.InvokeActionAfterDelay(this.showStatusLoggerForm, 1500, WindowsFormsSynchronizationContext.Current);
        /// Helper.InvokeActionAfterDelay(() => this.showStatusLoggerForm(69), 1500, WindowsFormsSynchronizationContext.Current);
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delayInMilliseconds"></param>
        /// <param name="syncContext"></param>
        public static void invokeActionAfterDelay(Action action, int delayInMilliseconds, SynchronizationContext syncContext = null) {
            var t = Task.Run(async delegate {
                await Task.Delay(delayInMilliseconds);
                if (syncContext != null) syncContext.Post(new SendOrPostCallback((o) => action()), null);
                else action();
            });
        }

    }
}
