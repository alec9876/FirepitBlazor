using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Models
{
    [Table("Meetings")]
    public partial class Meeting
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm tt}")]
        public DateTime Time { get; set; }
        public string Place { get; set; }

        public virtual ICollection<UserMeeting> UserMeetings { get; set; }
        public virtual IList<Beverages> Beverages { get; set; }
    }
}
