namespace ACME.ViewModels.Base
{
    using System;

    using ACME.Services.Dialog;
    using ACME.Services.Navigation;

    using GalaSoft.MvvmLight.Command;

    using Xam.Plugin;

    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Static Fields and Constants

        private static MainViewModel instance;

        #endregion

        #region Fields

        private CalendarPageViewModel calendarPageViewModel;

        private DashBoardPageViewModel dashBoardPageViewModel;

        private IDialogService dialogService;

        private MapPageViewModel mapPageViewModel;

        private MenuViewModel menuViewModel;

        private INavigationService navigationService;

        private NewTicketPageViewModel newTicketPageViewModel;

        private OverviewPageViewModel overviewPageViewModel;

        private PopupMenu Popup;

        #endregion

        #region Constructors

        public MainViewModel()
        {
            this.BackCommand = new RelayCommand(this.Back);
            this.GoCommand = new RelayCommand<string>(this.Go);
            this.LoadPopup();
        }

        #endregion

        #region Properties Public

        public static MainViewModel Instance
        {
            get
            {
                if (instance == null) instance = new MainViewModel();
                return instance;
            }
        }

        public INavigationService NavigationService
        {
            get
            {
                if (this.navigationService == null)
                    this.navigationService = Locator.Instance.Resolve(typeof(NavigationService)) as NavigationService;
                return this.navigationService;
            }
        }

        public IDialogService DialogService
        {
            get
            {
                if (this.dialogService == null)
                    this.dialogService = Locator.Instance.Resolve(typeof(DialogService)) as DialogService;
                return this.dialogService;
            }
        }

        public MenuViewModel MenuViewModel
        {
            get
            {
                if (this.menuViewModel == null)
                    this.menuViewModel = new MenuViewModel();
                return this.menuViewModel;
            }
        }

        public MapPageViewModel MapPageViewModel
        {
            get
            {
                if (this.mapPageViewModel == null)
                    this.mapPageViewModel = new MapPageViewModel();
                return this.mapPageViewModel;
            }
        }

        public CalendarPageViewModel CalendarPageViewModel
        {
            get
            {
                if (this.calendarPageViewModel == null) this.calendarPageViewModel = new CalendarPageViewModel();
                return this.calendarPageViewModel;
            }
        }

        public DashBoardPageViewModel DashBoardPageViewModel
        {
            get
            {
                if (this.dashBoardPageViewModel == null) this.dashBoardPageViewModel = new DashBoardPageViewModel();
                return this.dashBoardPageViewModel;
            }
        }

        public NewTicketPageViewModel NewTicketPageViewModel
        {
            get
            {
                if (this.newTicketPageViewModel == null) this.newTicketPageViewModel = new NewTicketPageViewModel();
                return this.newTicketPageViewModel;
            }
        }

        public OverviewPageViewModel OverviewPageViewModel
        {
            get
            {
                if (this.overviewPageViewModel == null) this.overviewPageViewModel = new OverviewPageViewModel();
                return this.overviewPageViewModel;
            }
        }

        public RelayCommand<object> ShowPopupCommand => new RelayCommand<object>(this.ShowPopup);

        public RelayCommand BackCommand { get; set; }

        public RelayCommand<string> GoCommand { get; set; }

        #endregion

        #region Methods

        private void Go(string obj)
        {
            this.NavigationService.NavigateTo(Type.GetType($"ACME.Pages.{obj}"));
        }

        private void LoadPopup()
        {
            this.Popup = new PopupMenu { BindingContext = this.MenuViewModel };
            this.Popup.SetBinding(PopupMenu.ItemsSourceProperty, "Items");
            this.Popup.OnItemSelected += this.ItemSelected;
        }

        private async void ItemSelected(string item)
        {
            if (this.DashBoardPageViewModel.SelectedTicket == null)
            {
                await this.DialogService.ShowAlertAsync("No ticket selected", "Error", "Close");
                return;
            }

            if (item == "Ticket") this.Go("TicketPage");
            else if (item == "Direction") this.Go("MapPage");
            else if (item == "Dashboard") this.Go("DashBoardPage");
        }

        private void ShowPopup(object obj)
        {
            this.Popup?.ShowPopup(obj as View);
        }

        private void Back()
        {
            this.NavigationService.Back();
        }

        #endregion
    }
}