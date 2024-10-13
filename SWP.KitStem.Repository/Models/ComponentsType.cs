using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("Type")]
    public class ComponentsType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;
        public bool Status { get; set; }
        [JsonIgnore]
        [InverseProperty("Type")]
        public virtual ICollection<Component>? Components { get; set; }
    }
}
