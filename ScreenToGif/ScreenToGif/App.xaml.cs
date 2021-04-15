using System;
using System.Windows;
using ScreenToGif.Windows;

namespace ScreenToGif
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            /*
             * Startup scheme:
             * Startup > Recorder > Editor
             * Startup > Editor
             * Recorder > Editor
             * Just the Recorder (not sure if the editor will host the encoding process)
             */

            var startup = new Startup();
            startup.ShowDialog();

            Environment.Exit(0);

        }

        private void App_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}