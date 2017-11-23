using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheaterBooking.Web.Database
{
    public class Booking
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            Tickets = new HashSet<Ticket>();
        }
    
        [Key]
        public int Booking_ID { get; set; }
        public int Customer_ID { get; set; }
        public DateTime Booking_Date { get; set; }

        // public virtual User Customer { get; set; } (use TheaterBooking.Web.Users)
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
