using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Models
{
    [Table("Meetings")]
    public partial class Meeting
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }

        public virtual ICollection<UserMeeting> UserMeetings { get; set; }
        public virtual IList<Beverages> Beverages { get; set; }
    }
}
