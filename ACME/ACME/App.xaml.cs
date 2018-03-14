namespace ACME
{
    using ACME.Pages;
    using ACME.ViewModels.Base;

    using Xamarin.Forms;

    public partial class App : Application
    {
        #region Constructors

        static App()
        {
            BuildDependencies();
        }

        public App()
        {
            InitializeComponent();

            this.MainPage = new LoginPage();
        }

        #endregion

        #region Public Methods

        public static void BuildDependencies()
        {
            Locator.Instance.Build();
        }

        #endregion

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion
    }
}