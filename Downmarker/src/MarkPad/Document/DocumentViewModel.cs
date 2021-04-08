using System.IO;
using System.Text.RegularExpressions;
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
                var textToRender = StripHeader(Document.Text);

                return markdown.Transform(textToRender);
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

        public void Open(string path)
        {
            _Filename = path;
            var text = File.ReadAllText(path);
            _Title = new FileInfo(path).Name;
            Original = Document.Text = text;
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => Render);
            NotifyOfPropertyChange(() => HasChanges);
            NotifyOfPropertyChange(() => DisplayName);
        }

        internal bool Save()
        {
            if (!HasChanges) return true;

            if (string.IsNullOrEmpty(_Filename))
            {
                var path = _DialogService.GetFileSavePath("Choose a location to save the document.", "*.md", "Markdown Files (*.md)|*.md|All Files (*.*)|*.*");

                if (string.IsNullOrEmpty(path)) return false;

                _Filename = path;
                _Title = new FileInfo(_Filename).Name;
            }

            File.WriteAllText(_Filename, Document.Text);
            Original = Document.Text;

            return true;
        }

        public override void CanClose(System.Action<bool> callback)
        {
            if (!HasChanges)
            {
                callback(true);
                return;
            }

            var saveResult = _DialogService.ShowConfirmationWithCancel("MarkPad", "Do you want to save changes to '" + _Title + "'?", "");
            bool result = false;

            // true = Yes
            if (saveResult == true) { result = Save(); }
            // false = No
            else if (saveResult == false) { result = true; }
            // no result = Cancel
            else if (!saveResult.HasValue) { result = false; }

            callback(result);
        }

        private static string StripHeader(string text)
        {
            const string DELIMITER = "---";
            var matches = Regex.Matches(text, DELIMITER, RegexOptions.Multiline);

            if (matches.Count != 2)
            {
                return text;
            }

            var startIndex = matches[0].Index;
            var endIndex = matches[1].Index;
            var length = endIndex - startIndex + DELIMITER.Length;
            var textToReplace = text.Substring(startIndex, length);

            return text.Replace(textToReplace, string.Empty);
        }
    }
}