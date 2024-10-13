using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("Component")]
    public class Component
    {
        [Key]
        public int Id { get; set; }

        public int TypeId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        [InverseProperty("Component")]
        public virtual ICollection<KitComponent>? KitComponents { get; set; }

        [ForeignKey("TypeId")]
        [InverseProperty("Components")]
        public virtual ComponentsType? Type { get; set; }
    }
}
