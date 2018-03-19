namespace ACME.Messages
{
    using Xamarin.Forms.Maps;

    public class MapServiceMessage
    {
        #region Constructors

        public MapServiceMessage(Position position)
        {
            this.Position = position;
        }

        #endregion

        #region Properties Public

        public Position Position { get; set; }

        #endregion
    }
}