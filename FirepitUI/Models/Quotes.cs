using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Models
{
    public class Quotes
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string PersonId { get; set; }
        public string WhoSaidIt { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person PersonIdKey { get; set; }
        [ForeignKey("WhoSaidIt")]
        public virtual Person WhoSaidItKey { get; set; }
    }
}
