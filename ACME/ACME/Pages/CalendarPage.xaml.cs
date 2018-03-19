namespace ACME.Pages
{
    using System;

    using ACME.Messages;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        #region Constructors

        public CalendarPage()
        {
            this.InitializeComponent();

            this.calendar.StartDate = DateTime.Now;
            this.calendar.MinDate = new DateTime(2010, 1, 1);
            this.calendar.MaxDate = DateTime.Now;
            this.calendar.RaiseSpecialDatesChanged();
            this.HandleReceivedMessages();
        }

        #endregion

        #region Methods

        private void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<CalendarServiceMessage>(
                this,
                "UpdateCalendar",
                obj => { this.calendar.RaiseSpecialDatesChanged(); });
        }

        #endregion
    }
}