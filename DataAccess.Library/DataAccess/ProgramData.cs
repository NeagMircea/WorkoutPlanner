using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.DataAccess
{
    public class ProgramData
    {

        public List<ProgramModel> GetProgramById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };

            var output =
                sql.LoadData<ProgramModel, dynamic>("dbo.spProgramLookup", p, "WPlannerData");

            return output;
        }


        public List<ProgramModel> GetAllPrograms()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
                sql.LoadData<ProgramModel, dynamic>("dbo.spProgramGetAll", new { }, "WPlannerData");

            return output;
        }


        public void SaveProgramRecord(ProgramModel program)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spProgramInsert", program, "WPlannerData");
        }


        public void DeleteProgramRecord(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };

            sql.SaveData("dbo.spProgramRemoveAt", p, "WPlannerData");
        }
    }
}
