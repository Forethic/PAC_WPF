using System.Windows;
using Microsoft.Win32;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Startup.xaml 的交互逻辑
    /// </summary>
    public partial class Startup : Window
    {
        #region Constructors

        public Startup()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            var recorder = new Recorder();
            Hide();

            var result = recorder.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var editor = new Editor();
                GenericShowDialog(editor);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                Filter = "Image (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg;*.png;*.gif",
                Title = "Open one image to insert"
            };

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var editor = new Editor();
                GenericShowDialog(editor);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var create = new Create();
            var result = create.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var editor = new Editor();
                GenericShowDialog(editor);
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options();
            options.ShowDialog();
        }

        #endregion

        #region Methods

        private void GenericShowDialog(Window window)
        {
            Hide();
            window.ShowDialog();
            Close();
        }

        #endregion
    }
}