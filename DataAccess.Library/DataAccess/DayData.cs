using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.DataAccess
{
    public class DayData
    {
        public List<DayModel> GetAllDays()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
                sql.LoadData<DayModel, dynamic>("dbo.spDaysGetAll", new { }, "WPlannerData");

            return output;
        }


        public void InsertDaysOfTheWeek()
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spDayInsert", new { }, "WPlannerData");
        }
    }
}
