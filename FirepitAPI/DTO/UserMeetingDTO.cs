using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.DTO
{
    public class UserMeetingDTO
    {
        public string PersonId { get; set; }
        public int MeetingId { get; set; }
        public bool? Going { get; set; }

        public virtual PersonDTO Person { get; set; }
        public virtual MeetingDTO Meeting { get; set; }
    }

    public class UserMeetingUpdateDTO
    {
        public string PersonId { get; set; }
        public int MeetingId { get; set; }
        public bool? Going { get; set; }
    }

}
