using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDesktopUI.ViewModels;

namespace WpfDesktopUI
{
    /// <summary>
    /// Class used to configure Caliburn Micro
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {

        public Bootstrapper()
        {
            Initialize();
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
