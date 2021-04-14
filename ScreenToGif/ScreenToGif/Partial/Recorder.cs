using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ScreenToGif.Util.Enum;

namespace ScreenToGif.Windows
{
    public partial class Recorder
    {
        #region Variables

        /// <summary>
        /// Lists of frames as file names.
        /// </summary>
        List<string> _ListFrames = new List<string>();

        /// <summary>
        /// The numbers of frames, this is updated while recording.
        /// </summary>
        private int _FrameCount = 0;

        /// <summary>
        /// The actual stage of the program.
        /// </summary>
        private Stage _Stage = Stage.Stopped;

        /// <summary>
        /// The Path of the Temp folder.
        /// </summary>
        private readonly string _TempPath = Path.GetTempPath() + $@"ScreenToGif\Recording\{DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")}\";

        private Bitmap _Bitmap;
        private Graphics _Graphics;

        System.Windows.Forms.Timer _CaptureTimer = new System.Windows.Forms.Timer();

        #endregion

    }
}