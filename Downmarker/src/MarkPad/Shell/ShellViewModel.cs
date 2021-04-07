using System;
using Caliburn.Micro;
using MarkPad.Document;
using MarkPad.MDI;
using MarkPad.Services.Interfaces;

namespace MarkPad.Shell
{
    internal class ShellViewModel : Conductor<IScreen>
    {
        private readonly Func<DocumentViewModel> _DocumentCreator;
        private readonly IDialogService _DialogService;

        public MDIViewModel MDI { get; private set; }
        public override string DisplayName { get => "MarkPad"; set { } }

        public ShellViewModel(IDialogService dialogService, MDIViewModel mdi, Func<DocumentViewModel> documentCreator)
        {
            MDI = mdi;
            _DialogService = dialogService;
            _DocumentCreator = documentCreator;
        }

        public void Exit() => TryClose();

        public void NewDocument() => MDI.Open(_DocumentCreator());

        public void NewJekyllDocument()
        {
            var creator = _DocumentCreator();
            creator.Document.BeginUpdate();
            creator.Document.Text = CreateJekyllHeader();
            creator.Document.EndUpdate();
            MDI.Open(creator);
        }

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

        private static string CreateJekyllHeader()
        {
            var permalink = "new-page.html";
            var title = "New Post";
            var description = "Some Description";
            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:zzz");

            return string.Format("---\r\nlayout: post\r\ntitle: {0}\r\npermalink: {1}\r\ndescription: {2}\r\ndate: {3}\r\ntags: \"some tags here\"\r\n---\r\n\r\n", title, permalink, description, date);
        }
    }
}