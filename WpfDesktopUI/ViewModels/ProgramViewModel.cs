using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.DataAccess;
using DataAccess.Library.Models;
using HelperLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WpfDesktopUI.EventModels;
using WpfDesktopUI.Models;
using WpfDesktopUI.Views.Interfaces.Composite;

namespace WpfDesktopUI.ViewModels
{
    public class ProgramViewModel : Screen, IStartView
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
                NotifyOfPropertyChange(() => CanViewSelected);
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


        public void LoadItems()
        {
            ProgramData data = new ProgramData();
            ProgramListBox = new BindingList<ProgramDisplayModel>();

            Helper.LoadItems(
                ProgramListBox, 
                data.GetAllPrograms,
                mapper.Map<List<ProgramDisplayModel>>
                );
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItems();
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

                ProgramData data = new ProgramData();
                Helper.SwapItems(ProgramListBox, SelectedProgram, -1, data.SwapProgramOrder, LoadItems);
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

                ProgramData data = new ProgramData();
                Helper.SwapItems(ProgramListBox, SelectedProgram, 1, data.SwapProgramOrder, LoadItems);
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
                data.SaveProgramRecord(NewProgramName);

                LoadItems();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    ErrorMessage = $"Program '{NewProgramName}' already exists!";
                }
                else
                {
                    throw;
                }
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

                LoadItems();
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

                if (SelectedProgram != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task ViewSelected()
        {
            try
            {
                ErrorMessage = "";
                await events.PublishOnUIThreadAsync(
                    new GoWorkoutViewEvent { ProgramId = SelectedProgram.Id, ProgramName = SelectedProgram.Name});
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
