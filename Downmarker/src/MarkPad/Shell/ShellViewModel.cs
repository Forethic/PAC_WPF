using System;
using Caliburn.Micro;
using MarkPad.Document;
using MarkPad.MDI;

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
    }
}