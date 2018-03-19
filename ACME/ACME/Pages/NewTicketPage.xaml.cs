namespace ACME.Pages
{
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTicketPage : ContentPage
    {
        #region Constructors

        public NewTicketPage()
        {
            this.InitializeComponent();

            this.datePicker.Date = DateTime.Now;
            this.datePicker.MaximumDate = DateTime.Now;
            this.datePicker.MinimumDate = new DateTime(2010, 1, 1);
        }

        #endregion
    }
}