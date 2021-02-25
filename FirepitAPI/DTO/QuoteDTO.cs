using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.DTO
{
    public class QuoteDTO
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string PersonId { get; set; }
        public string WhoSaidIt { get; set; }
        public virtual PersonDTO PersonIdKey { get; set; }
        public virtual PersonDTO WhoSaidItKey { get; set; }
    }

    public class QuoteCreateDTO
    {
        public string Quote { get; set; }
        public string PersonId { get; set; }
        public string WhoSaidIt { get; set; }
    }

    public class QuoteUpdateDTO
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string PersonId { get; set; }
        public string WhoSaidIt { get; set; }
    }

}
