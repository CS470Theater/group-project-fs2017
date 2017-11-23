using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheaterBooking.Web.Database
{
    public class Movie
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
            Genres = new HashSet<Genre>();
        }

        [Key]
        public int Movie_ID { get; set; }
        public int Rating_ID { get; set; }
        public string Movie_Name { get; set; }
        public string Movie_Desc { get; set; }
        public string Movie_Poster_URL { get; set; }
        public DateTime Date_Start { get; set; }
        public DateTime Date_End { get; set; }
        public byte Duration_Hour { get; set; }
        public byte Duration_Minute { get; set; }
        public string Movie_Trailer_URL { get; set; }

        public virtual Rating Rating { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Showtime> Showtimes { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
