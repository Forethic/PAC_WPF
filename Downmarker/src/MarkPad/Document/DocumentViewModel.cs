using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly string _Title;
        private readonly string _Original;
        private string _Document;

        public DocumentViewModel()
        {
            _Title = "New Document";
            _Original = "";
            _Document = "";
        }

        private void OnDocumentChanged()
        {
            NotifyOfPropertyChange(() => Render);
            NotifyOfPropertyChange(() => HasChanges);
            NotifyOfPropertyChange(() => DisplayName);
        }
    }
}