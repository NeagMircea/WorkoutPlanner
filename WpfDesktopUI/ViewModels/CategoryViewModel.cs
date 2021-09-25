using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Models;
using WpfDesktopUI.Views.Interfaces;
using WpfDesktopUI.Views.Interfaces.Composite;

namespace WpfDesktopUI.ViewModels
{
    public class CategoryViewModel : Screen, IAddView
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();
        public DayModel DayEventData { get; set; } = new DayModel();

        string viewTitle;
        public string ViewTitle
        {
            get
            {
                return viewTitle;
            }
            private set
            {
                viewTitle = value;
                NotifyOfPropertyChange(() => ViewTitle);
            }
        }

        private BindingList<CategoryDisplayModel> categoryListBox;
        public BindingList<CategoryDisplayModel> CategoryListBox
        {
            get
            {
                return categoryListBox;
            }
            set
            {
                categoryListBox = value;
                NotifyOfPropertyChange(() => CategoryListBox);
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
                NotifyOfPropertyChange(() => CanRemoveSelected);
                NotifyOfPropertyChange(() => CanAddNewSubcategory);
                LoadSubcategoriesBySelected();
            }
        }

        private BindingList<SubcategoryDisplayModel> subcategoryListBox;
        public BindingList<SubcategoryDisplayModel> SubcategoryListBox
        {
            get { return subcategoryListBox; }
            set
            {
                subcategoryListBox = value;
                NotifyOfPropertyChange(() => SubcategoryListBox);
            }
        }

        private SubcategoryDisplayModel selectedSubcategory;
        public SubcategoryDisplayModel SelectedSubcategory
        {
            get
            {
                return selectedSubcategory;
            }
            set
            {
                selectedSubcategory = value;
                NotifyOfPropertyChange(() => SelectedSubcategory);
                NotifyOfPropertyChange(() => CanRemoveSubcategory);
            }
        }

        public bool CanAddNewSubcategory
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

        public bool CanRemoveSubcategory
        {
            get
            {
                bool output = false;

                if (SelectedSubcategory != null)
                {
                    output = true;
                }

                return output;
            }
        }

        private string newCategoryName;
        public string NewCategoryName
        {
            get
            {
                return newCategoryName;
            }
            set
            {
                newCategoryName = value;
                NotifyOfPropertyChange(() => NewCategoryName);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        private BindingList<SubcategoryDisplayModel> subcategoryComboBox;
        public BindingList<SubcategoryDisplayModel> SubcategoryComboBox
        {
            get { return subcategoryComboBox; }
            set
            {
                subcategoryComboBox = value;
                NotifyOfPropertyChange(() => SubcategoryComboBox);
            }
        }

        private ObservableCollection<SubcategoryDisplayModel> selectedSubcategoryCB = new ObservableCollection<SubcategoryDisplayModel>();
        public ObservableCollection<SubcategoryDisplayModel> SelectedSubcategoryCB
        {
            get { return selectedSubcategoryCB; ; }
            set
            {
                selectedSubcategoryCB = value;
                NotifyOfPropertyChange(() => SelectedSubcategoryCB);
                NotifyOfPropertyChange(() => CanAddNew);
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

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewCategoryName?.Length > 0 /*&& SelectedSubcategoryCB?.Count > 0*/) 
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanRemoveSelected
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

        public string errorMessage;
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

      
        public CategoryViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        public void LoadItems()
        {
            try
            {
                ErrorMessage = "";
                LoadCategories();
                LoadSubcategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        private void LoadCategories()
        {
            CategoryData data = new CategoryData();
            CategoryListBox = new BindingList<CategoryDisplayModel>();

            Helper.LoadItems(
                CategoryListBox, 
                data.GetAllCategories, 
                mapper.Map<List<CategoryDisplayModel>>
                );
        }


        private void LoadSubcategories()
        {
            SubcategoryData data = new SubcategoryData();
            SubcategoryComboBox = new BindingList<SubcategoryDisplayModel>();

            Helper.LoadItems(
                SubcategoryComboBox,
                data.GetAllSubcategories,
                mapper.Map<List<SubcategoryDisplayModel>>);
        }


        private void LoadSubcategoriesBySelected()
        {        
            SubcategoryData data = new SubcategoryData();
            SubcategoryListBox = new BindingList<SubcategoryDisplayModel>();

            Helper.LoadItems(
                SubcategoryListBox, 
                SelectedCategory,
                data.GetSubcategoryByCategoryId,
                mapper.Map<List<SubcategoryDisplayModel>>
                );
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
            ViewTitle = "Category Panel";
            ResetAddForm();
        }


        public async Task AddNewSubcategory()
        {
            try
            {
                ErrorMessage = "";

                await events.PublishOnUIThreadAsync(
                    new GoSubcategoryViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ProgramEventData.Name,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName,

                        DayId = DayEventData.DayId,
                        DayName = DayEventData.DayName,

                        CategoryId = selectedCategory.CategoryId,
                        CategoryName = selectedCategory.CategoryName
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public void RemoveSubcategory()
        {
            SubcategoryData data = new SubcategoryData();
            data.RemoveSubcategoryFromCategory(SelectedCategory.CategoryId, SelectedSubcategory.SubcategoryId);

            LoadSubcategoriesBySelected();
        }


        public void AddNew()
        {
            try
            {
                ErrorMessage = "";

                CategoryData data = new CategoryData();

                data.SaveCategoryRecord(NewCategoryName, Helper.GetIdsFromCollection(SelectedSubcategoryCB));

                LoadCategories();

                ResetAddForm();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    ErrorMessage = $"Category '{NewCategoryName}' already exists!";
                }
                else
                {
                    throw;
                }
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        
        public void RemoveSelected()
        {
            try
            {
                CategoryData data = new CategoryData();
                data.RemoveCategoryRecord(SelectedCategory.CategoryId);

                LoadCategories();
                LoadSubcategoriesBySelected();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }


        public async Task GoBack()
        {
            try
            {
                ErrorMessage = "";

                await events.PublishOnUIThreadAsync(
                    new GoExerciseAddViewEvent
                    {
                        ProgramId = ProgramEventData.Id,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName,

                        DayId = DayEventData.DayId,
                        DayName =DayEventData.DayName
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public void SetText()
        {
            NotifyOfPropertyChange(() => CanAddNew);

            if (SelectedSubcategoryCB == null)
            {
                SubcategoryCBText = "0";
                return;
            }

            SubcategoryCBText = SelectedSubcategoryCB.Count.ToString();
        }


        private void ResetAddForm()
        {
            NewCategoryName = "Category Name";
            SubcategoryCBText = "Subcategories";
        }

    }
}
