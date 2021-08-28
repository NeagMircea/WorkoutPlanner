using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.DataAccess
{
    public class WorkoutProgramData
    {
        public List<WorkoutProgramModel> GetWorkoutsByProgramId(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ProgramId = id };

            var output =
                sql.LoadData<WorkoutProgramModel, dynamic>("dbo.spWorkoutLookupByProgramId", p, "WPlannerData");

            return output;
        }


        public void AddWorkoutToProgram(int programId, int workoutId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ProgramId = programId, WorkoutId = workoutId };

            sql.SaveData("dbo.spWorkoutProgramsInsert", p, "WPlannerData");

        }


        public void RemoveWorkoutFromProgram(int programId, int workoutId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ProgramId = programId, WorkoutId = workoutId };

            sql.SaveData("dbo.spWorkoutProgramRemoveAt", p, "WPlannerData");
        }


        public void SwapWorkoutProgramOrder(int workoutOneId, int workoutOneOrder, int workoutTwoId, int workoutTwoOrder)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                WorkoutOneId = workoutOneId,
                WorkoutOneOrder = workoutOneOrder,
                WorkoutTwoId = workoutTwoId,
                WorkoutTwoOrder = workoutTwoOrder
            };

            sql.SaveData("dbo.spWorkoutProgramSwapOrder", p, "WPlannerData");
        }
    }
}
