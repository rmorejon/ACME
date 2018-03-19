namespace ACME.Services.Navigation
{
    using System;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    public class NavigationService : INavigationService
    {
        #region INavigationService implementation

        public void SetMainPage(Type type)
        {
            Application.Current.MainPage = new NavigationPage((dynamic)Activator.CreateInstance(type));
        }

        public async Task NavigateTo(Type type)
        {
            await Application.Current.MainPage.Navigation.PushAsync((dynamic)Activator.CreateInstance(type));
        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync(true);
        }

        #endregion
    }
}