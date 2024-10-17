using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class LabModelRequest
    {
        public int Page { get; set; } = 0;
        [FromQuery(Name = "lab-name")]
        public string? LabName { get; set; }
        [FromQuery(Name = "kit-name")]
        public string? KitName { get; set; }
    }
}
