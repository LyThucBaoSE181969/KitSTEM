using Microsoft.AspNetCore.Mvc;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class KitModelRequest
    {
        public int Page { get; set; } = 0;
        [FromQuery(Name = "kit-name")]
        public string KitName { get; set; } = "";
        [FromQuery(Name = "category-name")]
        public string CategoryName { get; set; } = "";
    }
}
