using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
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
    public class WorkoutAddViewModel : Screen, IAddView, IMoveItem, IAddSelectedItem
    {
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

        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();

        private BindingList<WorkoutDisplayModel> existingWorkouts;
        public BindingList<WorkoutDisplayModel> ExistingWorkouts
        {
            get
            {
                return existingWorkouts;
            }
            set
            {
                existingWorkouts = value;
                NotifyOfPropertyChange(() => ExistingWorkouts);
            }
        }

        private WorkoutDisplayModel selectedWorkout;
        public WorkoutDisplayModel SelectedWorkout
        {
            get
            {
                return selectedWorkout;
            }
            set
            {
                selectedWorkout = value;
                NotifyOfPropertyChange(() => SelectedWorkout);
                NotifyOfPropertyChange(() => CanMoveUp);
                NotifyOfPropertyChange(() => CanMoveDown);
                NotifyOfPropertyChange(() => CanRemoveSelected);
                NotifyOfPropertyChange(() => CanAddSelected);
            }
        }

        private string newWorkoutName;
        public string NewWorkoutName
        {
            get
            {
                return newWorkoutName;
            }
            set
            {
                newWorkoutName = value;
                NotifyOfPropertyChange(() => NewWorkoutName);
                NotifyOfPropertyChange(() => CanAddNew);
            }
        }

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewWorkoutName?.Length > 0)
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

                if (SelectedWorkout != null)
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

                if (SelectedWorkout != null)
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

        public bool CanMoveUp
        {
            get
            {
                bool output = false;

                if (SelectedWorkout != null && ExistingWorkouts != null
                    && ExistingWorkouts.IndexOf(SelectedWorkout) > 0)
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

                if (SelectedWorkout != null && ExistingWorkouts != null
                    && ExistingWorkouts.IndexOf(SelectedWorkout) < ExistingWorkouts.Count - 1)
                {
                    output = true;
                }

                return output;
            }
        }

        private IEventAggregator events;
        private IMapper mapper;


        public WorkoutAddViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            ViewTitle = $"{ProgramEventData.Name} - add workout";
            LoadItems();
        }


        public void LoadItems()
        {
            try
            {
                ErrorMessage = "";
                LoadWorkouts();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }


        private void LoadWorkouts()
        {
            WorkoutData data = new WorkoutData();
            ExistingWorkouts = new BindingList<WorkoutDisplayModel>();

            Helper.LoadItems(
                ExistingWorkouts,
                data.GetAllWorkouts,
                mapper.Map<List<WorkoutDisplayModel>>
                );
        }


        public void AddNew()
        {
            try
            {
                ErrorMessage = "";

                WorkoutData data = new WorkoutData();
                data.SaveWorkoutRecord(NewWorkoutName);

                LoadItems();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    ErrorMessage = $"Workout '{NewWorkoutName}' already exists!";
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


        public void AddSelected()
        {
            try
            {
                ErrorMessage = "";

                WorkoutProgramData data = new WorkoutProgramData();
                data.AddWorkoutToProgram(ProgramEventData.Id, SelectedWorkout.WorkoutId);

                LoadItems();
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
                ErrorMessage = "";

                WorkoutData data = new WorkoutData();
                data.DeleteWorkoutRecord(SelectedWorkout.WorkoutId);

                LoadItems();
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

                WorkoutData data = new WorkoutData();
                Helper.SwapItems(ExistingWorkouts, SelectedWorkout, -1, data.SwapWorkoutOrder, LoadItems);
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

                WorkoutData data = new WorkoutData();
                Helper.SwapItems(ExistingWorkouts, SelectedWorkout, 1, data.SwapWorkoutOrder, LoadItems);

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
                    new GoWorkoutViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ProgramEventData.Name
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
