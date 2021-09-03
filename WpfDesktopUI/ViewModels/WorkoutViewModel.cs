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
using WpfDesktopUI.Views.Interfaces.Composite;

namespace WpfDesktopUI.ViewModels
{
    public class WorkoutViewModel : Screen, IDefaultView
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

        private BindingList<WorkoutProgramDisplayModel> workoutListBox;
        public BindingList<WorkoutProgramDisplayModel> WorkoutListBox
        {
            get
            {
                return workoutListBox;
            }
            set
            {
                workoutListBox = value;
                NotifyOfPropertyChange(() => WorkoutListBox);
            }
        }

        private WorkoutProgramDisplayModel selectedWorkout;
        public WorkoutProgramDisplayModel SelectedWorkout
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
                NotifyOfPropertyChange(() => CanViewSelected);
                NotifyOfPropertyChange(() => CanRemoveSelected);
            }
        }


        public bool CanAddNew
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

                if (SelectedWorkout != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanViewSelected
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

        public bool CanMoveUp
        {
            get
            {
                bool output = false;

                if (SelectedWorkout != null && WorkoutListBox != null
                    && WorkoutListBox.IndexOf(SelectedWorkout) > 0)
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

                if (SelectedWorkout != null && WorkoutListBox != null
                    && WorkoutListBox.IndexOf(SelectedWorkout) < WorkoutListBox.Count - 1)
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


        public WorkoutViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        public void LoadItems()
        {
            WorkoutProgramData data = new WorkoutProgramData();
            List<WorkoutProgramModel> workoutList = data.GetWorkoutsByProgramId(ProgramEventData.Id);

            var workouts = mapper.Map<List<WorkoutProgramDisplayModel>>(workoutList);

            WorkoutListBox = new BindingList<WorkoutProgramDisplayModel>(workouts);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            ViewTitle = ProgramEventData.Name;
            LoadItems();
        }


        public async Task AddNew()
        {
            try
            {
                ErrorMessage = "";
                await events.PublishOnUIThreadAsync(
                    new GoWorkoutAddViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ViewTitle
                    });
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

                WorkoutProgramData data = new WorkoutProgramData();
                data.RemoveWorkoutFromProgram(ProgramEventData.Id, SelectedWorkout.WorkoutId);

                LoadItems();            
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            } 
        }


        public async Task ViewSelected()
        {
            try
            {
                ErrorMessage = "";
                await events.PublishOnUIThreadAsync(
                    new GoExerciseViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ViewTitle,
                        WorkoutId = SelectedWorkout.WorkoutId,
                        WorkoutName = SelectedWorkout.WorkoutName
                    });
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

                WorkoutProgramData data = new WorkoutProgramData();
                Helper.SwapItems(WorkoutListBox, SelectedWorkout, -1, data.SwapWorkoutProgramOrder, LoadItems);
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

                WorkoutProgramData data = new WorkoutProgramData();
                Helper.SwapItems(WorkoutListBox, SelectedWorkout, 1, data.SwapWorkoutProgramOrder, LoadItems);
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
                await events.PublishOnUIThreadAsync(new GoProgramViewEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
