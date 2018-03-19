namespace ACME.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ACME.Enum;

    using SQLite.Net.Attributes;

    [Table("Ticket")]
    public class Ticket : INotifyPropertyChanged
    {
        #region Fields

        private DateTime dateTime;

        private TimeSpan time;

        #endregion

        #region Properties Public

        [PrimaryKey]
        [AutoIncrement]
        [Unique]
        [NotNull]
        public int TicketId { get; set; }

        public string Customer { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Reason { get; set; }

        public string Note { get; set; }

        public Departament Departament { get; set; }

        public ServiceType ServiceType { get; set; }

        public DateTime DateTime
        {
            get => this.dateTime;
            set
            {
                if (this.dateTime == value)
                    return;

                this.dateTime = value;
                this.OnPropertyChanged(nameof(this.DateTime));
                this.OnPropertyChanged(nameof(this.FullDateTime));
                this.OnPropertyChanged(nameof(this.FullTime));
            }
        }

        public TimeSpan Time
        {
            get => this.time;
            set
            {
                if (this.time == value)
                    return;

                this.time = value;
                this.OnPropertyChanged(nameof(this.Time));
                this.OnPropertyChanged(nameof(this.FullDateTime));
                this.OnPropertyChanged(nameof(this.FullTime));
            }
        }

        [Ignore]
        public string FullDateTime => $"{this.DateTime:dddd, MMM d, yyyy} {this.FullTime}";

        [Ignore]
        public string FullTime
        {
            get
            {
                var dateTimeTemp = new DateTime(this.Time.Ticks);
                return $"{dateTimeTemp:hh:mm tt}";
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}