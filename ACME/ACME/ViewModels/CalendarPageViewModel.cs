namespace ACME.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using ACME.Messages;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;

    using XamForms.Controls;

    public class CalendarPageViewModel : ViewModelBase
    {
        #region Fields

        private DateTime? selectedDate;

        #endregion

        #region Constructors

        public CalendarPageViewModel()
        {
            this.LoadCommand = new RelayCommand(this.Load);
            this.SelectedDate = DateTime.Now;
            this.SpecialDates = new ObservableCollection<SpecialDate>();
        }

        #endregion

        #region Properties Public

        public RelayCommand LoadCommand { get; set; }

        public DateTime? SelectedDate
        {
            get => this.selectedDate;
            set
            {
                if (this.selectedDate == value) return;

                this.selectedDate = value;
                this.RaisePropertyChanged(nameof(this.SelectedDate));
            }
        }

        public ObservableCollection<SpecialDate> SpecialDates { get; set; }

        #endregion

        #region Methods

        private void Load()
        {
            this.SpecialDates.Clear();
            var tickets = MainViewModel.Instance.DashBoardPageViewModel.AllTickets;

            foreach (var ticket in tickets)
                this.SpecialDates.Add(
                    new SpecialDate(ticket.DateTime.Date)
                        {
                            BackgroundColor = Color.DarkCyan,
                            TextColor = Color.White,
                            BorderWidth = 4,
                            Selectable = true
                        });

            this.RaisePropertyChanged(nameof(this.SpecialDates));
            MessagingCenter.Send(new CalendarServiceMessage(), "UpdateCalendar");
        }

        #endregion
    }
}