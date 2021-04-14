/***********************************************
* 说    明：
* 命名空间：ScreenToGif.Windows
* 类 名 称：Startup
* 创建日期：2021/4/14 13:37:00
* 作    者：梁永德
* 版 本 号：4.0.30319.42000
* 文 件 名：Startup
* 修改记录(Revision History)：
*     R1：
*        修改作者：梁永德
*        修改日期：2021/4/14 13:37:00
*        修改理由：新建文件
***********************************************/

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

        #endregion

        private void GenericShowDialog(Window window)
        {
            Hide();
            window.ShowDialog();
            Close();
        }
    }
}