using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopUI.ViewModels
{
    public class ProgramViewModel : Screen
    {
        private BindingList<string> programListBox;

        public BindingList<string> ProgramListBox
        {
            get
            {
                return programListBox;
            }
            set
            {
                programListBox = value;
                NotifyOfPropertyChange(() => ProgramListBox);
                //TODO ListBox Logic
            }
        }


        public bool CanMoveUp()
        {
            //TODO MOVE UP CONDITION
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public void MoveUp()
        {
            //TODO MOVE UP LOGIC
            Console.WriteLine();
        }




    }
}
