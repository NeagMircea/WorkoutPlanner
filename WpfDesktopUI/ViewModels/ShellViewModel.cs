using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private ProgramViewModel programVM;

        //constructor injection
        public ShellViewModel(ProgramViewModel programVM)
        {
            this.programVM = programVM;
            ActivateItemAsync(programVM);
        }
    }
}
