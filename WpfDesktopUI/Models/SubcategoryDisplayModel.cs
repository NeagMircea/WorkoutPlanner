using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class SubcategoryDisplayModel /*: INotifyPropertyChanged*/ : IDisplayModel
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string SubcategoryInfo { get; set; }

        public int GetId => SubcategoryId;

        public int GetOrder => 0;

        //private int subcategoryId;
        //public int SubcategoryId
        //{
        //    get { return subcategoryId; }
        //    set
        //    {
        //        subcategoryId = value;
        //        OnPropertyChanged(nameof(SubcategoryId));
        //    }
        //}

        //private string subcategoryName;
        //public string SubcategoryName
        //{
        //    get { return subcategoryName; }
        //    set
        //    {
        //        subcategoryName = value;
        //        OnPropertyChanged("SubcategoryName");
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
