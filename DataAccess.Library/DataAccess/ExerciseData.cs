using Dapper;
using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public List<ExerciseModel> GetExerciseByCategorySubcat(int categoryId, List<int> subcategoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                CategoryId = categoryId,
                SubcategoryId = new List<int>(subcategoryId)
            };

            string query = @"SELECT MIN(ExerciseId), ExerciseName, VideoPath, Sets, Reps, Duration 
                             FROM Exercises_Categories_Subcategories 
                             WHERE CategoryId = @CategoryId AND SubcategoryId IN @SubcategoryId
                             GROUP BY [ExerciseId], [ExerciseName], [VideoPath], [Sets], [Reps], [Duration];";

            var output =
                sql.QueryString<ExerciseModel, dynamic>(query, p, "WPlannerData");

            return output;
        }


        public void SaveExerciseRecord(ExerciseModel model, int categoryId, List<int> subcategoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new DynamicParameters();
            p.Add("@ExerciseName", model.ExerciseName);
            p.Add("@VideoPath", model.VideoPath);
            p.Add("@CategoryId", categoryId);
            p.Add("@Sets", model.Sets);
            p.Add("@Reps", model.Reps);
            p.Add("@Duration", model.Duration);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            sql.SaveData("dbo.spExerciseInsert", p, "WPlannerData");

            int exerciseId = p.Get<int>("@Id");

            foreach (int id in subcategoryId)
            {
                p = new DynamicParameters();
                p.Add("@ExerciseId", exerciseId);
                p.Add("@CategoryId", categoryId);
                p.Add("@SubcategoryId", id);

                sql.SaveData("dbo.spExercisesCategoriesSubcategoriesInsert", p, "WPlannerData");
            }
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
