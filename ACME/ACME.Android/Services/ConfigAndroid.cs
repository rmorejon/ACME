using ACME.Droid.Services;

using Xamarin.Forms;

[assembly: Dependency(typeof(ConfigAndroid))]

namespace ACME.Droid.Services
{
    using System;

    using ACME.Interfaces;

    using SQLite.Net.Interop;
    using SQLite.Net.Platform.XamarinAndroid;

    public class ConfigAndroid : IConfig
    {
        #region IConfig Members

        /// <summary>
        ///     The directory db.
        /// </summary>
        public string DirectoryDB => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        /// <summary>
        ///     Gets the platform.
        /// </summary>
        public ISQLitePlatform Platform => new SQLitePlatformAndroid();

        #endregion
    }
}