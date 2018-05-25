namespace ConvenientShop.API.Models
{
    public class ProductDetailSimpleDto
    {
        public string BarCode { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
    }
}