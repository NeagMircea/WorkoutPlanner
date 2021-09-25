using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                LoadSubcategoriesByCategory();
                NotifyOfPropertyChange(() => CanResetFilters);
            }
        }

        private BindingList<SubcategoryDisplayModel> subcategoryComboBox;
        public BindingList<SubcategoryDisplayModel> SubcategoryComboBox
        {
            get
            {
                return subcategoryComboBox;
            }
            set
            {
                subcategoryComboBox = value;
                NotifyOfPropertyChange(() => SubcategoryComboBox);
            }
        }

        private ObservableCollection<SubcategoryDisplayModel> selectedSubcategories = new ObservableCollection<SubcategoryDisplayModel>();
        public ObservableCollection<SubcategoryDisplayModel> SelectedSubcategories
        {
            get
            {
                return selectedSubcategories;
            }
            set
            {
                selectedSubcategories = value;
                NotifyOfPropertyChange(() => SelectedSubcategories);
            }
        }

        private string subcategoryCBText;
        public string SubcategoryCBText
        {
            get { return subcategoryCBText; }
            set
            {
                subcategoryCBText = value;
                NotifyOfPropertyChange(() => SubcategoryCBText);
            }
        }

        public bool CanResetFilters
        {
            get
            {
                bool output = false;

                if (SelectedCategory != null)
                {
                    output = true;
                }

                return output;
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

        //private string newVideoPath = "Video Path";
        //public string NewVideoPath
        //{
        //    get
        //    {
        //        return newVideoPath;
        //    }
        //    set
        //    {
        //        newVideoPath = value;
        //        NotifyOfPropertyChange(() => NewVideoPath);
        //    }
        //}

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
                LoadNewSubcategoriesByCategory();
            }
        }

        private BindingList<SubcategoryDisplayModel> newSubcategoryComboBox;
        public BindingList<SubcategoryDisplayModel> NewSubcategoryComboBox
        {
            get
            {
                return newSubcategoryComboBox;
            }
            set
            {
                newSubcategoryComboBox = value;
                NotifyOfPropertyChange(() => NewSubcategoryComboBox);
            }
        }

        private ObservableCollection<SubcategoryDisplayModel> selectedNewSubcategories = new ObservableCollection<SubcategoryDisplayModel>();
        public ObservableCollection<SubcategoryDisplayModel> SelectedNewSubcategories
        {
            get
            {
                return selectedNewSubcategories;
            }
            set
            {
                selectedNewSubcategories = value;
                NotifyOfPropertyChange(() => SelectedNewSubcategories);
            }
        }

        private string subcategoryNewCBText;
        public string SubcategoryNewCBText
        {
            get { return subcategoryNewCBText; }
            set
            {
                subcategoryNewCBText = value;
                NotifyOfPropertyChange(() => SubcategoryNewCBText);
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

        private string minReps = "min Reps";
        public string MinReps
        {
            get
            {
                return minReps;
            }
            set
            {
                minReps = value;
                NotifyOfPropertyChange(() => MinReps);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private string maxReps = "max Reps";
        public string MaxReps
        {
            get
            {
                return maxReps;
            }
            set
            {
                maxReps = value;
                NotifyOfPropertyChange(() => MaxReps);
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

                if (NewExerciseName?.Length > 0 && SelectedNewCategory != null 
                    /*&& SelectedNewSubcategories.Count > 0*/)
                {
                    if (!int.TryParse(ExerciseSets, out int sets) ||
                        !int.TryParse(MinReps, out int minReps) ||
                        !int.TryParse(MaxReps, out int maxReps) ||
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
                SubcategoryNewCBText = "Subcategories";
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

            CategoryComboBox = new BindingList<CategoryDisplayModel>();
            NewCategoryComboBox = new BindingList<CategoryDisplayModel>();

            Helper.LoadItems( 
                CategoryComboBox,
                data.GetAllCategories,
                mapper.Map<List<CategoryDisplayModel>>
                );

            Helper.LoadItems(
                NewCategoryComboBox,
                data.GetAllCategories,
                mapper.Map<List<CategoryDisplayModel>>
                );
        }


        private void LoadSubcategoriesByCategory()
        {
            SubcategoryData data = new SubcategoryData();
            SubcategoryComboBox = new BindingList<SubcategoryDisplayModel>();

            Helper.LoadItems(
                SubcategoryComboBox, 
                SelectedCategory,
                data.GetSubcategoryByCategoryId,
                mapper.Map<List<SubcategoryDisplayModel>>
                );
        }


        private void LoadNewSubcategoriesByCategory()
        {
            SubcategoryData data = new SubcategoryData();
            NewSubcategoryComboBox = new BindingList<SubcategoryDisplayModel>();

            Helper.LoadItems(
                NewSubcategoryComboBox,
                SelectedNewCategory,
                data.GetSubcategoryByCategoryId,
                mapper.Map<List<SubcategoryDisplayModel>>
                );
        }


        private void FilterExercisesByCategories()
        {
            if (SelectedCategory == null)
            {
                LoadExercises();
                return;
            }

            ExerciseData data = new ExerciseData();
            List<ExerciseModel> exerciseList = data.GetExerciseByCategory(SelectedCategory.CategoryId);

            var exercises = mapper.Map<List<ExerciseDisplayModel>>(exerciseList);

            ExistingExercises = new BindingList<ExerciseDisplayModel>(exercises);
        }


        private void FilterExercisesBySubcategories()
        {
            if (SelectedSubcategories == null || SelectedSubcategories.Count == 0)
            {
                FilterExercisesByCategories();
                return;
            }

            ExerciseData data = new ExerciseData();

            List<ExerciseModel> exerciseList = data.GetExerciseByCategorySubcat(
                
                SelectedCategory.CategoryId, 
                Helper.GetIdsFromCollection(SelectedSubcategories)
                );

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
            try
            {
                ErrorMessage = "";

                ExerciseData data = new ExerciseData();

                ExerciseModel newExercise = new ExerciseModel
                {
                    ExerciseName = NewExerciseName,
                    VideoPath = "",
                    Sets = int.Parse(ExerciseSets),
                    MinReps = int.Parse(this.MinReps),
                    MaxReps = int.Parse(this.MaxReps),
                    Duration = float.Parse(ExerciseDuration)
                };

                data.SaveExerciseRecord(newExercise, SelectedNewCategory.CategoryId,
                    Helper.GetIdsFromCollection(SelectedNewSubcategories));

                LoadItems();
                ResetAddForm();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
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

        public void ResetFilters()
        {
            SelectedCategory = null;

            SelectedSubcategories.Clear();

            FilterExercisesByCategories();
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


        public void SetCBText()
        {
            FilterExercisesBySubcategories();

            if (SelectedSubcategories == null)
            {
                SubcategoryCBText = "0";
                return;
            }

            SubcategoryCBText = SelectedSubcategories.Count.ToString();
        }


        public void SetNewCBText()
        {
            NotifyOfPropertyChange(() => CanAddNew);

            if (SelectedNewSubcategories == null)
            {
                SubcategoryNewCBText = "0";
                return;
            }

            SubcategoryNewCBText = SelectedNewSubcategories.Count.ToString();
        }


        public void ResetAddForm()
        {
            NewExerciseName = "ExerciseName";
            SelectedNewCategory = null;
            SelectedNewSubcategories.Clear();
            SubcategoryNewCBText = "Subcategories";
            ExerciseDuration = "Duration";
            ExerciseSets = "Sets";
            MinReps = "min Reps";
            MaxReps = "max Reps";
        }
    }
}
