using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.ResponseModel
{
    public class LabModelResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Author { get; set; }
        public bool Status { get; set; }
        public virtual Kit? Kit { get; set; }
        public virtual Level? Level { get; set; }
    }
}
