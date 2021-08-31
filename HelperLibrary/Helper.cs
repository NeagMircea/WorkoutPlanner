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


    }
}
