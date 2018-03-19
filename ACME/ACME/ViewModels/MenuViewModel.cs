namespace ACME.ViewModels
{
    using System.Collections.Generic;

    using ACME.Enum;

    using GalaSoft.MvvmLight;

    public class MenuViewModel : ViewModelBase
    {
        #region Fields

        private ModeMenu modeMenu;

        #endregion

        #region Constructors

        public MenuViewModel()
        {
            this.Items = new List<string>();
        }

        #endregion

        #region Properties Public

        public IList<string> Items { get; set; }

        #endregion

        #region Public Methods

        public void SetMenu(ModeMenu modeMenu)
        {
            if (this.modeMenu == modeMenu) return;
            this.modeMenu = modeMenu;

            this.Items = new List<string>();
            if (this.modeMenu != ModeMenu.None)
            {
                this.Items.Add(modeMenu.ToString());
                this.Items.Add("Direction");
            }

            this.RaisePropertyChanged(nameof(this.Items));
        }

        #endregion
    }
}