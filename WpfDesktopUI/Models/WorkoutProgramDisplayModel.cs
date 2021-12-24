using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class WorkoutProgramDisplayModel : IDisplayModel
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public string WorkoutInfo { get; set; }
        public int WorkoutProgramId { get; set; }
        public int WorkoutProgramOrder { get; set; }

        public int GetId => Id;

        public int GetOrder => WorkoutProgramOrder;
    }
}
