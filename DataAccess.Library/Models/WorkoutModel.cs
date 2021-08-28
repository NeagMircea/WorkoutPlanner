using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.Models
{
    public class WorkoutModel
    {
        public int WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public int WorkoutOrder { get; set; }
    }
}
