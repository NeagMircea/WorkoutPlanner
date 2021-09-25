using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<GoWorkoutViewEvent>, IHandle<GoProgramViewEvent>,
        IHandle<GoWorkoutAddViewEvent>, IHandle<GoExerciseViewEvent>, IHandle<GoExerciseAddViewEvent>,
        IHandle<GoCategoryViewEvent>, IHandle<GoPlayerViewEvent>, IHandle<GoSubcategoryViewEvent>, IHandle<GoMuscleViewEvent>
    {
        private IEventAggregator events;
        private SimpleContainer container;
        private ExerciseViewModel exerciseVM;

        //constructor injection
        public ShellViewModel(IEventAggregator events, SimpleContainer container)
        {
            this.events = events;
            this.container = container;
            exerciseVM = container.GetInstance<ExerciseViewModel>();

            ActivateItemAsync(container.GetInstance<ProgramViewModel>());
            events.Subscribe(this);
        }


        public async Task HandleAsync(GoWorkoutViewEvent message, CancellationToken cancellationToken)
        {
            WorkoutViewModel workoutVM = container.GetInstance<WorkoutViewModel>();

            workoutVM.ProgramEventData.Id = message.ProgramId;
            workoutVM.ProgramEventData.Name = message.ProgramName;

            await ActivateItemAsync(workoutVM);
        }


        public async Task HandleAsync(GoProgramViewEvent message, CancellationToken cancellationToken)
        {
            ProgramViewModel programVM = container.GetInstance<ProgramViewModel>();

            await ActivateItemAsync(programVM);
        }


        public async Task HandleAsync(GoWorkoutAddViewEvent message, CancellationToken cancellationToken)
        {
            WorkoutAddViewModel workoutAddVM = container.GetInstance<WorkoutAddViewModel>();

            workoutAddVM.ProgramEventData.Id = message.ProgramId;
            workoutAddVM.ProgramEventData.Name = message.ProgramName;

            await ActivateItemAsync(workoutAddVM);
        }

        public async Task HandleAsync(GoExerciseViewEvent message, CancellationToken cancellationToken)
        {
            //ExerciseViewModel exerciseVM = container.GetInstance<ExerciseViewModel>();

            exerciseVM.ProgramEventData.Id = message.ProgramId;
            exerciseVM.ProgramEventData.Name = message.ProgramName;

            exerciseVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            exerciseVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            await ActivateItemAsync(exerciseVM);
        }

        public async Task HandleAsync(GoExerciseAddViewEvent message, CancellationToken cancellationToken)
        {
            ExerciseAddViewModel exerciseAddVM = container.GetInstance<ExerciseAddViewModel>();

            exerciseAddVM.ProgramEventData.Id = message.ProgramId;

            exerciseAddVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            exerciseAddVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            exerciseAddVM.DayEventData.DayId = message.DayId;
            exerciseAddVM.DayEventData.DayName = message.DayName;
            
            await ActivateItemAsync(exerciseAddVM);
        }

        public async Task HandleAsync(GoCategoryViewEvent message, CancellationToken cancellationToken)
        {
            CategoryViewModel categoryVM = container.GetInstance<CategoryViewModel>();

            categoryVM.ProgramEventData.Id = message.ProgramId;

            categoryVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            categoryVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            categoryVM.DayEventData.DayId = message.DayId;
            categoryVM.DayEventData.DayName = message.DayName;

            await ActivateItemAsync(categoryVM);
        }

        public async Task HandleAsync(GoPlayerViewEvent message, CancellationToken cancellationToken)
        {
            PlayerViewModel playerVM = container.GetInstance<PlayerViewModel>();

            playerVM.ProgramEventData.Id = message.ProgramId;
            playerVM.ProgramEventData.Name = message.ProgramName;

            playerVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            playerVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            playerVM.ExerciseEventData.Id = message.ExerciseId;
            playerVM.ExerciseEventData.ExerciseName = message.ExerciseName;
            playerVM.ExerciseEventData.VideoPath = message.VideoPath;

            await ActivateItemAsync(playerVM);
        }

        public async Task HandleAsync(GoSubcategoryViewEvent message, CancellationToken cancellationToken)
        {
            SubcategoryViewModel subcategoryVM = container.GetInstance<SubcategoryViewModel>();

            subcategoryVM.ProgramEventData.Id = message.ProgramId;
            subcategoryVM.ProgramEventData.Name = message.ProgramName;

            subcategoryVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            subcategoryVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            subcategoryVM.DayEventData.DayId = message.DayId;
            subcategoryVM.DayEventData.DayName = message.DayName;

            subcategoryVM.CategoryEventData.CategoryId = message.CategoryId;
            subcategoryVM.CategoryEventData.CategoryName = message.CategoryName;

            await ActivateItemAsync(subcategoryVM);
        }

        public async Task HandleAsync(GoMuscleViewEvent message, CancellationToken cancellationToken)
        {
            MuscleViewModel muscleVM = container.GetInstance<MuscleViewModel>();
            
            muscleVM.ProgramEventData.Id = message.ProgramId;
            muscleVM.ProgramEventData.Name = message.ProgramName;

            muscleVM.WorkoutEventData.WorkoutId = message.WorkoutId;
            muscleVM.WorkoutEventData.WorkoutName = message.WorkoutName;

            muscleVM.ExerciseEventData.ExerciseId = message.ExerciseId;
            muscleVM.ExerciseEventData.ExerciseName = message.ExerciseName;

            await ActivateItemAsync(muscleVM);
        }
    }
}
