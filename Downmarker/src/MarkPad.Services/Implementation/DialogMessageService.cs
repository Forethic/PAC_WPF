using System;
using System.Windows;
using Ookii.Dialogs.Wpf;

namespace MarkPad.Services.Implementation
{
    enum DialogMessageResult
    {
        None,
        OK,
        Cancel,
        Retry,
        Yes,
        No,
        Close,
        CustomButtonClicked,
    }

    [Flags]
    enum DialogMessageButtons
    {
        None = 0x0000,
        OK = 0x0001,
        Yes = 0x0002,
        No = 0x0004,
        Cancel = 0x0008,
        Retry = 0x00010,
        Close = 0x00020
    }

    enum DialogMessageIcon
    {
        None,
        Error,
        Question,
        Warning,
        Information,
        Shield,
    }

    internal class DialogMessageService
    {
        private Window _Owner;

        public string Title { get; set; }
        public string Extra { get; set; }
        public string Text { get; set; }
        public DialogMessageButtons Buttons { get; set; }
        public DialogMessageIcon Icon { get; set; }

        public DialogMessageService(Window owner)
        {
            _Owner = owner;
        }

        public DialogMessageResult Show()
        {
            if (TaskDialog.OSSupportsTaskDialogs) return DoOokiiMsgBox();
            return DoWin32MsgBox();
        }

        private DialogMessageResult DoOokiiMsgBox()
        {
            var taskDialog = new TaskDialog();

            if ((Buttons & DialogMessageButtons.OK) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
            if ((Buttons & DialogMessageButtons.Cancel) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));

            if ((Buttons & DialogMessageButtons.Yes) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            if ((Buttons & DialogMessageButtons.No) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.No));

            if ((Buttons & DialogMessageButtons.Close) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Close));
            if ((Buttons & DialogMessageButtons.Retry) != 0) taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Retry));

            switch (Icon)
            {
                case DialogMessageIcon.Error:
                    taskDialog.MainIcon = TaskDialogIcon.Error;
                    break;
                case DialogMessageIcon.Question:
                    taskDialog.MainIcon = TaskDialogIcon.Warning;
                    break;
                case DialogMessageIcon.Warning:
                    taskDialog.MainIcon = TaskDialogIcon.Warning;
                    break;
                case DialogMessageIcon.Information:
                    taskDialog.MainIcon = TaskDialogIcon.Information;
                    break;
                case DialogMessageIcon.Shield:
                    taskDialog.MainIcon = TaskDialogIcon.Shield;
                    break;
            }

            taskDialog.WindowTitle = Title;
            taskDialog.MainInstruction = Text;
            taskDialog.Content = Extra;

            TaskDialogButton result = null;

            if (_Owner == null) result = taskDialog.ShowDialog();
            else
            {
                var dispatcher = _Owner.Dispatcher;

                result = dispatcher.Invoke(new Func<TaskDialogButton>(() => taskDialog.ShowDialog(_Owner)), System.Windows.Threading.DispatcherPriority.Normal);
            }

            switch (result.ButtonType)
            {
                case ButtonType.Cancel:
                    return DialogMessageResult.Cancel;
                case ButtonType.Close:
                    return DialogMessageResult.Close;
                case ButtonType.Custom:
                    return DialogMessageResult.CustomButtonClicked;
                case ButtonType.No:
                    return DialogMessageResult.No;
                case ButtonType.Ok:
                    return DialogMessageResult.OK;
                case ButtonType.Retry:
                    return DialogMessageResult.Retry;
                case ButtonType.Yes:
                    return DialogMessageResult.Yes;
            }

            return DialogMessageResult.None;
        }

        private DialogMessageResult DoWin32MsgBox()
        {
            MessageBoxButton button = MessageBoxButton.OK;
            if (Buttons == (DialogMessageButtons.OK | DialogMessageButtons.Cancel))
                button = MessageBoxButton.OKCancel;
            else if (Buttons == (DialogMessageButtons.Yes | DialogMessageButtons.No))
                button = MessageBoxButton.YesNo;
            else if (Buttons == (DialogMessageButtons.Yes | DialogMessageButtons.No | DialogMessageButtons.Cancel))
                button = MessageBoxButton.YesNoCancel;

            var icon = MessageBoxImage.None;
            switch (Icon)
            {
                case DialogMessageIcon.Error:
                    icon = MessageBoxImage.Error;
                    break;
                case DialogMessageIcon.Question:
                    icon = MessageBoxImage.Question;
                    break;
                case DialogMessageIcon.Warning:
                    icon = MessageBoxImage.Warning;
                    break;
                case DialogMessageIcon.Information:
                    icon = MessageBoxImage.Information;
                    break;
            }

            MessageBoxResult result = MessageBoxResult.None;

            if (_Owner == null) result = MessageBox.Show(string.Format("{0}{1}{1}{2}", Text, Environment.NewLine, Extra), Title, button, icon);
            else
            {
                var dispatcher = _Owner.Dispatcher;

                result = dispatcher.Invoke(new Func<MessageBoxResult>(() => MessageBox.Show(_Owner, string.Format("{0}{1}{1}{2}", Text, Environment.NewLine, Extra), Title, button, icon)));
            }

            switch (result)
            {
                case MessageBoxResult.Cancel:
                    return DialogMessageResult.Cancel;
                case MessageBoxResult.No:
                    return DialogMessageResult.No;
                case MessageBoxResult.None:
                    return DialogMessageResult.None;
                case MessageBoxResult.OK:
                    return DialogMessageResult.OK;
                case MessageBoxResult.Yes:
                    return DialogMessageResult.Yes;
            }

            return DialogMessageResult.None;
        }
    }
}