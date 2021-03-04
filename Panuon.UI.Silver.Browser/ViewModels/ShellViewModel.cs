using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Panuon.UI.Silver.Browser.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        public ShellViewModel()
        {
            SourceItems = new ObservableCollection<SourceItemModel>()
            {
                new SourceItemModel(){ Content = "Item 1",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 2",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 3",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 4",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 5",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 6",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
                new SourceItemModel(){ Content = "Item 7",
                    Items = new ObservableCollection<SourceItemModel>()
                    {
                        new SourceItemModel(){ Content = "Item 1", },
                        new SourceItemModel(){ Content = "Item 2", },
                        new SourceItemModel(){ Content = "Item 3", },
                    }
                },
            };
            DataGridItems = new ObservableCollection<DataGridItemModel>()
            {
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
                new DataGridItemModel(){ Column1 = "Column1", Column2 = "Column2", Column3 = "Column3"},
            };
        }

        public ObservableCollection<SourceItemModel> SourceItems { get => _sourceItems; set { _sourceItems = value; NotifyPropertyChanged(nameof(SourceItems)); } }
        private ObservableCollection<SourceItemModel> _sourceItems;

        public ObservableCollection<DataGridItemModel> DataGridItems { get => _dataGridItems; set { _dataGridItems = value; NotifyPropertyChanged(nameof(DataGridItems)); } }
        private ObservableCollection<DataGridItemModel> _dataGridItems;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class SourceItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Content { get; set; }

        public bool IsSelected { get; set; }

        public ObservableCollection<SourceItemModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }
        private ObservableCollection<SourceItemModel> _items;

    }

    public class DataGridItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}