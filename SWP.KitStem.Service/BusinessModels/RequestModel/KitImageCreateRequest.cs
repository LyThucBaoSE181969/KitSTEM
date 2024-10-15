using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class KitImageCreateRequest
    {
        public Guid Id { get; set; }

        public int KitId { get; set; }

        [Unicode(false)]
        public string Url { get; set; }
    }
}
