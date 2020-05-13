namespace HPlusSport.API.HelperClasses
{
    public class PriceFilter : PageSizeModifiers
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
