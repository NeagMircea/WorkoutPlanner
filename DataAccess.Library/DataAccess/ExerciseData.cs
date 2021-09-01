using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.DataAccess
{
    public class ExerciseData
    {
        public List<ExerciseModel> GetAllExercises()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
                sql.LoadData<ExerciseModel, dynamic>("dbo.spExercisesGetAll", new { }, "WPlannerData");

            return output;
        }


        public List<ExerciseModel> GetExerciseByCategory(int categoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();
 
            var p = new { CategoryId = categoryId };

            var output =
                sql.LoadData<ExerciseModel, dynamic>("dbo.spExercisesGetByCategoryId", p, "WPlannerData");

            return output;
        }


        public List<ExerciseModel> GetExercisesByWorkoutDayId(int workoutId, int dayId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                WorkoutId = workoutId,
                DayId = dayId
            };

            var output =
                sql.LoadData<ExerciseModel, dynamic>("dbo.spExercisesGetByWorkoutDayId", p, "WPlannerData");

            return output;
        }


        public void SaveExerciseRecord(ExerciseModel model, int categoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                ExerciseName = model.ExerciseName,
                VideoPath = model.VideoPath,
                CategoryId = categoryId,
                Sets = model.Sets,
                Reps = model.Reps,
                Duration = model.Duration
            };

            sql.SaveData("dbo.spExerciseInsert", p, "WPlannerData");
        }


        public void SaveExerciseRecord(int workoutId, int dayId, int exerciseId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                WorkoutId = workoutId,
                DayId = dayId,
                ExerciseId = exerciseId
            };

            sql.SaveData("dbo.spWorkoutDayExercisesInsert", p, "WPlannerData");
        }


        public void RemoveExerciseRecord(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };

            sql.SaveData("dbo.spExerciseRemoveAt", p, "WPlannerData");
        }


        public void RemoveWorkoutDayExerciseRecord(int workoutId, int dayId, int exerciseId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                WorkoutId = workoutId,
                DayId = dayId,
                ExerciseId = exerciseId
            };

            sql.SaveData("dbo.spWorkoutDayExerciseRemoveAt", p, "WPlannerData");
        }


        public void SwapExerciseOrder(int idOne, int exerciseOneOrder, int idTwo, int exerciseTwoOrder)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                IdOne = idOne,
                ExerciseOneOrder = exerciseOneOrder,
                IdTwo = idTwo,
                ExerciseTwoOrder = exerciseTwoOrder
            };

            sql.SaveData("dbo.spExercisesSwapOrder", p, "WPlannerData");
        }
    }
}
