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
    public class ShellViewModel : Conductor<object>, IHandle<SelectProgramEvent>, IHandle<ActivateProgramViewEvent>
    {
        private IEventAggregator events;
        private SimpleContainer container;
        //private WorkoutViewModel workoutVM;

        //constructor injection
        public ShellViewModel(IEventAggregator events, /*WorkoutViewModel workoutVM,*/ SimpleContainer container)
        {
            this.events = events;
            //this.workoutVM = workoutVM;
            this.container = container;

            ActivateItemAsync(container.GetInstance<ProgramViewModel>());
            events.Subscribe(this);
        }


        public async Task HandleAsync(SelectProgramEvent message, CancellationToken cancellationToken)
        {
            WorkoutViewModel workoutVM = container.GetInstance<WorkoutViewModel>();

            workoutVM.ProgramId = message.Id;
            workoutVM.ViewTitle = message.ProgramName;

            await ActivateItemAsync(workoutVM);
        }


        public async Task HandleAsync(ActivateProgramViewEvent message, CancellationToken cancellationToken)
        {
            ProgramViewModel programVM = container.GetInstance<ProgramViewModel>();

            await ActivateItemAsync(programVM);
        }
    }
}
