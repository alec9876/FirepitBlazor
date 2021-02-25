using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Models
{
    [Table("Beverages")]
    public partial class Beverages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }

        public string PersonId { get; set; }
        public virtual Person Person { get; set; }

        public int? MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
    }

    public enum BeverageTypes
    {
        Beer, Bourbon, Gin, Scotch, Soda, Vodka, Wine, Other
    }

}
