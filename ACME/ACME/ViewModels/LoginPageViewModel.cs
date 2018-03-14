namespace ACME.ViewModels
{
    using System;

    using ACME.Services.Dialog;
    using ACME.Services.Navigation;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;

    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        private bool isRunning;

        private string password;

        private string user;

        private readonly IDialogService dialogService;

        private readonly INavigationService navigationService;

        #endregion

        #region Constructors

        public LoginPageViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.LoginCommand = new RelayCommand(this.Login, this.CanLogin);
            this.IsRunning = false;
        }

        public LoginPageViewModel()
        {
            this.LoginCommand = new RelayCommand(this.Login, this.CanLogin);
            this.IsRunning = false;
            this.dialogService = Locator.Instance.Resolve(typeof(DialogService)) as DialogService;
            this.navigationService = Locator.Instance.Resolve(typeof(NavigationService)) as NavigationService;
        }

        #endregion

        #region Properties Public

        public string User
        {
            get => this.user;
            set
            {
                if (this.user == value) return;
                this.user = value;
                this.RaisePropertyChanged(nameof(this.User));
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                if (this.password == value) return;
                this.password = value;
                this.RaisePropertyChanged(nameof(this.Password));
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set
            {
                if (this.isRunning == value) return;
                this.isRunning = value;
                this.RaisePropertyChanged(nameof(this.IsRunning));
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        #endregion

        #region Methods

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(this.Password) && !string.IsNullOrEmpty(this.User) && !this.IsRunning;
        }

        private async void Login()
        {
            if (Application.Current.Resources["User"].ToString() == this.User
                && Application.Current.Resources["Password"].ToString() == this.User)
            {
                this.navigationService.SetMainPage(Type.GetType("ACME.Pages.DashBoardPage"));
            }
            else
            {
                await this.dialogService.ShowAlertAsync("Credenciales inválidas", "Error", "Close");
            }
        }

        #endregion
    }
}