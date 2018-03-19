namespace ACME.ViewModels
{
    using System.Collections.ObjectModel;

    using ACME.Enum;
    using ACME.Messages;
    using ACME.Models;
    using ACME.Services.Data;
    using ACME.Services.Dialog;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;

    public class NewTicketPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly DataService dataService;

        private readonly IDialogService dialogService;

        private Ticket ticket;

        #endregion

        #region Constructors

        public NewTicketPageViewModel()
        {
            this.AddCommand = new RelayCommand(this.Add);
            this.Ticket = new Ticket();
            this.dialogService = Locator.Instance.Resolve(typeof(DialogService)) as DialogService;
            this.dataService = Locator.Instance.Resolve(typeof(DataService)) as DataService;
            this.LoadItemSource();
        }

        #endregion

        #region Properties Public

        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<ServiceType> ServiceTypes { get; set; }

        public ObservableCollection<Departament> Departaments { get; set; }

        public Ticket Ticket
        {
            get => this.ticket;
            set
            {
                if (this.ticket == value)
                    return;

                this.ticket = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void LoadItemSource()
        {
            this.ServiceTypes = new ObservableCollection<ServiceType> { ServiceType.CallType, ServiceType.Another };

            this.Departaments = new ObservableCollection<Departament> { Departament.Plumbing, Departament.Another };
        }

        private async void Add()
        {
            if (string.IsNullOrEmpty(this.Ticket.Customer))
            {
                await this.dialogService.ShowAlertAsync("The field customer is requerid", "Error", "Close");
                return;
            }

            this.dataService.Insert(this.Ticket);
            await this.dialogService.ShowAlertAsync("The ticket is created sucefully", "Confirmation", "Close");

            MessagingCenter.Send(new TicketServiceMessage(this.Ticket), "AddTicket");
            this.Ticket = new Ticket();
        }

        #endregion
    }
}