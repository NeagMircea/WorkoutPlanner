﻿using DataAccess.Library.DataAccess.Internal;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
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
    }
}