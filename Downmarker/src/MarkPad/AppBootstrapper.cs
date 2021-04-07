using System.Linq;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using MarkPad.Shell;
using MarkPad.Framework;
using System;
using System.Collections.Generic;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace MarkPad
{
    class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private IContainer _Container;

        private void SetupLogging()
        {
            var config = new LoggingConfiguration();

            var debuggerTarget = new DebuggerTarget
            {
                Layout = "[${level:uppercase=true}] (${logger}) ${message}"
            };

            config.AddTarget("debugger", debuggerTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debuggerTarget));

            NLog.LogManager.Configuration = config;
        }

        protected override void Configure()
        {
            SetupLogging();

            Caliburn.Micro.LogManager.GetLog = t => new NLogAdapter(t);

            var builder = new ContainerBuilder();

            SetupCaliburnMicroDefaults(builder);

            builder.RegisterModule<EventAggregationAutoSubscriptionModule>();
            builder.RegisterModule<MarkPad.Services.ServicesModule>();

            _Container = builder.Build();
        }

        protected override void PrepareApplication()
        {
            Application.Startup += OnStartup;

#if (!DEBUG)
            Application.DispatcherUnhandledException += OnUnhandledException;
#endif

            Application.Exit += OnExit;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
        }

        #region Autofac Stuff

        private static void SetupCaliburnMicroDefaults(ContainerBuilder builder)
        {
            // register view models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                // must be a type with a name that ends with ViewModel
                .Where(type => type.Name.EndsWith("ViewModel"))
                // registered as self
                .AsSelf()
                // always create a new one
                .InstancePerDependency();

            //Register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                // must be a type with a name that ends with View
                .Where(type => type.Name.EndsWith("View"))
                // registered as self
                .AsSelf()
                // always create a new one
                .InstancePerDependency();

            // register the single window manager for this container
            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            // register the single event aggregator for this container
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrEmpty(key))
            {
                if (_Container.TryResolve(service, out instance))
                    return instance;
            }
            else
            {
                if (_Container.TryResolveNamed(key, service, out instance))
                    return instance;
            }

            throw new Exception($"Could not locate any instances of contract {key ?? service.Name}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _Container.InjectProperties(instance);
        }

        #endregion
    }
}