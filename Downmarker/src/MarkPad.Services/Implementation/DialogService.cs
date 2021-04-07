using MarkPad.Services.Interfaces;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace MarkPad.Services.Implementation
{
    internal class DialogService : IDialogService
    {
        #region IDialogService Members

        public string GetFileOpenPath(string title, string filter)
        {
            if (VistaFileDialog.IsVistaFileDialogSupported)
            {
                var openFileDialog = new VistaOpenFileDialog
                {
                    Title = title,
                    CheckFileExists = true,
                    RestoreDirectory = true,
                    Filter = filter,
                };

                if (openFileDialog.ShowDialog() == true)
                    return openFileDialog.FileName;
            }
            else
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = title,
                    CheckFileExists = true,
                    RestoreDirectory = true,
                    Filter = filter,
                };

                if (openFileDialog.ShowDialog() == true)
                    return openFileDialog.FileName;
            }

            return "";
        }

        public string GetFileSavePath(string title, string defaultExt, string filter)
        {
            if (VistaFileDialog.IsVistaFileDialogSupported)
            {
                var saveFileDialog = new VistaSaveFileDialog()
                {
                    Title = title,
                    DefaultExt = defaultExt,
                    CheckFileExists = false,
                    RestoreDirectory = true,
                    Filter = filter,
                };

                if (saveFileDialog.ShowDialog() == true)
                    return saveFileDialog.FileName;
            }
            else
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Title = title,
                    DefaultExt = defaultExt,
                    CheckFileExists = false,
                    RestoreDirectory = true,
                    Filter = filter,
                };

                if (saveFileDialog.ShowDialog() == true)
                    return saveFileDialog.FileName;
            }

            return "";
        }

        public bool ShowConfirmation(string title, string text, string extra)
        {
            var service = new DialogMessageService(null)
            {
                Icon = DialogMessageIcon.Question,
                Buttons = DialogMessageButtons.Yes | DialogMessageButtons.No,
                Title = title,
                Text = text,
                Extra = extra,
            };

            return service.Show() == DialogMessageResult.Yes;
        }

        public bool? ShowConfirmationWithCancel(string title, string text, string extra)
        {
            var service = new DialogMessageService(null)
            {
                Icon = DialogMessageIcon.Question,
                Buttons = DialogMessageButtons.Yes | DialogMessageButtons.No | DialogMessageButtons.Cancel,
                Title = title,
                Text = text,
                Extra = extra,
            };

            switch (service.Show())
            {
                case DialogMessageResult.Yes:
                    return true;
                case DialogMessageResult.No:
                    return false;
            }
            return null;
        }

        public void ShowError(string title, string text, string extra)
        {
            var service = new DialogMessageService(null)
            {
                Icon = DialogMessageIcon.Error,
                Buttons = DialogMessageButtons.OK,
                Title = title,
                Text = text,
                Extra = extra,
            };

            service.Show();
        }

        public void ShowMessage(string title, string text, string extra)
        {
            var service = new DialogMessageService(null)
            {
                Icon = DialogMessageIcon.Information,
                Buttons = DialogMessageButtons.OK,
                Title = title,
                Text = text,
                Extra = extra,
            };

            service.Show();
        }

        public void ShowWarning(string title, string text, string extra)
        {
            var service = new DialogMessageService(null)
            {
                Icon = DialogMessageIcon.Warning,
                Buttons = DialogMessageButtons.OK,
                Title = title,
                Text = text,
                Extra = extra,
            };

            service.Show();
        }

        #endregion
    }
}