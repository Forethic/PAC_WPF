using System;
using Caliburn.Micro;
using MarkPad.Document;
using MarkPad.MDI;
using Microsoft.Win32;

namespace MarkPad.Shell
{
    internal class ShellViewModel : Conductor<IScreen>
    {
        public MDIViewModel MDI { get; private set; }

        private Func<DocumentViewModel> _DocumentCreator;

        public ShellViewModel(MDIViewModel mdi, Func<DocumentViewModel> documentCreator)
        {
            MDI = mdi;
            _DocumentCreator = documentCreator;
        }

        public void Exit() => TryClose();

        public void NewDocument() => MDI.Open(_DocumentCreator());

        public void OpenDocument()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != true)
                return;

            var doc = _DocumentCreator();
            doc.Open(ofd.FileName);
            MDI.Open(doc);
        }

        public void SaveDocument()
        {
            if (MDI.ActiveItem is DocumentViewModel doc)
            {
                doc.Save();
            }
        }

        public void SaveAllDocument()
        {
            foreach (DocumentViewModel doc in MDI.Items)
            {
                doc.Save();
            }
        }
    }
}