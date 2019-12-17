using System;
using System.Windows;

using AutoMapper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Tracker.ECL;
using Tracker.ECL.DTO;
using Tracker.ECL.Interfaces;
using Tracker.Interfaces;
using Tracker.ViewModels;

using TrackerCommon;

using TrackerLib;
using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace Tracker.Infrastructure
{
    //
    // this class is the funnel through which all dependency injection flows
    //

    public class Locator
    {
        private readonly ServiceProvider _provider;
        private static bool _initialized = false;
        private static readonly object _lockObject = new object();

        #region Initializers

        private void InitializeMapper(IServiceCollection services)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientEntity, Client>().ReverseMap();
                cfg.CreateMap<ClientTypeEntity, ClientType>().ReverseMap();
                cfg.CreateMap<HoursEntity, Hours>().ReverseMap();
                cfg.CreateMap<MileageEntity, Mileage>().ReverseMap();
                cfg.CreateMap<NoteEntity, Note>().ReverseMap();
                cfg.CreateMap<PhoneEntity, Phone>().ReverseMap();
                cfg.CreateMap<PhoneTypeEntity, PhoneType>().ReverseMap();
            });
            Mapper mapper = new Mapper(config);
            services.AddSingleton<IMapper>(mapper);
        }

        private void InitializeDAL(IServiceCollection services)
        {
            services.AddTransient<IClientDAL, ClientDAL>();
            services.AddTransient<IClientTypeDAL, ClientTypeDAL>();
            services.AddTransient<IHoursDAL, HoursDAL>();
            services.AddTransient<IMileageDAL, MileageDAL>();
            services.AddTransient<INoteDAL, NoteDAL>();
            services.AddTransient<IPhoneDAL, PhoneDAL>();
            services.AddTransient<IPhoneTypeDAL, PhoneTypeDAL>();
            services.AddTransient<ISettingsDAL, SettingsDAL>();
        }

        private void InitializeECL(ServiceCollection services)
        {
            services.AddTransient<IClientECL, ClientECL>();
            services.AddTransient<IClientTypeECL, ClientTypeECL>();
            services.AddTransient<IHoursECL, HoursECL>();
            services.AddTransient<IMileageECL, MileageECL>();
            services.AddTransient<INoteECL, NoteECL>();
            services.AddTransient<IPhoneECL, PhoneECL>();
            services.AddTransient<IPhoneTypeECL, PhoneTypeECL>();
        }

        private void InitializeViewModels(ServiceCollection services)
        {
            services.AddTransient<AboutViewModel>();
            services.AddTransient<BackupViewModel>();
            services.AddTransient<ClientTypeViewModel>();
            services.AddTransient<ClientViewModel>();
            services.AddTransient<DatePickerViewModel>();
            services.AddTransient<ExplorerViewModel>();
            services.AddTransient<HoursViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<MileageViewModel>();
            services.AddTransient<NoteViewModel>();
            services.AddTransient<PalletteViewModel>();
            services.AddTransient<PasswordViewModel>();
            services.AddTransient<PhoneTypeViewModel>();
            services.AddTransient<PhoneViewModel>();
            services.AddTransient<PopupViewModel>();
            services.AddTransient<QAViewModel>();
            services.AddTransient<ReportsViewModel>();
            services.AddSingleton<StatusbarViewModel>();
        }

        #endregion

        #region Properties 

        public IConfiguration Configuration { get => _provider.GetRequiredService<IConfiguration>(); }
        public IExplorerService ExplorerService { get => _provider.GetRequiredService<IExplorerService>(); }
        public IPasswordManager PasswordManager { get => _provider.GetRequiredService<IPasswordManager>(); }
        public IServiceProvider Provider { get => _provider; }
        public ISettingsService Settings { get => _provider.GetRequiredService<ISettingsService>(); }
        public Context Context { get => _provider.GetRequiredService<Context>(); }

        #region ViewModels

        public AboutViewModel AboutViewModel { get => _provider.GetRequiredService<AboutViewModel>(); }
        public BackupViewModel BackupViewModel { get => _provider.GetRequiredService<BackupViewModel>(); }
        public ClientTypeViewModel ClientTypeViewModel { get => _provider.GetRequiredService<ClientTypeViewModel>(); }
        public ClientViewModel ClientViewModel { get => _provider.GetRequiredService<ClientViewModel>(); }
        public DatePickerViewModel DatePickerViewModel { get => _provider.GetRequiredService<DatePickerViewModel>(); }
        public ExplorerViewModel ExplorerViewModel { get => _provider.GetRequiredService<ExplorerViewModel>(); }
        public HoursViewModel HoursViewModel { get => _provider.GetRequiredService<HoursViewModel>(); }
        public MainViewModel MainViewModel { get => _provider.GetRequiredService<MainViewModel>(); }
        public MileageViewModel MileageViewModel { get => _provider.GetRequiredService<MileageViewModel>(); }
        public NoteViewModel NoteViewModel { get => _provider.GetRequiredService<NoteViewModel>(); }
        public PalletteViewModel PalletteViewModel { get => _provider.GetRequiredService<PalletteViewModel>(); }
        public PasswordViewModel PasswordViewModel { get => _provider.GetRequiredService<PasswordViewModel>(); }
        public PhoneTypeViewModel PhoneTypeViewModel { get => _provider.GetRequiredService<PhoneTypeViewModel>(); }
        public PhoneViewModel PhoneViewModel { get => _provider.GetRequiredService<PhoneViewModel>(); }
        public PopupViewModel PopupViewModel { get => _provider.GetRequiredService<PopupViewModel>(); }
        public QAViewModel QAViewModel { get => _provider.GetRequiredService<QAViewModel>(); }
        public ReportsViewModel ReportsViewModel { get => _provider.GetRequiredService<ReportsViewModel>(); }
        public StatusbarViewModel StatusbarViewModel { get => _provider.GetRequiredService<StatusbarViewModel>(); }

        #endregion

        #region DALs (Data Access Layer classes, interface to EF and the database, produces Entity objects)

        public IClientDAL ClientDAL { get => _provider.GetRequiredService<IClientDAL>(); }
        public IClientTypeDAL ClientTypeDAL { get => _provider.GetRequiredService<IClientTypeDAL>(); }
        public IHoursDAL HoursDAL { get => _provider.GetRequiredService<IHoursDAL>(); }
        public IMileageDAL MileageDAL { get => _provider.GetRequiredService<IMileageDAL>(); }
        public INoteDAL NoteDAL { get => _provider.GetRequiredService<INoteDAL>(); }
        public IPhoneDAL PhoneDAL { get => _provider.GetRequiredService<IPhoneDAL>(); }
        public IPhoneTypeDAL PhoneTypeDAL { get => _provider.GetRequiredService<IPhoneTypeDAL>(); }
        public ISettingsDAL SettingsDAL { get => _provider.GetRequiredService<ISettingsDAL>(); }

        #endregion

        #region ECLs (Entity Conversion Layer classes, consumes Entity objects and produces observable DTO and Model objects)

        public IClientECL ClientECL { get => _provider.GetRequiredService<IClientECL>(); }
        public IClientTypeECL ClientTypeECL { get => _provider.GetRequiredService<IClientTypeECL>(); }
        public IHoursECL HoursECL { get => _provider.GetRequiredService<IHoursECL>(); }
        public IMileageECL MileageECL { get => _provider.GetRequiredService<IMileageECL>(); }
        public INoteECL NoteECL { get => _provider.GetRequiredService<INoteECL>(); }
        public IPhoneECL PhoneECL { get => _provider.GetRequiredService<IPhoneECL>(); }
        public IPhoneTypeECL PhoneTypeECL { get => _provider.GetRequiredService<IPhoneTypeECL>(); }

        #endregion

        #endregion

        public Locator()
        {
            lock(_lockObject)
            {
                if (!_initialized)
                {
                    ServiceCollection services = new ServiceCollection();

                    services.AddDbContext<Context>(ServiceLifetime.Transient);

                    services.AddSingleton(ConfigurationFactory.Create());
                    services.AddTransient<IExplorerService, ExplorerService>();
                    services.AddSingleton<IPasswordManager, PasswordManager>();
                    services.AddTransient<IStringCypherService, StringCypherService>();
                    services.AddSingleton<ISettingsService, SettingsService>();

                    InitializeMapper(services);
                    InitializeDAL(services);
                    InitializeECL(services);
                    InitializeViewModels(services);

                    _provider = services.BuildServiceProvider();

                    Application.Current.Resources[Constants.Locator] = this;

                    _initialized = true;
                }
            }
        }
    }
}
