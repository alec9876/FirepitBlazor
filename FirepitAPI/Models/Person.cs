using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FirepitAPI.Models
{
    public partial class Person : IdentityUser
    {
        public Person()
        {
            ChatHistoryFromUser = new HashSet<ChatHistory>();
            ChatHistoryToUser = new HashSet<ChatHistory>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string GoesBy { get; set; }
        public bool Attend { get; set; }
        public string  Image { get; set; }

        public virtual ICollection<ChatHistory> ChatHistoryFromUser { get; set; }
        public virtual ICollection<ChatHistory> ChatHistoryToUser { get; set; }
        public virtual ICollection<UserMeeting> UserMeetings { get; set; }
        public IList<Beverages> Beverages { get; set; }
        public IList<Quotes> WhoSaidItQuotes { get; set; }
        public IList<Quotes> PersonQuotes { get; set; }

    }
}
