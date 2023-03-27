using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomWebApp.Models
{
    public class SupplyCategory
    {
        public int Id { get; set; }
        public int SupplyCategoryId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Supply Supply { get; set; }
        public Category Category { get; set; }
        public int SupplyId { get; set; }
        public ICollection<Supply>? Supplies { get; set; }
        //public object Supply { get; internal set; }
    }
}
