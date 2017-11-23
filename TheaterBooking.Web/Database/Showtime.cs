using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheaterBooking.Web.Database
{
    public class Showtime
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Showtime_ID { get; set; }
        public int Screen_ID { get; set; }
        public int Movie_ID { get; set; }
        public DateTime Show_Date { get; set; }
        public TimeSpan Show_Time { get; set; }
    
        public virtual Movie Movie { get; set; }
        public virtual Screen Screen { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
