using System.Collections.Generic;
using System.Windows;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Editor.xaml 的交互逻辑
    /// </summary>
    public partial class Editor : Window
    {
        #region Constructors

        public Editor()
        {
            InitializeComponent();
        }

        public Editor(int width, int height)
        {
            InitializeComponent();
        }

        public Editor(List<string> recording)
        {
            InitializeComponent();
        }

        #endregion
    }
}