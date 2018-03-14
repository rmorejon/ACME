namespace ACME.ViewModels
{
    using System;
    using System.Collections.ObjectModel;

    using ACME.Models;

    using GalaSoft.MvvmLight;

    public class DashBoardPageViewModel : ViewModelBase
    {
        public ObservableCollection<Ticket> Tickets { get; set; }

        public DashBoardPageViewModel()
        {
            this.Tickets = new ObservableCollection<Ticket>();
            this.Tickets.Add(new Ticket()
                                 {
                                     User = new User()
                                                {
                                                    Id = "1",
                                                    LastName = "moreon",
                                                    Name = "Rolando",
                                                    Password = "12",
                                                    Phone = "124423"
                                                },
                                     DateTime = DateTime.Now,
                                     Address = "Lisa",
                                     TicketId = 1280
                                 });

            this.Tickets.Add(new Ticket()
                                 {
                                     User = new User()
                                                {
                                                    Id = "2",
                                                    LastName = "moreon",
                                                    Name = "Pepe",
                                                    Password = "12",
                                                    Phone = "2131444"
                                                },
                                     DateTime = DateTime.Now,
                                     Address = "Marianao",
                                     TicketId = 1286
                                 });
        }
    }
}