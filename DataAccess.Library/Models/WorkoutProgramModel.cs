using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.Models
{   
    public class WorkoutProgramModel
    {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public int WorkoutProgramId { get; set; }
        public int WorkoutProgramOrder { get; set; }
    }
}
