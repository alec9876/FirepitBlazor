using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.DTO
{
    public class PersonDTO 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string GoesBy { get; set; }
        public bool Attend { get; set; }
        public string Image { get; set; }

        public virtual ICollection<UserMeetingDTO> Meetings { get; set; }
        public virtual IList<BeverageDTO> Beverages { get; set; }
    }

    public class PersonUpdateDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string GoesBy { get; set; }
    }
}
