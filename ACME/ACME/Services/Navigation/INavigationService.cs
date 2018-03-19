namespace ACME.Services.Navigation
{
    using System;
    using System.Threading.Tasks;

    public interface INavigationService
    {
        #region Public Methods

        void SetMainPage(Type type);

        Task NavigateTo(Type type);

        Task Back();

        #endregion
    }
}