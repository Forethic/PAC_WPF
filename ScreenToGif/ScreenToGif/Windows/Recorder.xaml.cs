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
using TextBox = System.Windows.Controls.TextBox;
using ScreenToGif.Util.Enum;
using System.Collections.Generic;
using ScreenToGif.Properties;
using ScreenToGif.Controls;

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
            int left = 0;
            Dispatcher.Invoke(() => { left = (int)Left; });
            int top = 0;
            Dispatcher.Invoke(() => { top = (int)Top; });

            var lefttop = new Point(left + 7, top + 32);
            _Graphics.CopyFromScreen(lefttop.X, lefttop.Y, 0, 0, _Size, CopyPixelOperation.SourceCopy);
            _AddDel.BeginInvoke($"{_TempPath}{_FrameCount}.bmp", new Bitmap(_Bitmap), Callback, null);

            Dispatcher.Invoke(() => Title = $"Screen To Gif • {_FrameCount}");

            _FrameCount++;
            GC.Collect();
        }

        private void Stop_Click(object sender, RoutedEventArgs e) => _CaptureTimer.Stop();

        private void RecordPause_Click(object sender, RoutedEventArgs e)
        {
            Record_Pause();
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
            //HeightTextBox.Text = Height.ToString();
            WidthTextBox.Text = Width.ToString();

            HeightTextBox.Value = (int)Height;
            WidthTextBox.Value = (int)Width;
        }

        private void SizeBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is NumericTextBox textbox)
            {
                textbox.Value = Convert.ToInt32(textbox.Text);
                textbox.Value = e.Delta > 0 ? textbox.Value + 1 : textbox.Value - 1;

                AdjustToSize();
            }
        }

        #endregion

        #region Methods

        private void Record_Pause()
        {
            CreateTemp();

            if (_Stage == Stage.Stopped)
            {
                #region To Record

                _CaptureTimer.Interval = 1000 / NumericUpDown.Value;

                _ListFrames = new List<string>();
                _ListCursor = new List<Util.CursorInfo>();

                #region If Fullscreen

                if (Settings.Default.FullScreen)
                {
                    _Bitmap = new Bitmap(_SizeScreen.X, _SizeScreen.Y);
                }
                else
                {
                    _Bitmap = new Bitmap((int)Width - 24, (int)Height - 65);
                }
                _Graphics = Graphics.FromImage(_Bitmap);

                #endregion

                HeightTextBox.IsEnabled = false;
                WidthTextBox.IsEnabled = false;
                NumericUpDown.IsEnabled = false;
                RecordPauseButton.IsEnabled = false;
                IsRecording(true);
                Topmost = true;

                _AddDel = AddFrames;

                #region Start

                if (Settings.Default.PreStart)
                {
                    Title = "Screen To Gif (2 seconds to go)";

                    _Stage = Stage.PreStarting;
                    _PreStartCount = 1;
                }
                else
                {
                    if (Settings.Default.ShowCursor)
                    {
                        #region if show cursor

                        if (!Settings.Default.Snapshot)
                        {
                            #region Normal Recording

                            if (!Settings.Default.FullScreen) { }
                            else { }

                            _Stage = Stage.Recording;
                            RecordPauseButton.Tag = "/ScreenToGif;component/Resources/Pause16x.png";

                            #endregion
                        }
                        else
                        {
                            #region SnapShot Recording

                            _Stage = Stage.Snapping;
                            RecordPauseButton.Tag = "/ScreenToGif;component/Resources/Pause16x.png";
                            Title = "Screen To Gif - ";

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region If not
                        if (!Settings.Default.Snapshot)
                        {
                            #region Normal Recording

                            if (!Settings.Default.FullScreen) { }
                            else { }

                            _Stage = Stage.Recording;
                            RecordPauseButton.Tag = "/ScreenToGif;component/Resources/Pause16x.png";

                            #endregion
                        }
                        else
                        {
                            #region SnapShot Recording

                            _Stage = Stage.Snapping;

                            #endregion
                        }
                        #endregion
                    }
                }

                #endregion

                #endregion
            }
            else if (_Stage == Stage.Recording)
            {
                #region To Pause

                RecordPauseButton.Tag = Tag = "/ScreenToGif;component/Resources/Record16x.png";
                _Stage = Stage.Paused;

                #endregion
            }

            else if (_Stage == Stage.Paused)
            {
                #region To Record Again

                RecordPauseButton.Tag = Tag = "/ScreenToGif;component/Resources/Pause16x.png";
                _Stage = Stage.Recording;

                #endregion
            }
            else if (_Stage == Stage.Snapping)
            {

            }


            _Size = new Size((int)Width - 14, (int)Height - 65);
            _CaptureTimer.Interval = 1000 / NumericUpDown.Value;
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

            HeightTextBox.Value = Convert.ToInt32(HeightTextBox.Text);
            WidthTextBox.Value = Convert.ToInt32(WidthTextBox.Text);

            Width = WidthTextBox.Value;
            Height = HeightTextBox.Value;
        }

        #endregion
    }
}