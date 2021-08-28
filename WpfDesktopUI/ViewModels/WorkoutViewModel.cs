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
        public string ViewTitle { get; set; }

        private int programId;
        public int ProgramId
        {
            get
            {
                return programId;
            }
            set
            {
                programId = value;
            }
        }

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
            List<WorkoutProgramModel> workoutList = data.GetWorkoutsByProgramId(ProgramId);

            var workouts = mapper.Map<List<WorkoutProgramDisplayModel>>(workoutList);

            WorkoutListBox = new BindingList<WorkoutProgramDisplayModel>(workouts);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
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
                        ProgramId = ProgramId,
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
                data.RemoveWorkoutFromProgram(ProgramId, SelectedWorkout.WorkoutId);

                LoadItems();            
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            } 
        }


        public async Task ViewSelected()
        {
            //TODO implement this 
            await TryCloseAsync();
        }


        public void MoveUp()
        {
            try
            {
                ErrorMessage = "";

                int index1 = WorkoutListBox.IndexOf(SelectedWorkout);
                int index2 = index1 - 1;

                int dbIndex1 = WorkoutListBox[index1].WorkoutProgramId;
                int dbIndex2 = WorkoutListBox[index2].WorkoutProgramId;

                int dbOrder1 = WorkoutListBox[index1].WorkoutProgramOrder;
                int dbOrder2 = WorkoutListBox[index2].WorkoutProgramOrder;

                WorkoutProgramData data = new WorkoutProgramData();

                data.SwapWorkoutProgramOrder(dbIndex1, dbOrder1, dbIndex2, dbOrder2);

                LoadItems();
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

                int index1 = WorkoutListBox.IndexOf(SelectedWorkout);
                int index2 = index1 + 1;

                int dbIndex1 = WorkoutListBox[index1].WorkoutProgramId;
                int dbIndex2 = WorkoutListBox[index2].WorkoutProgramId;

                int dbOrder1 = WorkoutListBox[index1].WorkoutProgramOrder;
                int dbOrder2 = WorkoutListBox[index2].WorkoutProgramOrder;

                WorkoutProgramData data = new WorkoutProgramData();

                data.SwapWorkoutProgramOrder(dbIndex1, dbOrder1, dbIndex2, dbOrder2);

                LoadItems();
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
