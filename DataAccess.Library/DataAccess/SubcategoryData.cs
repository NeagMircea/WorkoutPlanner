using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Library.DataAccess
{
    public class SubcategoryData
    {
        public List<SubcategoryModel> GetAllSubcategories()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
                sql.LoadData<SubcategoryModel, dynamic>("dbo.spSubcategoryGetAll", new { }, "WPlannerData");

            return output;
        }


        public List<SubcategoryModel> GetSubcategoryByCategoryId(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new {CategoryId = id};

            var output =
                sql.LoadData<SubcategoryModel, dynamic>("dbo.spSubcategoryGetByCategoryId", p, "WPlannerData");

            return output;
        }


        public void SaveSubcategoryRecord(string subcategoryName)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                SubcategoryName = subcategoryName
            };

            sql.SaveData("dbo.spSubcategoryInsert", p, "WPlannerData");
        }


        public void SaveSubcategoryToCategory(int categoryId, int subcategoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                CategoryId = categoryId,
                SubcategoryId = subcategoryId
            };

            sql.SaveData("dbo.spCategorySubcategoryInsert", p, "WPlannerData");
        }


        public void RemoveSubcategoryRecord(int subcategoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                SubcategoryId = subcategoryId
            };

            sql.SaveData("dbo.spSubcategoryRemoveAt", p, "WPlannerData");
        }


        public void RemoveSubcategoryFromCategory(int categoryId, int subcategoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                CategoryId = categoryId,
                SubcategoryId = subcategoryId
            };

            sql.SaveData("dbo.spCategorySubcategoryRemoveAt", p, "WPlannerData");
        }


        public List<SubcategoryModel> GetUnusedSubcategories(int categoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { CategoryId = categoryId };

            var output = sql.LoadData<SubcategoryModel, dynamic>("dbo.spSubcategoryGetUnused", p, "WPlannerData");

            return output;
        }
    }
}
