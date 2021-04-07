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

        public TextDocument Document
        {
            get => _Document;
            set
            {
                _Document = value;
                OnDocumentChanged();
            }
        }

        public string Render
        {
            get
            {
                var markdown = new Markdown();

                return markdown.Transform(Document.Text);
            }
        }

        public bool HasChanges => _Original != Document.Text;

        public override string DisplayName { get => _Title + (HasChanges ? " *" : ""); set { } }

        private string _Title;
        private string _Original;
        private TextDocument _Document;
        private string _Filename;

        public DocumentViewModel(IDialogService dialogService)
        {
            _DialogService = dialogService;

            _Title = "New Document";
            _Original = "";
            _Document = new TextDocument();
        }

        public void Open(string filename)
        {
            _Filename = filename;
            var text = File.ReadAllText(filename);
            _Title = new FileInfo(filename).Name;
            _Original = _Document.Text = text;
        }

        public void Update() => OnDocumentChanged();

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

            File.WriteAllText(_Filename, _Document.Text);
            _Original = _Document.Text;

            OnDocumentChanged();
        }

        private void OnDocumentChanged()
        {
            NotifyOfPropertyChange(() => Render);
            NotifyOfPropertyChange(() => HasChanges);
            NotifyOfPropertyChange(() => DisplayName);
        }
    }
}