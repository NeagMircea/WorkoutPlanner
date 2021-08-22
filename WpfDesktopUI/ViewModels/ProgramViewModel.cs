using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Models;

namespace WpfDesktopUI.ViewModels
{
    public class ProgramViewModel : Screen
    {
        private BindingList<ProgramModel> programListBox;
        public BindingList<ProgramModel> ProgramListBox
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
                NotifyOfPropertyChange(() => ProgramName);   
            }
        }

        private string newProgramName;
        public string NewProgramName
        {
            get
            {
                return newProgramName;
            }
            set
            {
                newProgramName = value;
                NotifyOfPropertyChange(() => NewProgramName);
            }
        }


        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        private IEventAggregator events;


        public ProgramViewModel(IEventAggregator events)
        {
            this.events = events;
        }


        private void LoadPrograms()
        {
            //TODO CHANGE PROGRAM MODEL TO PROGRAM DISPLAY MODEL
            ProgramData data = new ProgramData();
            List<ProgramModel> programs = data.GetAllPrograms();
            ProgramListBox = new BindingList<ProgramModel>(programs);
        }


        private void LoadCategories()
        {
            //TODO CHANGE CATEGORY MODEL TO CATEGORY DISPLAY MODEL
            CategoryData data = new CategoryData();
            List<CategoryModel> categories = data.GetAllCategories();
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadPrograms();
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
            try
            {
                ErrorMessage = "";

                var x = 2;
                x = x / 0;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanMoveDown()
        {
            //TODO MOVE DOWN CONDITION
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public void MoveDown()
        {
            //TODO MOVE DOWN LOGIC
            try
            {
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanAddNew()
        {
            //TODO ADD NEW CONDITION
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public async Task AddNew()
        {
            //TODO ADD NEW LOGIC
            try
            {
                ErrorMessage = "";
                await events.PublishOnUIThreadAsync(new TestEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanViewSelected()
        {
            //TODO VIEW SELECTED CONDITION
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public void ViewSelected()
        {
            //TODO VIEW SELECTED LOGIC
            try
            {
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanRemoveSelected()
        {
            //TODO REMOVE SELECTED CONDITION
            bool output = false;

            if (true)
            {
                output = true;
            }

            return output;
        }


        public void RemoveSelected()
        {
            //TODO REMOVE SELECTED LOGIC
            try
            {
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
