using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels
{
    public class CartModel
    {
        public string UserId { get; set; } = null!;

        public int PackageId { get; set; }

        public int PackageQuantity { get; set; }

        public virtual Package Package { get; set; } = null!;

        public virtual AspNetUser User { get; set; } = null!;
    }
}
