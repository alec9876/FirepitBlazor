using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.DTO
{
    public class BeverageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }

        public string PersonId { get; set; }
        public virtual PersonDTO Person { get; set; }
        public int MeetingId { get; set; }
        public virtual MeetingDTO Meeting { get; set; }
    }

    public class BeverageCreateDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }

        public string PersonId { get; set; }
        public int? MeetingId { get; set; }
    }

    public class BeverageUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }

        public string PersonId { get; set; }
        public int MeetingId { get; set; }
    }
}
