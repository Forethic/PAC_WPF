using System;
using Caliburn.Micro;
using MarkPad.Document;
using MarkPad.MDI;
using MarkPad.Services.Interfaces;
using Microsoft.Win32;

namespace MarkPad.Shell
{
    internal class ShellViewModel : Conductor<IScreen>
    {
        private readonly Func<DocumentViewModel> _DocumentCreator;
        private readonly IDialogService _DialogService;

        public MDIViewModel MDI { get; private set; }

        public ShellViewModel(IDialogService dialogService, MDIViewModel mdi, Func<DocumentViewModel> documentCreator)
        {
            MDI = mdi;
            _DialogService = dialogService;
            _DocumentCreator = documentCreator;
        }

        public void Exit() => TryClose();

        public void NewDocument() => MDI.Open(_DocumentCreator());

        public void OpenDocument()
        {
            var path = _DialogService.GetFileOpenPath("Open a markdown document.", "Any File (*.*)|*.*");
            if (string.IsNullOrEmpty(path)) return;

            var doc = _DocumentCreator();
            doc.Open(path);
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