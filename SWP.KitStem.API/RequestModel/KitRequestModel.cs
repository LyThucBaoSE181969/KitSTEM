namespace SWP.KitStem.API.RequestModel
{
    public class KitRequestModel
    {
        public string Name { get; set; } = null!;

        public string Brief { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int PurchaseCost { get; set; }

        public int CategoryId { get; set; }
        public bool Status { get; set; }

    }
}
