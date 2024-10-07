using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public List<KitModel> Kits { get; set; } = new List<KitModel>();

    }
}
