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
                NotifyOfPropertyChange(() => CanMoveUp);
                NotifyOfPropertyChange(() => CanMoveDown);
                NotifyOfPropertyChange(() => CanViewSelected);
                NotifyOfPropertyChange(() => CanViewMuscle);
            }
        }


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
                LoadExercises();           
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

                if (SelectedExercise != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanViewMuscle
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

        public bool CanMoveUp
        {
            get
            {
                bool output = false;

                if (SelectedExercise != null && ExerciseListBox != null
                    && ExerciseListBox.IndexOf(SelectedExercise) > 0)
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

                if (SelectedExercise != null && ExerciseListBox != null
                    && ExerciseListBox.IndexOf(SelectedExercise) < ExerciseListBox.Count - 1)
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


        public ExerciseViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        public void LoadItems()
        {
            try
            {
                ErrorMessage = "";             
                LoadDays();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        private void LoadDays()
        {
            DayData data = new DayData();
            data.InsertDaysOfTheWeek();

            DaysComboBox = new BindingList<DayDisplayModel>();

            Helper.LoadItems(
                DaysComboBox,
                data.GetAllDays,
                mapper.Map<List<DayDisplayModel>>
                );
        }


        private void LoadExercises()
        {
            ExerciseData data = new ExerciseData();
            List<ExerciseModel> exerciseList = data.GetExercisesByWorkoutDayId(
                WorkoutEventData.WorkoutId, SelectedDay.DayId);

            var exercises = mapper.Map<List<ExerciseDisplayModel>>(exerciseList);
            ExerciseListBox = new BindingList<ExerciseDisplayModel>(exercises);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            //check if we're returning to this view
            if (SelectedDay != null)
            {
                LoadExercises();
                return;
            }
                
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


        public void MoveUp()
        {
            try
            {
                ErrorMessage = "";

                ExerciseData data = new ExerciseData();
                Helper.SwapItems(ExerciseListBox, SelectedExercise, -1, data.SwapExerciseOrder, LoadExercises);
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

                ExerciseData data = new ExerciseData();
                Helper.SwapItems(ExerciseListBox, SelectedExercise, 1, data.SwapExerciseOrder, LoadExercises);
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
                ExerciseData data = new ExerciseData();

                data.RemoveWorkoutDayExerciseRecord(
                    WorkoutEventData.WorkoutId, SelectedDay.DayId, SelectedExercise.ExerciseId);

                LoadExercises();
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
                await events.PublishOnUIThreadAsync(
                    new GoPlayerViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ProgramEventData.Name,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName,

                        ExerciseId = SelectedExercise.ExerciseId,
                        ExerciseName = SelectedExercise.ExerciseName,
                        VideoPath = SelectedExercise.VideoPath
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public async Task ViewMuscle()
        {
            try
            {
                await events.PublishOnUIThreadAsync(
                    new GoMuscleViewEvent
                    {
                        ProgramId = ProgramEventData.Id,
                        ProgramName = ProgramEventData.Name,

                        WorkoutId = WorkoutEventData.WorkoutId,
                        WorkoutName = WorkoutEventData.WorkoutName,

                        ExerciseId = SelectedExercise.ExerciseId,
                        ExerciseName = SelectedExercise.ExerciseName
                    });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
