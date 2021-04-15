using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Recorder.xaml 的交互逻辑
    /// </summary>
    public partial class Recorder
    {
        private Point _Point;
        private Size _Size;
        private Point _SizeScreen = new Point(SystemInformation.PrimaryMonitorSize);

        #region Record Async

        /// <summary>
        /// Saves the Bitmap to the disk and adds the filename in the list of frames.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="bitmap"></param>
        public delegate void AddFrame(string filename, Bitmap bitmap);

        private AddFrame _AddDel;

        private void AddFrames(string filename, Bitmap bitmap)
        {
            _ListFrames.Add(filename);
            bitmap.Save(filename);
            bitmap.Dispose();
        }

        private void Callback(IAsyncResult result)
        {
            _AddDel.EndInvoke(result);
        }

        #endregion

        #region Constructors

        public Recorder()
        {
            InitializeComponent();

            _CaptureTimer.Tick += CaptureTimer_Tick;
        }

        #endregion

        #region Event

        private void CaptureTimer_Tick(object sender, EventArgs e)
        {
            _Graphics.CopyFromScreen(_Point.X, _Point.Y, 0, 0, _Size, CopyPixelOperation.SourceCopy);
            _AddDel.BeginInvoke($"{_TempPath}{_FrameCount}.bmp", new Bitmap(_Bitmap), Callback, null);

            Dispatcher.Invoke(() => Title = $"Screen To Gif • {_FrameCount}");

            _FrameCount++;
            GC.Collect();
        }

        private void Stop_Click(object sender, RoutedEventArgs e) => _CaptureTimer.Stop();

        private void RecordPause_Click(object sender, RoutedEventArgs e)
        {
            Record_Pause();

            _AddDel = AddFrames;
            _Point = new Point((int)Left + 5, (int)Top + 5);
            _Size = new Size((int)Width, (int)Height);
            _Bitmap = new Bitmap(_Size.Width, _Size.Height);
            _Graphics = Graphics.FromImage(_Bitmap);

            _CaptureTimer.Interval = NumbericUpDown.Value;
            _CaptureTimer.Start();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
            {
                e.Handled = false;
                return;
            }

            if (IsTextDisallowed(e.Text))
            {
                e.Handled = false;
                return;
            }

            if (string.IsNullOrEmpty(e.Text))
            {
                e.Handled = false;
                return;
            }
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));

                if (IsTextDisallowed(text)) { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { AdjustToSize(); }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) => AdjustToSize();

        private void LightWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HeightTextBox.Text = Height.ToString();
            WidthTextBox.Text = Width.ToString();
        }

        #endregion

        #region Methods

        private void Record_Pause()
        {
            CreateTemp();

            _AddDel = AddFrames;
            _Point = new Point((int)Left + 5, (int)Top + 5);
            _Size = new Size((int)Width, (int)Height);
            _Bitmap = new Bitmap(_Size.Width, _Size.Height);
            _Graphics = Graphics.FromImage(_Bitmap);

            _CaptureTimer.Interval = 1000 / NumbericUpDown.Value;
            _CaptureTimer.Start();
        }

        private void CreateTemp()
        {
            if (!Directory.Exists(_TempPath))
            {
                Directory.CreateDirectory(_TempPath);
                Directory.CreateDirectory(_TempPath + "Undo");
                Directory.CreateDirectory(_TempPath + "Edit");
            }
        }

        private bool IsTextDisallowed(string text)
        {
            var regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }

        private void AdjustToSize()
        {
            int heightTb = Convert.ToInt32(HeightTextBox.Text);
            int widthTb = Convert.ToInt32(WidthTextBox.Text);

            #region Checks if size is smaller than screen size

            if (heightTb > _SizeScreen.Y)
            {
                heightTb = _SizeScreen.Y;
                HeightTextBox.Text = _SizeScreen.Y.ToString();
            }

            if (widthTb > _SizeScreen.X)
            {
                widthTb = _SizeScreen.X;
                WidthTextBox.Text = _SizeScreen.X.ToString();
            }

            #endregion

            Width = widthTb;
            Height = heightTb;
        }

        #endregion


    }
}