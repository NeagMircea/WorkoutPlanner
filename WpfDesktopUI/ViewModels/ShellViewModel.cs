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
        IHandle<GoWorkoutAddViewEvent>
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

            workoutVM.ProgramId = message.ProgramId;
            workoutVM.ViewTitle = message.ProgramName;

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

            workoutAddVM.ProgramId = message.ProgramId;
            workoutAddVM.ViewTitle = message.ProgramName;

            await ActivateItemAsync(workoutAddVM);
        }
    }
}
