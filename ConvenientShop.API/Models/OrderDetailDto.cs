namespace ConvenientShop.API.Models
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public bool IsReceived { get; set; }
    }
}