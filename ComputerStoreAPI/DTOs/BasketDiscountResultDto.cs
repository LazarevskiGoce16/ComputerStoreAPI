namespace ComputerStoreAPI.DTOs
{
    public class BasketDiscountResultDto
    {
        public decimal TotalPrice { get; set; }
        public decimal DiscountApplied { get; set; }
        public string? Error { get; set; }
    }
}
