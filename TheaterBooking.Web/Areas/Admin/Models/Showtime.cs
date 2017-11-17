//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheaterBooking.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Showtime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Showtime()
        {
            this.Tickets = new HashSet<Ticket>();
        }
    
        public int Showtime_ID { get; set; }
        public int Screen_ID { get; set; }
        public int Movie_ID { get; set; }
        public System.DateTime Show_Date { get; set; }
        public System.TimeSpan Show_Time { get; set; }
    
        public virtual Movie Movie { get; set; }
        public virtual Screen Screen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
