using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private BindingList<ProgramDisplayModel> programListBox;
        public BindingList<ProgramDisplayModel> ProgramListBox
        {
            get
            {
                return programListBox;
            }
            set
            {
                programListBox = value;
                NotifyOfPropertyChange(() => ProgramListBox);
            }
        }

        private ProgramDisplayModel selectedProgram;
        public ProgramDisplayModel SelectedProgram
        {
            get
            {
                return selectedProgram;
            }
            set
            {
                selectedProgram = value;
                NotifyOfPropertyChange(() => SelectedProgram);
                NotifyOfPropertyChange(() => CanMoveUp);
                NotifyOfPropertyChange(() => CanMoveDown);
                NotifyOfPropertyChange(() => CanRemoveSelected);
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
                NotifyOfPropertyChange(() => CanAddNew);
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
        private IMapper mapper;

        public ProgramViewModel(IEventAggregator events, IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }


        private void LoadPrograms()
        {
            ProgramData data = new ProgramData();
            List<ProgramModel> programList = data.GetAllPrograms();

            var programs = mapper.Map<List<ProgramDisplayModel>>(programList); 

            ProgramListBox = new BindingList<ProgramDisplayModel>(programs);
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadPrograms();
        }


        public bool CanMoveUp
        {
            get
            {
                bool output = false;

                if (SelectedProgram != null && ProgramListBox != null 
                    && ProgramListBox.IndexOf(SelectedProgram) > 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public void MoveUp()
        {
            try
            {
                ErrorMessage = "";

                int index1 = ProgramListBox.IndexOf(SelectedProgram);
                int index2 = index1 - 1;

                Helper.Swap(ProgramListBox, index1, index2);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanMoveDown
        {
            get
            {
                bool output = false;

                if (SelectedProgram != null && ProgramListBox != null
                    && ProgramListBox.IndexOf(SelectedProgram) < ProgramListBox.Count - 1)
                {
                    output = true;
                }

                return output;
            }
        }


        public void MoveDown()
        {
            try
            {
                ErrorMessage = "";

                int index1 = ProgramListBox.IndexOf(SelectedProgram);
                int index2 = index1 + 1;

                Helper.Swap(ProgramListBox, index1, index2);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanAddNew
        {
            get
            {
                bool output = false;

                if (NewProgramName?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public void AddNew()
        {
            try
            {
                ErrorMessage = "";

                ProgramData data = new ProgramData();
                data.SaveProgramRecord(new ProgramModel { Name = NewProgramName });

                LoadPrograms();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanRemoveSelected
        {
            get
            {
                bool output = false;

                if (SelectedProgram != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public void RemoveSelected()
        {
            try
            {
                ErrorMessage = "";

                ProgramData data = new ProgramData();
                data.DeleteProgramRecord(SelectedProgram.Id);

                LoadPrograms();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        public bool CanViewSelected
        {
            get
            {
                bool output = false;

                if (true)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task ViewSelected()
        {
            //TODO VIEW SELECTED LOGIC
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
    }
}
