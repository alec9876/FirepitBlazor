using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Models
{
    public class UserMeeting
    {
        public string PersonId { get; set; }
        public int MeetingId { get; set; }
        public bool? Going { get; set; }
        
        public virtual Person Person { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
