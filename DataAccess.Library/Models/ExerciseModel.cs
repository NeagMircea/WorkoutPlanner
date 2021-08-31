using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.Models
{
    public class ExerciseModel
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string VideoPath { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Duration { get; set; }
    }
}
