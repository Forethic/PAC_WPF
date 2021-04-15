using System.Drawing;

namespace ScreenToGif.Util
{
    public class CursorInfo
    {
        /// <summary>
        /// The position of the cursor.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// The image of the icon.
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// True if clicked
        /// </summary>
        public bool Clicked { get; set; }
    }
}