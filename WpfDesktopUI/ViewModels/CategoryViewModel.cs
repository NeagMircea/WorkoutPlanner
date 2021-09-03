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
    public class CategoryViewModel : Screen, IAddView, IMoveItem
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
                NotifyOfPropertyChange(() => CanMoveUp);
                NotifyOfPropertyChange(() => CanMoveDown);
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

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewCategoryName?.Length > 0)
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

        public bool CanMoveUp
        {
            get
            {
                bool output = false;

                if (SelectedCategory != null && CategoryListBox != null
                    && CategoryListBox.IndexOf(SelectedCategory) > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanMoveDown
        {
            get
            {
                bool output = false;

                if (SelectedCategory != null && CategoryListBox != null
                    && CategoryListBox.IndexOf(SelectedCategory) < CategoryListBox.Count - 1)
                {
                    output = true;
                }

                return output;
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
                LoadCategories();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }


        private void LoadCategories()
        {
            CategoryData data = new CategoryData();
            List<CategoryModel> categoryList = data.GetAllCategories();

            var categories = mapper.Map<List<CategoryDisplayModel>>(categoryList);

            CategoryListBox= new BindingList<CategoryDisplayModel>(categories);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
            ViewTitle = "Category Panel";
        }


        public void AddNew()
        {
            try
            {
                CategoryData data = new CategoryData();
                data.SaveCategoryRecord(NewCategoryName);

                LoadCategories();
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
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }


        public void MoveUp()
        {
            try
            {
                ErrorMessage = "";

                CategoryData data = new CategoryData();
                Helper.SwapItems(CategoryListBox, SelectedCategory, -1, data.SwapCategoryOrder, LoadCategories);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public void MoveDown()
        {
            try
            {
                ErrorMessage = "";

                CategoryData data = new CategoryData();
                Helper.SwapItems(CategoryListBox, SelectedCategory, 1, data.SwapCategoryOrder, LoadCategories);

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
    }
}
