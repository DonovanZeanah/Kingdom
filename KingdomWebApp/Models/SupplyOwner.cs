using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdom.Models;
using KingdomWebApp.Models;

namespace Kingdom.Models
{
    public class SupplyOwner
    {
        public int Id { get; set; }
        public int SupplyOwnerId { get; set; }
        public int OwnerId { get; set; }
        public int SupplyId { get; set; }

        [NotMapped]
        public Supply? Supply { get; set; }
        [NotMapped]

        public Owner? Owner { get; set; }
        [NotMapped]

        public ICollection<Supply>? Supplies { get; set; }
        [NotMapped]

        public ICollection<Owner>? Owners { get; set; }


    }
}
