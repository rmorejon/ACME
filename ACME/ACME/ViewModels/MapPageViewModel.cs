namespace ACME.ViewModels
{
    using System.Collections.Generic;

    using ACME.Messages;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public class MapPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly Geocoder geoCoder;

        #endregion

        #region Constructors

        public MapPageViewModel()
        {
            this.geoCoder = new Geocoder();
        }

        #endregion

        #region Properties Public

        public RelayCommand LoadCommand => new RelayCommand(this.Load);

        public List<Pin> Pins { get; set; }

        #endregion

        #region Methods

        private async void Load()
        {
            var ticket = MainViewModel.Instance.DashBoardPageViewModel.SelectedTicket;
            this.Pins = new List<Pin>();

            if (ticket == null || string.IsNullOrEmpty(ticket.Address)) return;

            var approximateLocation = await this.geoCoder.GetPositionsForAddressAsync(ticket.Address);
            var flag = false;

            foreach (var p in approximateLocation)
            {
                var position = new Position(p.Latitude, p.Longitude);
                this.Pins.Add(new Pin { Position = position, Address = ticket.Address });

                if (!flag)
                {
                    MessagingCenter.Send(new MapServiceMessage(position), "ChangePin");
                    flag = true;
                }
            }

            this.RaisePropertyChanged(nameof(this.Pins));
        }

        #endregion
    }
}