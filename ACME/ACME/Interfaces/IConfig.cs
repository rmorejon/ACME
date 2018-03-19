namespace ACME.Interfaces
{
    using SQLite.Net.Interop;

    public interface IConfig
    {
        #region Properties Public

        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }

        #endregion
    }
}