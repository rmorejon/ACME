namespace ACME.Pages
{
    using ACME.Messages;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        #region Constructors

        public MapPage()
        {
            this.InitializeComponent();

            this.MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(1)));

            this.HandleReceivedMessages();
        }

        #endregion

        #region Methods

        private void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<MapServiceMessage>(
                this,
                "ChangePin",
                map => { this.MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(map.Position, Distance.FromMiles(1))); });
        }

        #endregion
    }
}