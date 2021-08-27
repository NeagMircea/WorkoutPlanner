using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class WorkoutDisplayModel
    {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public int WorkoutOrder { get; set; }
        public int WorkoutProgramId { get; set; }
    }
}
