using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Models
{
    [Table("People")]
    public partial class Person
    {

        public string Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Biography")]
        [StringLength(250)]
        public string Bio { get; set; }
        public string GoesBy { get; set; }
        public string  Image { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<UserMeeting> UserMeetings { get; set; }
        public IList<Beverages> Beverages { get; set; }

    }
}
