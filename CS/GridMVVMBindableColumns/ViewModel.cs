using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Utils.Commands;
using DevExpress.Xpf.Core.Commands;
using System.Collections;
using System.Linq;
using System.ComponentModel;

namespace Model {
    public class ViewModel : INotifyPropertyChanged {
        bool categoryColumnVisibility = true;
        public ObservableCollection<DataItem> Source { get; private set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public bool CategoryColumnVisibility {
            get { return categoryColumnVisibility; }
            set {
                if(categoryColumnVisibility == value)
                    return;
                categoryColumnVisibility = value;
                RaisePropertyChanged("CategoryColumnVisibility");
            }
        }
        public ICommand ShowHideCategoriesColumn { get; set; }
        public ViewModel() {
            Source = DataItem.Data;
            Categories = new ObservableCollection<Category>() {
                new Category() { Id=0, Name="A category"},
                new Category() { Id=1, Name="B category"},
                new Category() { Id=2, Name="C category"},
                new Category() { Id=3, Name="D category"}
            };
            ShowHideCategoriesColumn = new DelegateCommand<object>(obj => {
                CategoryColumnVisibility = !CategoryColumnVisibility;
            }, null);
        }

        void RaisePropertyChanged(string name) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Category {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DataItem {
        public static ObservableCollection<DataItem> Data {
            get {
                ObservableCollection<DataItem> res = new ObservableCollection<DataItem>();
                for(int i = 0; i < 100; i++) {
                    res.Add(new DataItem() { Id = i, Name = "Name" + i.ToString(), Category = i % 4 });
                }
                return res;
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public override string ToString() {
            return Name;
        }
    }
}
