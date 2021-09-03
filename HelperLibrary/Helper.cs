using HelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public static class Helper
    {
        public static void Swap<T>(IList<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2]; 
            list[index2] = temp;
        }


        public static void LoadItems<T, U, V>(V container, 
            Func<List<T>> dataFunc, Func<List<T>, List<U>> mapFunc) where V : Collection<U>
        {
            List<T> itemList = dataFunc();

            var items = mapFunc(itemList);

            container.Clear();

            foreach (var item in items)
            {
                container.Add(item);
            }

            //ExerciseData data = new ExerciseData();
            //List<ExerciseModel> exerciseList = data.GetAllExercises();

            //var exercises = mapper.Map<List<ExerciseDisplayModel>>(exerciseList);

            //ExistingExercises = new BindingList<ExerciseDisplayModel>(exercises);


            //Helper.LoadItems(ExistingExercises,
            //    new ExerciseData().GetAllExercises,
            //    mapper.Map<List<ExerciseDisplayModel>>);
        }

        /// <summary>
        /// Swap the locations of two items from a Collection of IDisplayModel
        /// </summary>
        /// <typeparam name="T">DisplayModel type</typeparam>
        /// <param name="itemList"></param>
        /// <param name="selectedItem"></param>
        /// <param name="dataFunc">database function used to swap Collection items</param>
        /// <param name="loadFunc">the function used to reload the items in the Collection</param>
        /// <param name="step">the swap step, negative for upwards, positive for downwards</param>
        public static void SwapItems<T>(Collection<T> itemList, T selectedItem, int step,
            Action<int, int, int, int> dataFunc, Action loadFunc) where T : IDisplayModel
        {
            int index1 = itemList.IndexOf(selectedItem);
            int index2 = index1 + step;

            int dbIndex1 = itemList[index1].GetId;
            int dbIndex2 = itemList[index2].GetId;

            int dbOrder1 = itemList[index1].GetOrder;
            int dbOrder2 = itemList[index2].GetOrder;

            dataFunc(dbIndex1, dbOrder1, dbIndex2, dbOrder2);

            loadFunc();

            //int index1 = CategoryListBox.IndexOf(SelectedCategory);
            //int index2 = index1 - 1;

            //int dbIndex1 = CategoryListBox[index1].CategoryId;
            //int dbIndex2 = CategoryListBox[index2].CategoryId;

            //int dbOrder1 = CategoryListBox[index1].CategoryOrder;
            //int dbOrder2 = CategoryListBox[index2].CategoryOrder;

            //CategoryData data = new CategoryData();

            //data.SwapCategoryOrder(dbIndex1, dbOrder1, dbIndex2, dbOrder2);

            //LoadCategories();
        }
    }
}
