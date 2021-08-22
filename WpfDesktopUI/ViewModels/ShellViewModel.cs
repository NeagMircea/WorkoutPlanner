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
    public class ShellViewModel : Conductor<object>, IHandle<TestEvent>
    {
        private IEventAggregator events;

        private TestViewModel testVM;

        private SimpleContainer container;

        //constructor injection
        public ShellViewModel(IEventAggregator events, TestViewModel testVM, SimpleContainer container)
        {
            this.events = events;
            this.testVM = testVM;
            this.container = container;

            ActivateItemAsync(container.GetInstance<ProgramViewModel>());
            events.Subscribe(this);
        }


        public async Task HandleAsync(TestEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(testVM);
            //programVM = container.GetInstance<ProgramViewModel>();
        }


 
    }
}
