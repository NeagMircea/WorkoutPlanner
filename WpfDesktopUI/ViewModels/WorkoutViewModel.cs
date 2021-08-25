﻿using AutoMapper;
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

        private BindingList<WorkoutDisplayModel> workoutListBox;
        public BindingList<WorkoutDisplayModel> WorkoutListBox
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
                NotifyOfPropertyChange(() => CanViewSelected);
                NotifyOfPropertyChange(() => CanRemoveSelected);
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
            WorkoutData data = new WorkoutData();
            List<WorkoutModel> workoutList = data.GetWorkoutsByProgramId(ProgramId);

            var workouts = mapper.Map<List<WorkoutDisplayModel>>(workoutList);

            WorkoutListBox = new BindingList<WorkoutDisplayModel>(workouts);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
        }


        public void AddNew()
        {
            try
            {
                ErrorMessage = "";

                WorkoutData data = new WorkoutData();
                data.SaveWorkoutRecord(ProgramId, NewWorkoutName);

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

                Helper.Swap(WorkoutListBox, index1, index2);
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

                Helper.Swap(WorkoutListBox, index1, index2);

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
                await events.PublishOnUIThreadAsync(new ActivateProgramViewEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}