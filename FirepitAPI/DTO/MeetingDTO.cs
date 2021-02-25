using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.DTO
{
    public class MeetingDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }

        public virtual ICollection<UserMeetingDTO> UserMeetings { get; set; }
        public virtual IList<BeverageDTO> Beverages { get; set; }
    }

    public class MeetingCreateDTO
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm tt}")]
        public DateTime Time { get; set; }

        public string Place { get; set; }
    }

    public class MeetingUpdateDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm tt}")]
        public DateTime Time { get; set; }

        public string Place { get; set; }
    }

}
