namespace ConvenientShop.API.Models
{
    public class BillDetailDto
    {
        // public string BarCode { get; set; }
        // public string ProductName { get; set; }
        public int Quantity { get; set; }
        public ProductDetailSimpleDto ProductDetail { get; set; }
        public int SubTotal => ProductDetail.Price * Quantity;
    }
}