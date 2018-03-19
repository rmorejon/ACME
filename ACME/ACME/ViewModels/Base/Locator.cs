namespace ACME.ViewModels.Base
{
    using System;

    using ACME.Services.Data;
    using ACME.Services.Dialog;
    using ACME.Services.Navigation;

    using Autofac;

    public class Locator
    {
        #region Fields

        private readonly ContainerBuilder containerBuilder;

        private IContainer container;

        #endregion

        #region Constructors

        public Locator()
        {
            this.containerBuilder = new ContainerBuilder();
            this.containerBuilder.RegisterType<DialogService>().SingleInstance();
            this.containerBuilder.RegisterType<NavigationService>().SingleInstance();
            this.containerBuilder.RegisterType<DataService>().SingleInstance();
            this.Main = MainViewModel.Instance;
        }

        #endregion

        #region Properties Public

        public static Locator Instance { get; } = new Locator();

        public MainViewModel Main { get; set; }

        #endregion

        #region Public Methods

        public T Resolve<T>()
        {
            return this.container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return this.container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>()
            where T : class
        {
            this.containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            this.container = this.containerBuilder.Build();
        }

        #endregion
    }
}