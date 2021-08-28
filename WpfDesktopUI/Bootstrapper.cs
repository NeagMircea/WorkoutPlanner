using AutoMapper;
using Caliburn.Micro;
using DataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDesktopUI.Models;
using WpfDesktopUI.ViewModels;

namespace WpfDesktopUI
{
    /// <summary>
    /// Class used to configure Caliburn Micro
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }


        //configures class asotiation
        protected override void Configure()
        {
            container.Instance(ConfigureAutomapper());
            container.Instance(container);

            container
                //from CB Micro for window manipulation
                .Singleton<IWindowManager, WindowManager>()

                //from CB Micro for event messaging 
                .Singleton<IEventAggregator, EventAggregator>();


            //configure ViewModels using reflexion
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel")).ToList()
                .ForEach(viewModelType =>
                container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));
        }


        private IMapper ConfigureAutomapper()
        {
            //maps data model to display model, uses reflection
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProgramModel, ProgramDisplayModel>();
                cfg.CreateMap<CategoryModel, CategoryDisplayModel>();
                cfg.CreateMap<WorkoutModel, WorkoutDisplayModel>();
                cfg.CreateMap<WorkoutProgramModel, WorkoutProgramDisplayModel>();
                });

            var mapper = config.CreateMapper();

            return mapper;
        }


        //set startup view to shellview
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }


        //get instance from container instead
        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }


        //get all instances from container instead
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }


        //contstruct instance using container instead
        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
