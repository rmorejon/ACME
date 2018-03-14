namespace ACME.ViewModels.Base
{
    using System;

    using ACME.Services.Dialog;
    using ACME.Services.Navigation;

    using Autofac;

    public class Locator
    {
        #region Fields

        private IContainer container;

        private readonly ContainerBuilder containerBuilder;

        #endregion

        #region Constructors

        public Locator()
        {
            this.containerBuilder = new ContainerBuilder();
            //this.containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            this.containerBuilder.RegisterType<DialogService>().SingleInstance();
            this.containerBuilder.RegisterType<NavigationService>().SingleInstance();
        }

        #endregion

        #region Properties Public

        public static Locator Instance { get; } = new Locator();

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