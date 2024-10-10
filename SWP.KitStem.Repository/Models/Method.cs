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
    [Table("Method")]
    public class Method
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string NormalizedName { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Method")]
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
