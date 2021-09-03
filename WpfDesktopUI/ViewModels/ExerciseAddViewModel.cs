using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Models;
using WpfDesktopUI.Views.Interfaces;
using WpfDesktopUI.Views.Interfaces.Composite;

namespace WpfDesktopUI.ViewModels
{
    public class ExerciseAddViewModel : Screen, IAddView, IAddSelectedItem
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();
        public DayModel DayEventData { get; set; } = new DayModel();

        private string viewTitle;
        public string ViewTitle
        {
            get
            {
                return viewTitle;
            }
            set
            {
                viewTitle = value;
                NotifyOfPropertyChange(() => ViewTitle);
            }
        }

        private BindingList<ExerciseDisplayModel> existingExercises;
        public BindingList<ExerciseDisplayModel> ExistingExercises
        {
            get
            {
                return existingExercises;
            }
            set
            {
                existingExercises = value;
                NotifyOfPropertyChange(() => ExistingExercises);
            }
        }

        private ExerciseDisplayModel selectedExercise;
        public ExerciseDisplayModel SelectedExercise
        {
            get
            {
                return selectedExercise;
            }
            set
            {
                selectedExercise = value;
                NotifyOfPropertyChange(() => SelectedExercise);
                NotifyOfPropertyChange(() => CanAddSelected);
                NotifyOfPropertyChange(() => CanRemoveSelected);                
            }
        }

        private BindingList<CategoryDisplayModel> categoryComboBox;
        public BindingList<CategoryDisplayModel> CategoryComboBox
        {
            get
            {
                return categoryComboBox;
            }
            set
            {
                categoryComboBox = value;
                NotifyOfPropertyChange(() => CategoryComboBox);
            }
        }

        private CategoryDisplayModel selectedCategory;
        public CategoryDisplayModel SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
                FilterExercisesByCategories();
            }
        }

        private string newExerciseName = "Exercise Name";
        public string NewExerciseName
        {
            get
            {
                return newExerciseName;
            }
            set
            {
                newExerciseName = value;
                NotifyOfPropertyChange(() => NewExerciseName);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private string newVideoPath = "Video Path";
        public string NewVideoPath
        {
            get
            {
                return newVideoPath;
            }
            set
            {
                newVideoPath = value;
                NotifyOfPropertyChange(() => NewVideoPath);
            }
        }

        private BindingList<CategoryDisplayModel> newCategoryComboBox;
        public BindingList<CategoryDisplayModel> NewCategoryComboBox
        {
            get
            {
                return newCategoryComboBox;
            }
            set
            {
                newCategoryComboBox = value;
                NotifyOfPropertyChange(() => NewCategoryComboBox);
            }
        }


        private CategoryDisplayModel selectedNewCategory;
        public CategoryDisplayModel SelectedNewCategory
        {
            get
            {
                return selectedNewCategory;
            }
            set
            {
                selectedNewCategory = value;
                NotifyOfPropertyChange(() => SelectedNewCategory);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private string exerciseSets = "Sets";
        public string ExerciseSets
        {
            get
            {
                return exerciseSets;
            }
            set
            {
                exerciseSets = value;
                NotifyOfPropertyChange(() => ExerciseSets);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private string exerciseReps = "Reps";
        public string ExerciseReps
        {
            get
            {
                return exerciseReps;
            }
            set
            {
                exerciseReps = value;
                NotifyOfPropertyChange(() => ExerciseReps);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private string exerciseDuration = "Duration";
        public string ExerciseDuration
        {
            get
            {
                return exerciseDuration;
            }
            set
            {
                exerciseDuration = value;
                NotifyOfPropertyChange(() => ExerciseDuration);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        public bool CanAddSelected
        {
            get
            {
                bool output = false;

                if (SelectedExercise != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanAddNewCategory
        {
            get
            {
                return true;
            }
        }

        public bool CanRemoveSelected
        {
            get
            {
                bool output = false;

                if (SelectedExercise != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewExerciseName?.Length > 0 && SelectedNewCategory != null)
                {
                    if (!int.TryParse(ExerciseSets, out int sets) ||
                        !int.TryParse(ExerciseReps, out int reps) ||
                        !float.TryParse(ExerciseDuration, out float duration))
                    {
                        return false;
                    }

                    output = true;
                }

                return output;
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        private IEventAggregator events;
        private IMapper mapper;


        public ExerciseAddViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        public void LoadItems()
        {
            try
            {
                LoadExercises();
                LoadCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }


        private void LoadExercises()
        {
            ExerciseData data = new ExerciseData();
            List<ExerciseModel> exerciseList = data.GetAllExercises();

            var exercises = mapper.Map<List<ExerciseDisplayModel>>(exerciseList);

            ExistingExercises = new BindingList<ExerciseDisplayModel>(exercises);
        }


        private void LoadCategories()
        {
            CategoryData data = new CategoryData();
            List<CategoryModel> categoryList = data.GetAllCategories();

            var categories = mapper.Map<List<CategoryDisplayModel>>(categoryList);

            CategoryComboBox = new BindingList<CategoryDisplayModel>(categories);
            NewCategoryComboBox = new BindingList<CategoryDisplayModel>(categories);
        }


        private void FilterExercisesByCategories()
        {
            ExerciseData data = new ExerciseData();
            List<ExerciseModel> exerciseList = data.GetExerciseByCategory(SelectedCategory.CategoryId);

            var exercises = mapper.Map<List<ExerciseDisplayModel>>(exerciseList);

            ExistingExercises = new BindingList<ExerciseDisplayModel>(exercises);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
            ViewTitle = $"{WorkoutEventData.WorkoutName} - {DayEventData.DayName}";
        }


        public void AddNew()
        {
            ExerciseData data = new ExerciseData();

            ExerciseModel newExercise = new ExerciseModel
            {
                ExerciseName = NewExerciseName,
                VideoPath = NewVideoPath,
                Sets = int.Parse(ExerciseSets),
                Reps = int.Parse(ExerciseReps),
                Duration = float.Parse(ExerciseDuration)
            };

            data.SaveExerciseRecord(newExercise, SelectedNewCategory.CategoryId);
            LoadItems();
        }


        public async Task AddNewCategory()
        {
            try
            {
                ErrorMessage = "";
                await events.PublishOnUIThreadAsync(
                    new GoCategoryViewEvent
                    {
                        ProgramId = ProgramEventData.Id,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName,

                        DayId = DayEventData.DayId,
                        DayName = DayEventData.DayName
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }          
        }


        public void AddSelected()
        {
            ExerciseData data = new ExerciseData();

            data.SaveExerciseRecord(WorkoutEventData.WorkoutId, DayEventData.DayId, SelectedExercise.ExerciseId);
        }


        public async Task GoBack()
        {
            try
            {
                ErrorMessage = "";

                await events.PublishOnUIThreadAsync(
                    new GoExerciseViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ProgramEventData.Name,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName             
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    

        public void RemoveSelected()
        {
            ExerciseData data = new ExerciseData();
            data.RemoveExerciseRecord(SelectedExercise.ExerciseId);
            LoadItems();
        }
    }
}
