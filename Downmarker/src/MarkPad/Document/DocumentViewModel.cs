using System.IO;
using Caliburn.Micro;
using MarkdownSharp;

namespace MarkPad.Document
{
    internal class DocumentViewModel : Screen
    {
        public string Document
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

                return markdown.Transform(Document);
            }
        }

        public bool HasChanges => _Original != Document;

        public override string DisplayName { get => _Title + (HasChanges ? " *" : ""); set { } }

        private string _Title;
        private readonly string _Original;
        private string _Document;

        public DocumentViewModel()
        {
            _Title = "New Document";
            _Original = "";
            _Document = "";
        }

        public void Open(string filename)
        {
            var text = File.ReadAllText(filename);
            _Title = new FileInfo(filename).Name;
            _Document = text;
        }

        private void OnDocumentChanged()
        {
            NotifyOfPropertyChange(() => Render);
            NotifyOfPropertyChange(() => HasChanges);
            NotifyOfPropertyChange(() => DisplayName);
        }
    }
}