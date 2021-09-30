using Caliburn.Micro;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Views.Interfaces;

namespace WpfDesktopUI.ViewModels
{
    public class PlayerViewModel : Screen, IGoBack
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();
        public ExerciseModel ExerciseEventData { get; set; } = new ExerciseModel();

        private string viewTitle;
        public string ViewTitle
        {
            get { return viewTitle; }
            set
            {
                viewTitle = value;
                NotifyOfPropertyChange(() => ViewTitle);
            }
        }

        private Uri playerSource;
        public Uri PlayerSource
        {
            get { return playerSource; }
            set
            {
                playerSource = value;
                NotifyOfPropertyChange(() => PlayerSource);
            }
        }

        public bool CanGoBack
        {
            get
            {
                return true;
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


        public PlayerViewModel(IEventAggregator events)
        {
            this.events = events;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            ViewTitle = $"The {ExerciseEventData.ExerciseName}";
            SetupPlayer();
        }


        private void SetupPlayer()
        {
            if (ExerciseEventData.ExerciseName?.Length > 0) 
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                PlayerSource = new Uri($@"{baseDirectory}Media\Videos\{ExerciseEventData.ExerciseName}.mp4");
                //PlayerSource = new Uri($@"{baseDirectory}Videos\Super Meat Boy E3 2010 Trailer.mp4");
            }
            else
            {
                ErrorMessage = "Could not find your video";
            }     
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
    }
}
