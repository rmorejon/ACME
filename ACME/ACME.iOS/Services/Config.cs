using ACME.iOS.Services;

using Xamarin.Forms;

[assembly: Dependency(typeof(Config))]

namespace ACME.iOS.Services
{
    using System;
    using System.IO;

    using ACME.Interfaces;

    using SQLite.Net.Interop;
    using SQLite.Net.Platform.XamarinIOS;

    public class Config : IConfig
    {
        #region Fields

        private string directoryDB;

        private ISQLitePlatform platform;

        #endregion

        #region IConfig Members

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(this.directoryDB))
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    this.directoryDB = Path.Combine(directory, "..", "Library");
                }

                return this.directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (this.platform == null) this.platform = new SQLitePlatformIOS();
                return this.platform;
            }
        }

        #endregion
    }
}