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
    }
}
