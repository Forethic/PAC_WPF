using System.Windows;

namespace ScreenToGif.Windows
{
    /// <summary>
    /// Create.xaml 的交互逻辑
    /// </summary>
    public partial class Create : Window
    {
        #region Properties

        public int HeightValue { get; set; }

        public int WidthValue { get; set; }

        #endregion

        #region Constructors

        public Create()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation

            if (!int.TryParse(WidthText.Text, out var width)) { return; }

            if (!int.TryParse(HeightText.Text, out var height)) { return; }

            #endregion

            HeightValue = height;
            WidthValue = width;

            DialogResult = true;
            Close();
        }

        #endregion
    }
}