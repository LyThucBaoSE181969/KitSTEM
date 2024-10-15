using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels
{
    public class KitModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Brief { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int PurchaseCost { get; set; }

        public bool Status { get; set; }
        public bool IsActive()
        {
            return Status;
        }
        public CategoryModel? Category { get; set; }

        //public List<KitComponentModel>? KitComponents { get; set; }

        //public List<KitImageModel>? KitImages { get; set; }

        //public List<LabModel>? Labs { get; set; }

        //public List<PackageModel>? Packages { get; set; }
    }

}
