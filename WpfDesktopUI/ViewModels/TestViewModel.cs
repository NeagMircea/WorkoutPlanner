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
        private int programId;                      
        public int ProgramId
        {
            get
            {
                return programId;
            }
            set
            {
                programId = value;
            }
        }

        private string programName;
        public string ProgramName
        {
            get
            {
                return programName;
            }
            set
            {
                programName = value;
            }
        }

        private string textTitle;
        public string TextTitle
        {
            get
            {
                return textTitle;
            }
            set
            {
                textTitle = value;
                NotifyOfPropertyChange(() => TextTitle);
            }
        }


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


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            TextTitle = ProgramName;
        }
    }
}
