﻿using Caliburn.Micro;
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
        IHandle<GoCategoryViewEvent>
    {
        private IEventAggregator events;
        private SimpleContainer container;

        //constructor injection
        public ShellViewModel(IEventAggregator events, SimpleContainer container)
        {
            this.events = events;
            this.container = container;

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
            ExerciseViewModel exerciseVM = container.GetInstance<ExerciseViewModel>();

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
    }
}
