namespace ComputerStoreAPI.DTOs
{
    public class ProductStockDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Categories { get; set; }
    }
}
