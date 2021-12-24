using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Views.Interfaces;

namespace WpfDesktopUI.ViewModels
{
    public class MuscleViewModel : Screen, IGoBack
    {
        public ProgramModel ProgramEventData { get; set; } = new ProgramModel();
        public WorkoutModel WorkoutEventData { get; set; } = new WorkoutModel();
        public ExerciseModel ExerciseEventData { get; set; } = new ExerciseModel();

        private List<SubcategoryModel> subcategoryList;
        private int subcatIndex;

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

        private string muscleName;
        public string MuscleName
        {
            get { return muscleName; }
            set
            {
                muscleName = value;
                NotifyOfPropertyChange(() => MuscleName);
            }
        }          

        private Uri imageSource;
        public Uri ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }

        public bool CanGoPrev
        {
            get
            {
                bool output = false;

                if (subcategoryList != null && subcategoryList.Count > 0
                    && subcatIndex - 1 >= 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanGoNext
        {
            get
            {
                bool output = false;

                if (subcategoryList != null && subcategoryList.Count > 0
                    && subcatIndex + 1 < subcategoryList.Count)
                {
                    output = true;
                }

                return output;
            }
        }

        private string quickInfo;
        public string QuickInfo
        {
            get { return quickInfo; }
            set
            {
                quickInfo = value;
                NotifyOfPropertyChange(() => QuickInfo);
            }
        }

        private string muscleInfo;
        public string MuscleInfo
        {
            get { return muscleInfo; }
            set
            {
                muscleInfo = value;
                NotifyOfPropertyChange(() => MuscleInfo);
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


        public MuscleViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            ViewTitle = $"The {ExerciseEventData.ExerciseName}";

            LoadSubcategories();

            if (subcategoryList == null || subcategoryList.Count == 0)
                return;

            UpdateNameAndImage();
            UpdateQuickInfo();
            UpdateMuscleInfo();
        }


        private void LoadSubcategories()
        {
            try
            {
                ErrorMessage = "";

                SubcategoryData data = new SubcategoryData();

                subcategoryList = new List<SubcategoryModel>(
                    data.GetSubcategoriesByExerciseId(ExerciseEventData.ExerciseId));

                subcatIndex = 0;              
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        private void UpdateNameAndImage()
        {
            MuscleName = $"The {subcategoryList[subcatIndex].SubcategoryName}";

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ImageSource = new Uri($@"{baseDirectory}Media\Images\{subcategoryList[subcatIndex].SubcategoryName}.png");
        }


        private void UpdateQuickInfo()
        {
            QuickInfo = "This exercise targets ";

            for (int i = 0; i < subcategoryList.Count; i++)
            {
                if (i == subcategoryList.Count - 1)
                {
                    if (subcategoryList.Count > 1)
                    {
                        QuickInfo += $"and the {subcategoryList[i].SubcategoryName}.";
                    }
                    else
                    {
                        QuickInfo += $"the {subcategoryList[i].SubcategoryName}.";
                    }

                    continue;
                }

                QuickInfo += $"the {subcategoryList[i].SubcategoryName}, ";
            }         
        }


        private void UpdateMuscleInfo()
        {
            MuscleInfo = subcategoryList[subcatIndex].SubcategoryInfo;
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


        public void GoPrev()
        {
            subcatIndex--;
            UpdateNameAndImage();
            UpdateMuscleInfo();
            NotifyOfPropertyChange(() => CanGoPrev);
            NotifyOfPropertyChange(() => CanGoNext);
        }


        public void GoNext()
        {
            subcatIndex++;
            UpdateNameAndImage();
            UpdateMuscleInfo();
            NotifyOfPropertyChange(() => CanGoPrev);
            NotifyOfPropertyChange(() => CanGoNext);
        }



        //Source="/Media/Images/not_found.png"
    }
}
