namespace ACME.Messages
{
    using ACME.Models;

    public class TicketServiceMessage
    {
        #region Constructors

        public TicketServiceMessage(Ticket ticket)
        {
            this.Ticket = ticket;
        }

        #endregion

        #region Properties Public

        public Ticket Ticket { get; set; }

        #endregion
    }
}