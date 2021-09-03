using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.EventModels
{
    public class GoCategoryViewEvent
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }

        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }

        public int DayId { get; set; }
        public string DayName { get; set; }
    }
}
