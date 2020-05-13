namespace HPlusSport.API.HelperClasses
{
    public class PriceFilter : PageModifier
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
