using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.Models
{
    public class ExerciseDisplayModel : IDisplayModel
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string VideoPath { get; set; }
        public int Sets { get; set; }
        public int MinReps { get; set; }
        public int MaxReps { get; set; }
        public float Duration { get; set; }
        public int ExerciseOrder { get; set; }

        public int GetId => Id;

        public int GetOrder => ExerciseOrder;

        public string SetsDisplay
        {
            get
            {
                if (Sets > 0)
                {
                    return $"Sets: {Sets}";
                }
                else
                {
                    return $"Sets: 1";
                }
            }
        }

        public string RepRangeDisplay
        {
            get
            {
                if (MinReps <= 0 && MaxReps <= 0)
                {
                    return "";
                }

                if (MinReps > 0 && MaxReps > 0)
                {
                    return $" Reps: {MinReps}-{MaxReps}";
                }

                if (MinReps > 0 && MaxReps < 0)
                {
                    return $" Reps: {MinReps}";
                }
                else
                {
                    return $" Reps: {MaxReps}";
                }
            }

        } 

        public string DurationDisplay
        {
            get
            {
                if (Duration > 0)
                {
                    return $" Duration: {Duration}";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
