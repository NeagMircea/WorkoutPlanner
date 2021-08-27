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
    public class WorkoutData
    {
        public List<WorkoutModel> GetAllWorkouts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
            sql.LoadData<WorkoutModel, dynamic>("dbo.spWorkoutGetAll", new { }, "WPlannerData");

            return output;
        }


        public void DeleteWorkoutRecord(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };

            sql.SaveData("dbo.spWorkoutRemoveAt", p, "WPlannerData");
        }


        public void SaveWorkoutRecord(string workoutName)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new DynamicParameters();
            p.Add("@Name", workoutName);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            sql.SaveData("dbo.spWorkoutInsert", p, "WPlannerData");

            //int workoutId = p.Get<int>("@Id");

            //var r = new { ProgramId = programId, WorkoutId = workoutId };

            //sql.SaveData("dbo.spWorkoutProgramsInsert", r, "WPlannerData");
        }


        public List<WorkoutModel> GetWorkoutsByProgramId(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { ProgramId = id };

            var output = 
                sql.LoadData<WorkoutModel, dynamic>("dbo.spWorkoutLookupByProgramId", p, "WPlannerData");

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
