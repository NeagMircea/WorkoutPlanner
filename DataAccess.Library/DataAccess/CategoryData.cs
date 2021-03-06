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
    public class CategoryData
    {

        public List<CategoryModel> GetCategoryById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };
      
            var output = 
                sql.LoadData<CategoryModel, dynamic>("dbo.spCategoryLookup", p, "WPlannerData");

            return output;
        }


        public List<CategoryModel> GetAllCategories()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output =
                sql.LoadData<CategoryModel, dynamic>("dbo.spCategoryGetAll", new { }, "WPlannerData");

            return output;
        }


        public void SaveCategoryRecord(string categoryName, List<int> subcatIdList)
        {
            SqlDataAccess sql = new SqlDataAccess();

            //var p = new { CategoryName = categoryName };
            var p = new DynamicParameters();
            p.Add("@CategoryName", categoryName);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            sql.SaveData("dbo.spCategoryInsert", p, "WPlannerData");

            int id = p.Get<int>("@Id");

            foreach (int subcatId in subcatIdList)
            {
                var q = new
                {
                    CategoryId = id,
                    SubcategoryId = subcatId
                };

                sql.SaveData("dbo.spCategorySubcategoryInsert", q, "WPlannerData");
            }
        }


        public void RemoveCategoryRecord(int categoryId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { CategoryId = categoryId };

            sql.SaveData("dbo.spCategoryRemoveAt", p, "WPlannerData");
        }


        public void SwapCategoryOrder(int categoryOneId, int categoryOneOrder,
            int categoryTwoId, int categoryTwoOrder)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new
            {
                CategoryOneId = categoryOneId,
                CategoryOneOrder = categoryOneOrder,
                CategoryTwoId = categoryTwoId,
                CategoryTwoOrder = categoryTwoOrder
            };

            sql.SaveData("dbo.spCategorySwapOrder", p, "WPlannerData");
        }
    }
}
