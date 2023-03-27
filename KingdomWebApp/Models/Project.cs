using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomWebApp.Models
{
  public class Project
  {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public DateTime? StartTime { get; set; }
    public int? InitialCost { get; set; }
    //public string? Website { get; set; }
    //public string? Twitter { get; set; }
    //public string? Facebook { get; set; }
    public string? Contact { get; set; }
    [ForeignKey("Address")]
    public int? AddressId { get; set; }
    public Address Address { get; set; }
    public ProjectCategory ProjectCategory { get; set; }
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public List<Tool> Tools { get; set; } = new List<Tool>();
  }

  
}
