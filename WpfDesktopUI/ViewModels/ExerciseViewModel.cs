using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
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
    public class ExerciseViewModel : Screen, IDefaultView
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();

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
        //exercises <-> workout <-> days
        private BindingList<ExerciseDisplayModel> exerciseListBox;
        public BindingList<ExerciseDisplayModel> ExerciseListBox
        {
            get
            {
                return exerciseListBox;
            }
            set
            {
                exerciseListBox = value;
                NotifyOfPropertyChange(() => ExerciseListBox);
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
                NotifyOfPropertyChange(() => CanRemoveSelected);
                
            }
        }

        //DaysComboBox
        private BindingList<DayDisplayModel> daysComboBox;
        public BindingList<DayDisplayModel> DaysComboBox
        {
            get
            {
                return daysComboBox;
            }
            set
            {
                daysComboBox = value;
                NotifyOfPropertyChange(() => DaysComboBox);
            }
        }

        private DayDisplayModel selectedDay;
        public DayDisplayModel SelectedDay
        {
            get
            {
                return selectedDay;
            }
            set
            {
                selectedDay = value;
                NotifyOfPropertyChange(() => SelectedDay);
                NotifyOfPropertyChange(() => CanAddNew);
                
            }
        }

        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (SelectedDay != null)
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

                if (SelectedExercise != null)
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

                if (true)
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

                if (true)
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

                if (true)
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

                if (true)
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


        public ExerciseViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        public void LoadItems()
        {
            DayData data = new DayData();
            List<DayModel> dayList = data.GetAllDays();

            var days = mapper.Map<List<DayDisplayModel>>(dayList);

            DaysComboBox = new BindingList<DayDisplayModel>(days);

            //WorkoutProgramData data = new WorkoutProgramData();
            //List<WorkoutProgramModel> workoutList = data.GetWorkoutsByProgramId(ProgramId);

            //var workouts = mapper.Map<List<WorkoutProgramDisplayModel>>(workoutList);

            //WorkoutListBox = new BindingList<WorkoutProgramDisplayModel>(workouts);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
            ViewTitle = WorkoutEventData.WorkoutName;
        }


        public async Task AddNew()
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

                        DayId = SelectedDay.DayId,
                        DayName = SelectedDay.DayName
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public Task GoBack()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void RemoveSelected()
        {
            throw new NotImplementedException();
        }

        public Task ViewSelected()
        {
            throw new NotImplementedException();
        }
    }
}
