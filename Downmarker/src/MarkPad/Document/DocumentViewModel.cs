using System.IO;
using Caliburn.Micro;
using ICSharpCode.AvalonEdit.Document;
using MarkdownSharp;
using MarkPad.Services.Interfaces;

namespace MarkPad.Document
{
    internal class DocumentViewModel : Screen
    {
        private readonly IDialogService _DialogService;

        public TextDocument Document { get; set; }

        public string Render
        {
            get
            {
                var markdown = new Markdown();

                return markdown.Transform(Document.Text);
            }
        }

        public bool HasChanges => Original != Document.Text;
        public override string DisplayName { get => _Title + (HasChanges ? " *" : ""); set { } }
        public string Original { get; set; }

        private string _Title;
        private string _Filename;

        public DocumentViewModel(IDialogService dialogService)
        {
            _DialogService = dialogService;

            _Title = "New Document";
            Original = "";
            Document = new TextDocument();
        }

        public void Open(string filename)
        {
            _Filename = filename;
            var text = File.ReadAllText(filename);
            _Title = new FileInfo(filename).Name;
            Original = Document.Text = text;
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => Render);
            NotifyOfPropertyChange(() => HasChanges);
            NotifyOfPropertyChange(() => DisplayName);
        }

        internal void Save()
        {
            if (!HasChanges) return;

            if (string.IsNullOrEmpty(_Filename))
            {
                var path = _DialogService.GetFileSavePath("Choose a location to save the document.", "*.md", "Markdown Files (*.md)|*.md|All Files (*.*)|*.*");

                if (string.IsNullOrEmpty(path)) return;

                _Filename = path;
                _Title = new FileInfo(_Filename).Name;
            }

            File.WriteAllText(_Filename, Document.Text);
            Original = Document.Text;
        }


    }
}