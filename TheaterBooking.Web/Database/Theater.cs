using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheaterBooking.Web.Database
{
    public class Theater
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Theater()
        {
            Screens = new HashSet<Screen>();
        }

        [Key]
        public int Theater_ID { get; set; }
        public string Theater_Name { get; set; }
        public string Theater_Owner { get; set; }
        public string Theater_Address { get; set; }
        public string Theater_Phone { get; set; }
    
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Screen> Screens { get; set; }
    }
}
