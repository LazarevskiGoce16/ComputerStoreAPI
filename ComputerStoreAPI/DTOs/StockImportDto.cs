namespace ComputerStoreAPI.DTOs
{
    public class StockImportDto
    {
        public string Name { get; set; }
        public List<string> Categories { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
