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
    class SubcategoryViewModel : Screen, IAddView, IAddSelectedItem
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();
        public DayModel DayEventData { get; set; } = new DayModel();
        public CategoryModel CategoryEventData { get; set; } = new CategoryModel();

        private BindingList<SubcategoryDisplayModel> allSubcategories;
        public BindingList<SubcategoryDisplayModel> AllSubcategories
        {
            get { return allSubcategories; }
            set
            {
                allSubcategories = value;
                NotifyOfPropertyChange(() => AllSubcategories);
            }
        }

        private SubcategoryDisplayModel selectedSubcategoryAll;
        public SubcategoryDisplayModel SelectedSubcategoryAll
        {
            get
            {
                return selectedSubcategoryAll;
            }
            set
            {
                selectedSubcategoryAll = value;
                NotifyOfPropertyChange(() => SelectedSubcategoryAll);
                NotifyOfPropertyChange(() => CanRemoveSelected);
            }
        }

        private BindingList<SubcategoryDisplayModel> unusedSubcategories;
        public BindingList<SubcategoryDisplayModel> UnusedSubcategories
        {
            get { return unusedSubcategories; }
            set
            {
                unusedSubcategories = value;
                NotifyOfPropertyChange(() => UnusedSubcategories);
            }
        }

        private SubcategoryDisplayModel selectedSubcategoryUnused;
        public SubcategoryDisplayModel SelectedSubcategoryUnused
        {
            get
            {
                return selectedSubcategoryUnused;
            }
            set
            {
                selectedSubcategoryUnused = value;
                NotifyOfPropertyChange(() => SelectedSubcategoryUnused);
                NotifyOfPropertyChange(() => CanAddSelected);
                NotifyOfPropertyChange(() => CanRemoveSelected);
            }
        }

        private string newSubcategoryName;
        public string NewSubcategoryName
        {
            get { return newSubcategoryName; }
            set
            {
                newSubcategoryName = value;
                NotifyOfPropertyChange(() => NewSubcategoryName);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewSubcategoryName?.Length > 0) 
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

                if (SelectedSubcategoryAll != null) 
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanAddSelected
        {
            get
            {
                bool output = false;

                if (SelectedSubcategoryUnused != null)
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


        public SubcategoryViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
        }


        public void LoadItems()
        {
            LoadAllSubcategories();
            LoadUnusedSubcategories();
        }


        private void LoadAllSubcategories()
        {    
            AllSubcategories = new BindingList<SubcategoryDisplayModel>();
            SubcategoryData data = new SubcategoryData();

            Helper.LoadItems(
                AllSubcategories, 
                data.GetAllSubcategories, 
                mapper.Map<List<SubcategoryDisplayModel>>
                );
        }


        private void LoadUnusedSubcategories()
        {
            SubcategoryData data = new SubcategoryData();
            List<SubcategoryModel> subcatList = data.GetUnusedSubcategories(CategoryEventData.CategoryId);

            var subcategories = mapper.Map<List<SubcategoryDisplayModel>>(subcatList);

            UnusedSubcategories = new BindingList<SubcategoryDisplayModel>(subcategories);
        }


        public void AddNew()
        {
            SubcategoryData data = new SubcategoryData();
            data.SaveSubcategoryRecord(NewSubcategoryName);

            LoadAllSubcategories();
            LoadUnusedSubcategories();
        }


        public void AddSelected()
        {
            SubcategoryData data = new SubcategoryData();
            data.SaveSubcategoryToCategory(CategoryEventData.CategoryId, SelectedSubcategoryUnused.SubcategoryId);

            LoadUnusedSubcategories();
        }


        public void RemoveSelected()
        {
            SubcategoryData data = new SubcategoryData();
            data.RemoveSubcategoryRecord(SelectedSubcategoryAll.SubcategoryId);

            LoadAllSubcategories();
            LoadUnusedSubcategories();
        }


        public async Task GoBack()
        {
            try
            {
                ErrorMessage = "";

                await events.PublishOnUIThreadAsync(new GoCategoryViewEvent
                {
                    ProgramId = ProgramEventData.Id,
                    ProgramName = ProgramEventData.Name,

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
    }
}
