namespace ACME.Helper
{
    using System;

    internal class Helpers
    {
        #region Public Methods

        public static bool CompareDate(DateTime data, DateTime target)
        {
            return data.Year == target.Year && data.Month == target.Month && data.Day == target.Day;
        }

        #endregion
    }
}