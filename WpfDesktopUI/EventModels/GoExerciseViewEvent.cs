using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.EventModels
{
    public class GoExerciseViewEvent
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public string WorkoutInfo { get; set; }
    }
}
