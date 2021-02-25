using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Models
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Person FromUser { get; set; }
        public virtual Person ToUser { get; set; }

    }
}
