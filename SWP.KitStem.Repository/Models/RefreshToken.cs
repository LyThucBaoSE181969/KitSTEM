using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime ExpirationTime { get; set; }
    }
}
