using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Web.Database
{
    public class Rating
    {
        [Key]
        public int Rating_ID { get; set; }

        public string Rating_Symbol { get; set; }
    }
}
