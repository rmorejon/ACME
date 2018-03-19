namespace ACME.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using ACME.Enum;
    using ACME.Helper;
    using ACME.Messages;
    using ACME.Models;
    using ACME.Services.Data;
    using ACME.Services.Navigation;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;

    public class DashBoardPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly DataService dataService;

        private readonly INavigationService navigationService;

        private bool isData;

        private bool isRefreshing;

        #endregion

        #region Constructors

        public DashBoardPageViewModel()
        {
            this.Tickets = new ObservableCollection<Ticket>();
            this.AllTickets = new ObservableCollection<Ticket>();
            this.EditCommand = new RelayCommand<object>(this.Edit);
            this.navigationService = Locator.Instance.Resolve(typeof(NavigationService)) as NavigationService;
            this.dataService = Locator.Instance.Resolve(typeof(DataService)) as DataService;
            this.HandleReceivedMessages();
        }

        #endregion

        #region Properties Public

        public ObservableCollection<Ticket> Tickets { get; set; }

        public ObservableCollection<Ticket> AllTickets { get; set; }

        public Ticket SelectedTicket { get; set; }

        public RelayCommand<object> EditCommand { get; set; }

        public RelayCommand RefreshCommand => new RelayCommand(this.LoadTickets);

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set
            {
                if (this.isRefreshing == value) return;
                this.isRefreshing = value;
                this.RaisePropertyChanged(nameof(this.IsRefreshing));
            }
        }

        public bool IsData
        {
            get => this.isData;
            set
            {
                this.isData = value;
                this.RaisePropertyChanged(nameof(this.IsData));
            }
        }

        #endregion

        #region Methods

        private void Edit(object obj)
        {
            if (obj is Ticket ticket)
                this.SelectedTicket = ticket;
            this.navigationService.NavigateTo(Type.GetType($"ACME.Pages.TicketPage"));
        }

        private void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<TicketServiceMessage>(
                this,
                "AddTicket",
                ticket => { this.AllTickets.Add(ticket.Ticket); });
        }

        private void LoadTickets()
        {
            this.IsRefreshing = true;
            MainViewModel.Instance.MenuViewModel.SetMenu(ModeMenu.Ticket);
            if (this.AllTickets.Count == 0) this.LoadAllTickets();
            var selectedDate = MainViewModel.Instance.CalendarPageViewModel.SelectedDate;
            var data = new ObservableCollection<Ticket>();
            if (selectedDate != null)
                foreach (var form in this.AllTickets)
                    if (Helpers.CompareDate(selectedDate.Value, form.DateTime))
                        data.Add(form);

            this.Tickets = data;
            this.IsData = this.Tickets.Count > 0;
            this.RaisePropertyChanged(nameof(this.Tickets));
            this.IsRefreshing = false;
        }

        private void LoadAllTickets()
        {
            foreach (var form in this.dataService.Get<Ticket>()) this.AllTickets.Add(form);
        }

        #endregion
    }
}