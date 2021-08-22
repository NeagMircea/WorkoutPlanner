using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.ViewModels
{
    public class TestViewModel : Screen
    {

        public bool CanTestButton()
        {
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public void TestButton()
        {

        }
    }
}
