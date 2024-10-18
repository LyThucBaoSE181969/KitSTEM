using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class LevelCreateRequest
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
