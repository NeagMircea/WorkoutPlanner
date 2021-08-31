using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class CategoryDisplayModel /*: INotifyPropertyChanged*/
    {
        public int CategoryId { get; set; }

        private string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                //CallPropertyChanged(nameof(Name));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;


        //public void CallPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
