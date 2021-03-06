using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class ProgramDisplayModel /*: INotifyPropertyChanged*/ : IDisplayModel
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                //CallPropertyChanged(nameof(Name));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;


        //public void CallPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public int ProgramOrder { get; set; }

        public int GetId => Id;

        public int GetOrder => ProgramOrder;
    }
}
