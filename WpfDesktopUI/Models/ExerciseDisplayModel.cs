using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class ExerciseDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VideoPath { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Duration { get; set; }
    }
}
