using System.IO;
using Caliburn.Micro;
using MarkdownSharp;
using Microsoft.Win32;

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
        private string _Original;
        private string _Document;
        private string _Filename;

        public DocumentViewModel()
        {
            _Title = "New Document";
            _Original = "";
            _Document = "";
        }

        public void Open(string filename)
        {
            _Filename = filename;
            var text = File.ReadAllText(filename);
            _Title = new FileInfo(filename).Name;
            _Original = _Document = text;
        }

        internal void Save()
        {
            if (!HasChanges) return;

            if (string.IsNullOrEmpty(_Filename))
            {
                var saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog() == false)
                    return;

                _Filename = saveDialog.FileName;
                _Title = new FileInfo(_Filename).Name;
            }

            File.WriteAllText(_Filename, _Document);
            _Original = _Document;

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