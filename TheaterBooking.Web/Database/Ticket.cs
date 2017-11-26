using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterBooking.Web.Database
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ticket_ID { get; set; }
        public int Booking_ID { get; set; }
        public int Showtime_ID { get; set; }
        public int Price_ID { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual Price Price { get; set; }
        public virtual Showtime Showtime { get; set; }
    }
}
