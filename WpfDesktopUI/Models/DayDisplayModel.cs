using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class DayDisplayModel : IDisplayModel
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public int GetId => DayId;

        public int GetOrder => 0;
    }
}
