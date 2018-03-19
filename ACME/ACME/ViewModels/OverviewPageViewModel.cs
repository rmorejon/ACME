namespace ACME.ViewModels
{
    using ACME.Enum;
    using ACME.ViewModels.Base;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class OverviewPageViewModel : ViewModelBase
    {
        #region Constructors

        #endregion

        #region Properties Public

        public RelayCommand LoadCommand => new RelayCommand(this.Load);

        #endregion

        #region Methods

        private void Load()
        {
            MainViewModel.Instance.MenuViewModel.SetMenu(ModeMenu.Dashboard);
        }

        #endregion
    }
}