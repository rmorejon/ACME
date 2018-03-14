namespace ACME.Models
{
    using System;

    public class Ticket
    {
        #region Properties Public

        public int TicketId { get; set; }

        public User User { get; set; }

        public string Address { get; set; }

        public DateTime DateTime { get; set; }

        #endregion
    }
}